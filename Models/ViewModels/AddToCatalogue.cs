namespace Bookish.Models;

class AddToCatalogue
{
    public static void Main(CatalogueRequestModel catalogueRequest)
    {
        using (var context = new BookishContext()) {

            var std = new Catalogue()
            {
                    Title = catalogueRequest.Title,
                    // Id = 003,
                    Author = catalogueRequest.Author,
                    PublicationYear = catalogueRequest.PublicationYear
            };
            context.Catalogue.Add(std);
            context.SaveChanges();
        }
    }
}