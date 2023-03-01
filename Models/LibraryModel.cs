namespace Bookish.Models;


public class Catalogue
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }

}
public class Users
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Postcode { get; set; }
    public int PhoneNumber { get; set; }

}
public class Book
{
    public int Id { get; set; }
    public string Condition { get; set; }
}

public class Loan
{
    public int Id { get; set; }
    public string CheckoutDate { get; set; }
    public string DueDate { get; set; }
    public string ReturnedDate { get; set; }
    // FOREIGN KEY (teachers_id) REFERENCES teachers(id)
}


