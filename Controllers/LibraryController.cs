using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public interface IRetrieveList
{
    List<Catalogue> RetrieveList();
}

public interface IRetrieveListBooks
{
    List<string> RetrieveBooksList();
}
public class LibraryController : Controller
{
    private readonly ILogger<LibraryController> _logger;

    private readonly IRetrieveList _myService;

    private readonly IRetrieveListBooks _myServiceBookList;

    //this IRetrieveList is retrieved by the 'AddTransient()' function in Program.CS which provides an instance of a Class to myService
    public LibraryController(IRetrieveList myService, IRetrieveListBooks myServiceBookList, ILogger<LibraryController> logger)
    {
        _myService = myService;
        _myServiceBookList = myServiceBookList;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Books()
    {
        //context is an instance of our SQL database. 
        //context.catalogue is the Catalogue table from our SQL database
        var catalogueList = _myService.RetrieveList();

        return View(catalogueList);
    }

    public IActionResult CreateCatalogue()
    {
        return View();
    }
    // defines that the text within HTTPpost is performing a post action 
    [HttpPost]
    public IActionResult AddToCatalogue(CatalogueRequestModel catalogueRequest)
    {
        using (var context = new BookishContext())
        {

            var book = new Catalogue()
            {
                Title = catalogueRequest.Title,
                Author = catalogueRequest.Author,
                PublicationYear = catalogueRequest.PublicationYear
            };

            if (!ModelState.IsValid)
            {
                string allErrorsStr = "";
                foreach (var item in ModelState.ToList())
                {
                    if (item.Value.Errors.Any())
                    {
                        allErrorsStr += item.Value.Errors[0].ErrorMessage + "\n";
                    }
                }
                return BadRequest(allErrorsStr);
            }

            context.Catalogue.Add(book);
            context.SaveChanges();
        }
        return RedirectToAction("Books");
    }
    public IActionResult GetBookByTitle(string SearchString) //take in parameter from getbookby title here?
    {
        var bookList = _myService.RetrieveList();
        var selectedBookList = new List<Catalogue>();

        if (String.IsNullOrEmpty(SearchString))
        {
            selectedBookList = bookList;
        }
        else
        {
            foreach (var item in bookList)
            {
                if (item.Title.ToLower() == SearchString.ToLower())
                {
                    selectedBookList.Add(item);
                }
            }
        }
        return View(selectedBookList);
    }

    [HttpPost]
    public IActionResult DeleteFromCatalogue(int id)
    {
        using (var context = new BookishContext())
        {
            var ourBook = context.Catalogue.Find(id);
            //string res = Array.Find(arr, ele => ele.StartsWith("t",
            context.Catalogue.Remove(ourBook);
            context.SaveChanges();
        }
        return RedirectToAction("Books");
    }

    public IActionResult CreateCopyOfBook()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddBookCopy(CopyOfBookRequestModel newCopyOfBook)
    {
        using (var context = new BookishContext())
        {
            var newBook = new Book()
            {
                Condition = newCopyOfBook.Condition,
                Catalogue_id = newCopyOfBook.Catalogue_id
                //DO I need to specify Catalogue = *get Catalogue by Catalogue ID?*

            };
            context.Books.Add(newBook);
            context.SaveChanges();
            return RedirectToAction("CreateCopyOfBook");
        }
    }
    public IActionResult BookList()
    {
        var ourBookList = _myServiceBookList.RetrieveBooksList();

        return View(ourBookList);


    }
    public IActionResult CatalogueQuantities()
    {
        using (var context = new BookishContext())
        {
            var catalogue = context.Catalogue;
            var book = context.Books;

            var query = catalogue.GroupJoin(book,
            //where to join
            catalogue => catalogue.Id,
            book => book.Catalogue_id,
            (catalogue, catalogueGroup) => new
            {
                Title = catalogue.Title,
                TitleCount = catalogueGroup.Count()
            });

            var catalogueQuantitiesList = new List<string>();

            foreach (var ourBook in query)
            {
                catalogueQuantitiesList.Add($"{ourBook.Title}:    {ourBook.TitleCount}");
            }

            return View(catalogueQuantitiesList);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

