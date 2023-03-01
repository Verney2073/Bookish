namespace Bookish.Models;

class LibraryAddTest
{
    public static void Main()
    {
        using (var context = new BookishContext()) {

            var std = new Catalogue()
            {
                    Title = "Moby Dick",
                    Id = 003,
                    Author = "Pete",
                    PublicationYear = 1865
            };
            context.Catalogue.Add(std);
            context.SaveChanges();
        }
    }
}