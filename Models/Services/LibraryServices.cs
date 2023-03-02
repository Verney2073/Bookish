using Bookish.Controllers;
using Bookish.Models;

namespace Bookish;

public class RetrieveCatalogueList : IRetrieveList
{
    public List<Catalogue> RetrieveList()
    {
        var context = new BookishContext();
        List<Catalogue> SortedList = context.Catalogue.ToList().OrderBy(o => o.Author).ToList();
        return SortedList;
    }
}