namespace Bookish.Models;

class CatalogueViewModel
{
    public List<Catalogue> OurCatalogue { get; set; }

    public CatalogueViewModel()
    {
        var context = new BookishContext();
        var fullCatalogue = context.Catalogue.ToList();
        OurCatalogue = fullCatalogue;
    }
    public List<string> GetTitles()
    {
        var context = new BookishContext();
        var fullCatalogue = context.Catalogue.ToList();
        var titlesList = fullCatalogue.Select(a => a.Title).ToList();
        return titlesList;
    }

    public void SortBookTitles()
    {
        List<Catalogue> SortedList = OurCatalogue.OrderBy(o => o.Title).ToList();
        OurCatalogue = SortedList;
    }
}
