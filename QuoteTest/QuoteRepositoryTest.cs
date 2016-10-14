using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteRepository;
using QuoteRepository.POCO;
using System.Collections.Generic;
using System.Configuration;

namespace QuoteTest
{
    [TestClass]
    public class QuoteRepositoryTest
    {
        [TestMethod]
        public void ReadAllQuotes()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuoteTest.Properties.Settings.MyDBConnectionString"].ConnectionString;
            using (QuoteDB db = new QuoteDB(connectionString))
            {
                List<TheQuote> list = db.GetAllQuotes();
                Assert.IsTrue(list.Count > 0);
                foreach (TheQuote q in list)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(q.AuthorsFirstName));
                    Console.WriteLine("{0} {1}", q.AuthorsFirstName, q.AuthorsLastName);
                }
            }
        }

        [TestMethod]
        public void ReadSingleQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuoteTest.Properties.Settings.MyDBConnectionString"].ConnectionString;
            using (QuoteDB db = new QuoteDB(connectionString))
            {
                TheQuote quote = db.GetSingleQuote(1);
                Assert.IsNotNull(quote);
                Assert.IsFalse(string.IsNullOrEmpty(quote.AuthorsFirstName));
                Console.WriteLine("{0} {1}", quote.AuthorsFirstName, quote.AuthorsLastName);
            }
        }

        [TestMethod]
        public void QuotationDBConstructor()
        {
            TheQuote quotes = new TheQuote()
            {
                AuthorsFirstName = "Tefirst",
                AuthorsLastName = "daLast",
                PublicationsName = "The Mighty C# Program",
                PublishedDate = DateTime.MaxValue,
                Quote = "This class was so amazing, I cried Every day I missed it",
                Type = ReferenceType.Artical,
                Url = "https://www.thebestwebsite.com/thecool/artical/isHere.html",
                Published = true
            };
        }

        [TestMethod]
        public void InsertNewQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuoteTest.Properties.Settings.MyDBConnectionString"].ConnectionString;
            using (QuoteDB db = new QuoteDB(connectionString))
            {

            TheQuote q = new TheQuote()
            {
                Id = -1,
                AuthorsFirstName = "Farmer",
                AuthorsLastName = "Joe",
                PublicationsName = "How to Farm",
                PublishedDate = DateTime.Now,
                Quote = "If there was a Cow for every farmer, then the world would be out of cows",
                Type = ReferenceType.Book,
                Url = "www.wearefarmersdadadadadadada.com",
                Published = true
            };
                db.InsertNewQuote(q);
                Assert.IsTrue(q.Id > 0);
            }
        }

        [TestMethod]
        public void UpdateQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuoteTest.Properties.Settings.MyDBConnectionString"].ConnectionString;
            using (QuoteDB db = new QuoteDB(connectionString))
            {
            TheQuote q = new TheQuote()
            {
                Id = -1,
                AuthorsFirstName = "Farmer",
                AuthorsLastName = "Joe",
                PublicationsName = "How to Farm",
                PublishedDate = DateTime.Now,
                Quote = "If there was a Cow for every farmer, then the world would be out of cows",
                Type = ReferenceType.Book,
                Url = "www.wearefarmersdadadadadadada.com",
                Published = true

            };

                db.InsertNewQuote(q);
                Assert.IsTrue(q.Id > 0);

                q.AuthorsFirstName = "Farmer1";
                q.AuthorsLastName = "Joe1";
                q.PublicationsName = "How to Farm1";
                q.PublishedDate = DateTime.Now;
                q.Quote = "If there was a Cow for every farmer, then the world would be out of cows1";
                q.Type = ReferenceType.Book;
                q.Url = "www.wearefarmersdadadadadadada.com1";
                q.Published = true;

                db.UpdateQuote(q);
                TheQuote q2 = db.GetSingleQuote(q.Id);
                Assert.AreEqual(q.AuthorsFirstName, q2.AuthorsFirstName);
                Assert.AreEqual(q.AuthorsLastName, q2.AuthorsLastName);
                Assert.AreEqual(q.PublicationsName, q2.PublicationsName);
                Assert.AreEqual(q.Quote, q2.Quote);
                Assert.AreEqual(q.Type, q2.Type);
                Assert.AreEqual(q.Url, q2.Url);
                Assert.AreEqual(q.Published, q2.Published);
            }
        }

        [TestMethod]
        public void DeleteQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuoteTest.Properties.Settings.MyDBConnectionString"].ConnectionString;
            using (QuoteDB db = new QuoteDB(connectionString))
            {
                TheQuote q = new TheQuote()
                {
                    Id = -1,
                    AuthorsFirstName = "Farmr",
                    AuthorsLastName = "Joe",
                    PublicationsName = "How to Farm",
                    PublishedDate = DateTime.Now,
                    Quote = "If there was a Cow for every farmer, then the world would be out of cows",
                    Type = ReferenceType.Book,
                    Url = "www.wearefarmersdadadadadadada.com",
                    Published = true
                };

                db.InsertNewQuote(q);
                Assert.IsTrue(q.Id > 0);

                db.DeleteQuote(q.Id);
                TheQuote q2 = db.GetSingleQuote(q.Id);
                Assert.IsTrue(q2 == null);
            }
        }
    }
}
