using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuoteMaster.Models
{
    public class QuoteModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Author's First Name")]
        public string AuthorsFirstName { get; set; }

        [Required]
        [Display(Name ="Author's Last Name")]
        public string AuthorsLastName { get; set; }

        [Required]
        [Display(Name = "Publishing Company")]
        public string PublicationsName { get; set; }

        [Required]       
        [Display(Name = "Reference Type")]
        public string Type { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The Quote value cannot exceed 200 characters. ")]
        [Display(Name = "Offical Quote")]
        public string Quote { get; set; }
        
        [Display(Name = "Link to Quote")]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", 
            ErrorMessage = "Must Use standard URL format Ex: http://www.something.com")]
        [Required]
        public string Url { get; set; }

        [Required]
        [UIHint("IsActive")]
        [Display(Name = "Published")]
        public bool Published { get; set; }
         
        [Required]
        [Range(1, 200000000, ErrorMessage = "Publication number must be >= 1 or <= 200000000")]
        [Display(Name = "Publication Number")]
        public int PublicationNumber { get; set; }

        public List<SelectListItem> ReferenceList { get; set; }

    }  
  
}