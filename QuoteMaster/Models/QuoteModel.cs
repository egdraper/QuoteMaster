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
        [StringLength(200, ErrorMessage = "The Quote value cannot exceed 200 characters. ")]
        [Display(Name = "Offical Quote")]
        public string Quote { get; set; }
        
        [Display(Name = "Link to Quote")]
        public string Url { get; set; }

        [Required]
        [UIHint("IsActive")]
        [Display(Name = "Published")]
        public bool Published { get; set; }
         
        [Required]
        [Range(0, 200000000, ErrorMessage = "Publication number must greater than 0")]
        [Display(Name = "Publication Number")]
        public int PublicationNumber { get; set; }

        public List<SelectListItem> ReferenceList { get; set; }

    }  
  
}