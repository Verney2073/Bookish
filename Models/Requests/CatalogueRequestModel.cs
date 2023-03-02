using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookish.Models;

public class CatalogueRequestModel
{
    // int currentYear = DateTime.Now.Year;
    [Display(Name = "Title")]
    [StringLength(40)]
    [Required]
    public string Title { get; set; }
    [StringLength(40)]
    [Required]
    public string Author { get; set; }

    //Should be able to grab DateTime.Today
    [Display(Name = "Publication Year")]
    [Range(1, 2023, ErrorMessage = "Year must be 0 to 2023.")]
    [Required]
    public int PublicationYear { get; set; }
}




// [StringLength(60, MinimumLength = 3)]
// [Required]
// public string? Title { get; set; }

// [Display(Name = "Release Date")]
// [DataType(DataType.Date)]
// public DateTime ReleaseDate { get; set; }

// [Range(1, 100)]