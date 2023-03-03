using Bookish.Controllers;
using Bookish.Models;

namespace Bookish;

public class RetrieveCatalogueList : IRetrieveList
{
    public List<Catalogue> RetrieveList()
    {
        var context = new BookishContext();
        List<Catalogue> SortedList = context.Catalogue.ToList().OrderBy(o => o.Author).ToList();
        
        //add a column here using the BookList
        return SortedList;
    }
}

// public class RetrieveBookList: IRetrieveList {
//     //Join catalogue list with Copies of books list 
//     public List<Catalogue> RetrieveList()
//     {
//         var context = new BookishContext();
//         //Return a list of some other object
//         List<Catalogue> SortedList = context.Catalogue.ToList().OrderBy(o => o.Author).ToList();
        
//         //add a column here using the BookList
//         return SortedList;
//     }

// }

// // public List<Catalogue> RetrieveList()
// public class RetrieveBookList
// {
//     public List<T> RetrieveBookList() {
//     var context = new BookishContext();
    
//     var query = from item in context.Books
//                 join item in context.Books on item.Catalogue_id equals catalogue.Catalogue_id
//                 select new {
//                     Catalogue = catalogue,
//                     Condition = item.Condition // Add a new column for the book title
//                 };
                
//     // Project the anonymous type back into Catalogue objects
//     var sortedList = query.ToList().Select(x => {
//         var catalogue = x.Catalogue;
//         catalogue.Condition = x.Condition;
//         return catalogue;
//     }).ToList();
    
//     return sortedList;
//     }
// }
