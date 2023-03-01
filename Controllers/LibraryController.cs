using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class LibraryController : Controller
{
    private readonly ILogger<LibraryController> _logger;

    public LibraryController(ILogger<LibraryController> logger)
    {
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
        var context = new BookishContext();
        var catalogueList = context.Catalogue.ToList();

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

            context.Catalogue.Add(book);
            context.SaveChanges();
        }
        return RedirectToAction("Books");
    }

    public IActionResult GetBookByTitle(string SearchString) //take in parameter from getbookby title here?
    {
        using (var context = new BookishContext()) 
        {
            var bookList = context.Catalogue.ToList();

            
            var selectedBookList = new List<Catalogue>();
            foreach (var item in bookList)
            {
                if (item.Title == SearchString)
                {
                    selectedBookList.Add(item);
                }
            }
            return View(selectedBookList);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
