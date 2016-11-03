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
        [ValidateInput(true)]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

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
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuoteModel model)
        {
            try
            {
                TheQuote theQuote = CheckModel(model);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    using (QuoteDB db = new QuoteDB(connectionString))
                    {
                        db.InsertNewQuote(theQuote);
                    }
                    return RedirectToAction("Index");
                }

               
            }
            catch (Exception e)
            {
                model.ReferenceList = getQuoteQulities();
                return View(model);
            }
        }

        // GET: Quote/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuoteModel model)
        {
            try
            {
               TheQuote theQuote = CheckModel(model);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    using (QuoteDB db = new QuoteDB(connectionString))
                    {
                        db.UpdateQuote(theQuote);
                    }
                    return RedirectToAction("Index");
                }               
            }
            catch (Exception e)
            {
                model.ReferenceList = getQuoteQulities();
                return View(model);
            }
        }

        // GET: Quote/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, QuoteModel model)
        {
            if (model.Id == null)
            {
                return View();
            }
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

        private TheQuote CheckModel(QuoteModel model)
        {
            var theQuote = new TheQuote();

            theQuote.AuthorsFirstName = model.AuthorsFirstName;
            if (string.IsNullOrEmpty(theQuote.AuthorsFirstName))
                ModelState.AddModelError("AuthorsFirstName", "Authors first name is required");

            theQuote.AuthorsLastName = model.AuthorsLastName;
            if (string.IsNullOrEmpty(theQuote.AuthorsLastName))
                ModelState.AddModelError("AuthorsLastName", "Authors last name is required");

            theQuote.PublicationsName = model.PublicationsName;
            if (string.IsNullOrEmpty(theQuote.PublicationsName))
                ModelState.AddModelError("PublicationsName ", "Publication name is required");

            theQuote.Id = model.Id;
            if (theQuote.Id == null)
                ModelState.AddModelError("Id", "An Id is required for submission");
            if (theQuote.Id < 0)
                ModelState.AddModelError("Id", "An Id must be greater then 0");

            //date is not required (can be null).
            theQuote.PublishedDate = model.PublishedDate;

            theQuote.Quote = model.Quote;
            if (string.IsNullOrEmpty(theQuote.Quote))
                ModelState.AddModelError("Quote", "A quote is required");

            theQuote.Url = model.Url;

            if (model.Published == null)
                ModelState.AddModelError("Quote", "There must be a publication given");
            else
                theQuote.Published = model.Published;

            if (string.IsNullOrEmpty(model.Type))
                ModelState.AddModelError("Type", "A type is required");
            else
            {
                if (string.Compare(model.Type, "Book", true) == 0)
                    theQuote.Type = ReferenceType.Book;
                if (string.Compare(model.Type, "Artical", true) == 0)
                    theQuote.Type = ReferenceType.Artical;
                if (string.Compare(model.Type, "WebPost", true) == 0)
                    theQuote.Type = ReferenceType.WebPost;
                if (string.Compare(model.Type, "PersonalQuote", true) == 0)
                    theQuote.Type = ReferenceType.PersonalQuote;
            }

            if (model.PublicationNumber == null)
                ModelState.AddModelError("Type", "A type is required");
            else
                theQuote.PublicationNumber = model.PublicationNumber;

            if(model.ReferenceList == null)
            {
                model.ReferenceList = new List<SelectListItem>();
            }

            return theQuote;
        }
    }
}
