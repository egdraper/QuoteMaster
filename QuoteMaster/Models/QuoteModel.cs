using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuoteMaster.Models
{
    public enum ReferenceType
    {
        Book,
        Artical,
        WebPost,
        PersonalQuote
    }

    public class QuoteModel
    {
        public int Id { get; set; }

        [Display(Name ="Authors First Name")]
        public string AuthorsFirstName { get; set; }

        [Display(Name ="Authors Last Name")]
        public string AuthorsLastName { get; set; }

        [Display(Name = "Publishing Company")]
        public string PublicationsName { get; set; }

        [Display(Name = "Reference Type")]
        public ReferenceType? Type { get; set; }
 
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "Offical Quote")]
        public string Quote { get; set; }

        [Display(Name = "Link to Quote")]
        public string Url { get; set; }

        [Display(Name = "Published")]
        public bool Published { get; set; }

        public List<SelectListItem> ReferenceList { get; set; }

    }  
  
}