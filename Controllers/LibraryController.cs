using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public interface IRetrieveList
{
    List<Catalogue> RetrieveList();
}

public class LibraryController : Controller
{
    private readonly ILogger<LibraryController> _logger;

    private readonly IRetrieveList _myService;

    public LibraryController(IRetrieveList myService, ILogger<LibraryController> logger)
    {
        _myService = myService;
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
            context.Catalogue.Remove(ourBook);
            context.SaveChanges();
        }
        return RedirectToAction("Books");
    }

    public IActionResult CreateCopyOfBook()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
