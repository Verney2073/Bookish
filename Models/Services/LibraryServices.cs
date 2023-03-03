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

public class RetrieveBookCopyList : IRetrieveListBooks
{
    public List<string> RetrieveBooksList()
    {
        using (var context = new BookishContext())
        {
            var catalogue = context.Catalogue;
            var book = context.Books;

            var query = book.GroupJoin(catalogue,
            //where to join
            book => book.Catalogue_id,
            catalogue => catalogue.Id,
            (book, bookGroup) => new
            {
                BookItem = book.Catalogue,
                BookCondition = book.Condition
            });

            var bookList = new List<string>();

            foreach (var ourBook in query)
            {
                bookList.Add($"{ourBook.BookItem.Title}, {ourBook.BookItem.Author}, {ourBook.BookItem.PublicationYear} and {ourBook.BookCondition}");
            }

            return bookList;

        }
    }
}
