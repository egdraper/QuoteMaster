using QuoteMaster.Models;
using QuoteRepository;
using QuoteRepository.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuoteMaster.Controllers
{
    public class QuoteController : Controller
    {

        string connectionString = ConfigurationManager.ConnectionStrings["QuoteTest.Properties.Settings.MyDBConnectionString"].ConnectionString;
        List<SelectListItem> _quoteQualities = null;
        // GET: Quote
        public ActionResult Index()
        {
            List<QuoteModel> quotes = new List<QuoteModel>();

            using (QuoteDB db = new QuoteDB(connectionString))
            {
                List<TheQuote> temp = db.GetAllQuotes();
                foreach (TheQuote tq in temp)
                {
                    quotes.Add(new QuoteModel()
                    {
                        AuthorsFirstName = tq.AuthorsFirstName,
                        AuthorsLastName = tq.AuthorsLastName,
                        PublicationsName = tq.PublicationsName,
                        Id = tq.Id,
                        PublishedDate = tq.PublishedDate,
                        Quote = tq.Quote,
                        Type = tq.Type.ToString(),
                        Url = tq.Url,
                        Published = tq.Published,
                        PublicationNumber = tq.PublicationNumber
                        
                    });

                }

            }

            return View(quotes);
        }

        // GET: Quote/Details/5
        public ActionResult Details(int id)
        {
            QuoteModel quote;
            using (QuoteDB db = new QuoteDB(connectionString))
            {
                TheQuote tq = db.GetSingleQuote(id);
                quote = new QuoteModel()
                {
                    AuthorsFirstName = tq.AuthorsFirstName,
                    AuthorsLastName = tq.AuthorsLastName,
                    PublicationsName = tq.PublicationsName,
                    Id = tq.Id,
                    PublishedDate = tq.PublishedDate,
                    Quote = tq.Quote,
                    Type = tq.Type.ToString(),
                    Url = tq.Url,
                    Published = tq.Published,
                    PublicationNumber = tq.PublicationNumber
                };
            }
            return View(quote);
        }

        // GET: Quote/Create
        public ActionResult Create()
        {
            QuoteModel a = new QuoteModel();
            a.ReferenceList = this.getQuoteQulities();

            return View(a);
        }

        private List<SelectListItem> getQuoteQulities()
        {
            _quoteQualities = null;

            return new List<SelectListItem>()
            {
                new SelectListItem() {Selected = false, Text = "Book", Value = "Book" },
                new SelectListItem() {Selected = false, Text = "Artical", Value = "Artical" },
                new SelectListItem() {Selected = false, Text = "Web Post", Value = "WebPost" },
                new SelectListItem() {Selected = false, Text = "Personal Quote", Value = "PersonalQuote" },
            };                 
        }

        // POST: Quote/Create
        [HttpPost]
        public ActionResult Create(QuoteModel model)
        {
            try
            {
                var theQuote = new TheQuote()
                {
                    AuthorsFirstName = model.AuthorsFirstName,
                    AuthorsLastName = model.AuthorsLastName,
                    PublicationsName = model.PublicationsName,
                    Id = model.Id,
                    PublishedDate = model.PublishedDate,
                    Quote = model.Quote,
                    Url = model.Url,
                    Published = model.Published,
                    Type = (ReferenceType)Enum.Parse(typeof(ReferenceType), model.Type),
                    PublicationNumber = model.PublicationNumber,
                };

                using (QuoteDB db = new QuoteDB(connectionString))
                {
                    db.InsertNewQuote(theQuote);
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                model.ReferenceList = getQuoteQulities();
                return View(model);
            }
        }

        // GET: Quote/Edit/5
        public ActionResult Edit(int id)
        {
            QuoteModel quote;
            
            using (QuoteDB db = new QuoteDB(connectionString))
            {
                TheQuote tq = db.GetSingleQuote(id);
                quote = new QuoteModel()
                {
                    AuthorsFirstName = tq.AuthorsFirstName,
                    AuthorsLastName = tq.AuthorsLastName,
                    PublicationsName = tq.PublicationsName,
                    Id = tq.Id,
                    PublishedDate = tq.PublishedDate,
                    Quote = tq.Quote,
                    Url = tq.Url,
                    Published = tq.Published,
                    Type = tq.Type.ToString(),
                    PublicationNumber = tq.PublicationNumber,
                    ReferenceList = getQuoteQulities(),
                };
            }

            return View(quote);
        }

        // POST: Quote/Edit/5
        [HttpPost]
        public ActionResult Edit(QuoteModel model)
        {           
            try
            {
                var theQuote = new TheQuote()
                {
                    AuthorsFirstName = model.AuthorsFirstName,
                    AuthorsLastName = model.AuthorsLastName,
                    PublicationsName = model.PublicationsName,
                    Id = model.Id,
                    PublishedDate = model.PublishedDate,
                    Quote = model.Quote,
                    Url = model.Url,
                    Type = (ReferenceType)Enum.Parse(typeof(ReferenceType), model.Type),
                    Published = model.Published,
                    PublicationNumber = model.PublicationNumber
                };

                using (QuoteDB db = new QuoteDB(connectionString))
                {
                    db.UpdateQuote(theQuote);
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                model.ReferenceList = getQuoteQulities();
                return View(model);
            }
        }

        // GET: Quote/Delete/5
        public ActionResult Delete(int id)
        {
            QuoteModel quote;
            using (QuoteDB db = new QuoteDB(connectionString))
            {
                TheQuote tq = db.GetSingleQuote(id);
                quote = new QuoteModel()
                {
                    AuthorsFirstName = tq.AuthorsFirstName,
                    AuthorsLastName = tq.AuthorsLastName,
                    PublicationsName = tq.PublicationsName,
                    Id = tq.Id,
                    PublishedDate = tq.PublishedDate,
                    Quote = tq.Quote,
                    Url = tq.Url,
                    Published = tq.Published,
                    Type = tq.Type.ToString(),
                    PublicationNumber = tq.PublicationNumber
                };
            }

            return View(quote);    
        }

        // POST: Quote/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, QuoteModel model)
        {
            try
            {
                using (QuoteDB db = new QuoteDB(connectionString))
                {
                    db.DeleteQuote(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
