using System.ComponentModel.DataAnnotations.Schema;

namespace Bookish.Models;

public class Catalogue
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public ICollection<Book> Books { get; set; }

    // public List<Catalogue> CatalogueViewModel()
    // {
    //     var context = new BookishContext();
    //     var fullCatalogue = context.Catalogue.ToList();
    //     OurCatalogue = fullCatalogue;
    // }

    // public void SortBookTitles()
    // {
    //     List<Catalogue> SortedList = OurCatalogue.OrderBy(o => o.Title).ToList();
    //     OurCatalogue = SortedList;
    // }
}
