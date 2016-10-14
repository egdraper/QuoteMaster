using QuoteRepository.POCO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace QuoteRepository
{
    public class QuoteDB : IDisposable
    {
        private SqlConnection _conn;
        public QuoteDB(string connectionString)
        {
            _conn = new SqlConnection();
            _conn.ConnectionString = connectionString;
            _conn.Open();
        }

        public void Dispose()
        {
            _conn.Close();
        }

        public List<TheQuote> GetAllQuotes()
        {
            var sql = @"SELECT
                         Id, AuthorsFirstName, AuthorsLastName, PublicationsName,
                         Type, PublishedDate, Quote, Url, Published 
                        FROM dbo.Quote";

            var quoteList = new List<TheQuote>();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TheQuote quote = new TheQuote()
                        {
                            Id = (int)reader["Id"],
                            AuthorsFirstName = (string)reader["AuthorsFirstName"],
                            AuthorsLastName = (string)reader["AuthorsLastName"],
                            PublicationsName = (string)reader["PublicationsName"],
                            PublishedDate = (DateTime)reader["PublishedDate"],
                            Quote = (string)reader["Quote"],
                            Url = (string)reader["Url"],
                            Type = (ReferenceType)Enum.Parse(typeof(ReferenceType), (string)reader["Type"]),
                            Published = (bool)reader["Published"]

                        };


                        quoteList.Add(quote);
                    }
                }
            }
            return quoteList;
        }

        public TheQuote GetSingleQuote(int id)
        {
            var sql = @"SELECT
                         ID, AuthorsFirstName, AuthorsLastName, PublicationsName,
                         Type, PublishedDate, Quote, Url, Published
                        FROM dbo.Quote 
                        WHERE Id = @id";

            TheQuote quote = null;
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quote = new TheQuote()
                        {
                            Id = (int)reader["Id"],
                            AuthorsFirstName = (string)reader["AuthorsFirstName"],
                            AuthorsLastName = (string)reader["AuthorsLastName"],
                            PublicationsName = (string)reader["PublicationsName"],
                            PublishedDate = (DateTime)reader["PublishedDate"],
                            Quote = (string)reader["Quote"],
                            Url = (string)reader["Url"],                          
                            Type = (ReferenceType)Enum.Parse(typeof(ReferenceType), (string)reader["Type"]),
                            Published = (bool)reader["Published"]
                        };
                    }
                }
            }
            return quote;
        }

        public void InsertNewQuote(TheQuote quote)
        {
            var sql = @"INSERT INTO dbo.Quote
                         (AuthorsFirstName, AuthorsLastName, PublicationsName,
                         Type, PublishedDate, Quote, Url, Published)
                        VALUES ( @authorsFirstName, @authorsLastName, @publicationsName,
                         @type, @publishedDate, @quote, @url, @published)

                       SELECT Id = Scope_Identity()";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@authorsFirstName", quote.AuthorsFirstName);
                cmd.Parameters.AddWithValue("@authorsLastName", quote.AuthorsLastName);
                cmd.Parameters.AddWithValue("@publicationsName", quote.PublicationsName);
                cmd.Parameters.AddWithValue("@type", quote.Type.ToString());
                cmd.Parameters.AddWithValue("@publishedDate", quote.PublishedDate);
                cmd.Parameters.AddWithValue("@quote", quote.Quote);
                cmd.Parameters.AddWithValue("@url", quote.Url);
                cmd.Parameters.AddWithValue("@published", quote.Published);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quote.Id = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
        }

        public void UpdateQuote(TheQuote quote)
        {
            var sql = @"UPDATE dbo.Quote
                         Set AuthorsFirstName = @authorsFirstName,
                             AuthorsLastName = @authorsLastName,
                             PublicationsName = @publicationsName,
                             Type = @type, 
                             PublishedDate = @publishedDate, 
                             Quote = @quote, 
                             Url = @url 
                             Published = @published
                        WHERE Id = @id";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@authorsFirstName", quote.AuthorsFirstName);
                cmd.Parameters.AddWithValue("@authorsLastName", quote.AuthorsLastName);
                cmd.Parameters.AddWithValue("@publicationsName", quote.PublicationsName);
                cmd.Parameters.AddWithValue("@type", quote.Type.ToString());
                cmd.Parameters.AddWithValue("@publishedDate", quote.PublishedDate);
                cmd.Parameters.AddWithValue("@quote", quote.Quote);
                cmd.Parameters.AddWithValue("@url", quote.Url);
                cmd.Parameters.AddWithValue("@id", quote.Id);
                cmd.Parameters.AddWithValue("@published", quote.Published);

                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount < 1)
                {
                    throw new Exception("No Rows have been updated in the method UpdateQuotes");
                }
            }
        }

        public void DeleteQuote(int id)
        {
            var sql = @"DELETE FROM dbo.Quote WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount < 1)
                {
                    throw new Exception("No Rows Have been deleted ");
                }
            }
        }
    }
}
