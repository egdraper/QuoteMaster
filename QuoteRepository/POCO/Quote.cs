using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteRepository.POCO
{
   
        public enum ReferenceType
        {
            Book,
            Artical,
            WebPost,
            PersonalQuote
        }

        public class TheQuote
        {
            public int Id { get; set; }
            public string AuthorsFirstName { get; set; }
            public string AuthorsLastName { get; set; }
            public string PublicationsName { get; set; }
            public ReferenceType Type { get; set; }
            public DateTime PublishedDate { get; set; }
            public string Quote { get; set; }
            public string Url { get; set; }
            public bool Published { get; set; }
            public int PublicationNumber { get; set; }
        
    }
}
