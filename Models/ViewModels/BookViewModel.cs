using System.ComponentModel.DataAnnotations.Schema;

namespace Bookish.Models;

public class Book
{
    public int Id { get; set; }
    public string Condition { get; set; }

    // FOREIGN KEY (Catalogue_id) REFERENCES Catalogue(id)
    public int Catalogue_id { get; set; }
    [ForeignKey("Catalogue_id")]
    public Catalogue Catalogue { get; set; }
}