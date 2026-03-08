//Media class 
public abstract class Media
{
    public int Id { get; set; }
    public string Title { get; protected set; }
    public bool IsAvailable { get; protected set; } = true;
    public Media(int id, string title)
    {
        Id = id;
        Title = title;
    }

    //Mark Media as Borrowed
    public virtual void Borrow()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"'{Title}' is already borrowed.");

        IsAvailable = false;
        //Console.WriteLine($"You have borrowed '{Title}'.");
    }
    //Mark Media as Returned
    public virtual void Return()
    {
        IsAvailable = true;
    }

    public abstract string GetMediaInfo();
}

//Book class inheriting from Media
public class Book : Media
{
    public string Author { get; private set; }
    public string Genre { get; private set; }
    public Book(int id, string title, string author, string genre) : base(id, title)
    {
        Author = author;
        Genre = genre;
    }
    public override string GetMediaInfo()
    {
        return $"[Book] {Id} - {Title} by {Author} (Genre: {Genre}) | {(IsAvailable ? "Available" : "Borrowed")}";
    }
}

//DVD class inheriting from Media
public class DVD : Media
{
    public string Director { get; private set; }
    public int Runtime { get; private set; } // in minutes
    public DVD(int id, string title, string director, int runtime) : base(id, title)
    {
        Director = director;
        Runtime = runtime;
    }
    public override string GetMediaInfo()
    {
        return $"[DVD] {Id} - {Title} directed by {Director} (Runtime: {Runtime} mins) - {(IsAvailable ? "Available" : "Borrowed")}";
    }
}

//Borrower class
public class Borrower
{
    public int Id { get; set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Borrower(int id, string name, string address, string phoneNo, string email)
    {
        Id = id;
        Name = name;
        Address = address;
        PhoneNumber = phoneNo;
        Email = email;
    }
    public override string ToString()
    {
        return $"[Borrower] {Id} - {Name}, Address: {Address}, Phone: {PhoneNumber}, Email: {Email}";
    }
}

//Library class to manage media and borrowers
public class Library
{
    private List<Media> mediaCollection = new();
    private List<Borrower> borrowers = new();
    private int nextMediaId = 1;
    private int nextBorrowerId = 1;
    //Add a Book to the library
    public Book AddBook(string title, string author, string genre)
    {
        var book = new Book(nextMediaId++, title, author, genre);
        mediaCollection.Add(book);
        return book;
    }
    //Add a DVD to the library
    public DVD AddDVD(string title, string director, int runtime)
    {
        var dvd = new DVD(nextMediaId++, title, director, runtime);
        mediaCollection.Add(dvd);
        return dvd;
    }

    //Remove Media by Id
    public bool RemoveMedia(int mediaId)
    {
        var media = mediaCollection.FirstOrDefault(m => m.Id == mediaId);
        if (media == null)
            throw new InvalidOperationException($"Media with ID {mediaId} not found.");
        mediaCollection.Remove(media);
        return true;
    }

    //Add a Borrower to the library
    public Borrower AddBorrower(string name, string address, string phoneNo, string email)
    {
        var borrower = new Borrower(nextBorrowerId++, name, address, phoneNo, email);
        borrowers.Add(borrower);
        return borrower;
    }

    //Remove Borrower by Id
    public bool RemoveBorrower(int borrowerId)
    {
        var borrower = borrowers.FirstOrDefault(b => b.Id == borrowerId);
        if (borrower == null)
            throw new InvalidOperationException($"Borrower with ID {borrowerId} not found.");
        borrowers.Remove(borrower);
        return true;
    }

    //Borrow Media
    public bool BorrowMedia(int mediaId)
    {
        var media = mediaCollection.FirstOrDefault(m => m.Id == mediaId);
        if (media == null)
            throw new InvalidOperationException($"Media with ID {mediaId} not found.");
        media.Borrow();
        return true;
    }

    //Return Media
    public bool ReturnMedia(int mediaId)
    {
        var media = mediaCollection.FirstOrDefault(m => m.Id == mediaId);
        if (media == null)
            throw new InvalidOperationException($"Media with ID {mediaId} not found.");
        media.Return();
        return true;
    }
    //Display all Media in the library
    public void DisplayAllMedia()
    {
        foreach (var media in mediaCollection)
        {
            Console.WriteLine(media.GetMediaInfo());
        }
    }

    //Display all Borrowers in the library
    public void DisplayAllBorrowers()
    {
        foreach (var borrower in borrowers)
        {
            Console.WriteLine(borrower);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // Adding media to the library
        //books such as title, author, and genre, as well for DVDs as title, director, runtime.

        //Original books in the library
        var book1 = library.AddBook("The Great Gatsby", "F. Scott Fitzgerald", "Fantasy");
        var book2 = library.AddBook("To Kill a Mockingbird", "Harper Lee", "Novel");
        var book3 = library.AddBook("1984", "George Orwell", "Dystopian Fiction");
        var dvd1 = library.AddDVD("Inception", "Christopher Nolan", 148);
        var dvd2 = library.AddDVD("The Matrix", "The Wachowskis", 136);
        var dvd3 = library.AddDVD("The Lord of the Rings: The Fellowship of the Ring", "Peter Jackson", 228);

        //Add a Borrower
        var borrower1 = library.AddBorrower("John Doe", "234 Downtown Ave.", "403-555-1234", "john.doe@example.com");
        var borrower2 = library.AddBorrower("Jane Smith", "567 Uptown Blvd.", "403-555-5678", "jane.smith@example.com");

        // Borrowing media
        library.BorrowMedia(book1.Id);

        // Displaying all Initial media in the library
        Console.WriteLine("\n---All Initial media in the library---");
        library.DisplayAllMedia();

        //Displaying all borrowers in the library
        Console.WriteLine("\n---All Initial borrowers---");

        ////Returning media
        //library.ReturnMedia(book1.Id);

        //Console.WriteLine("\n--After Returning---");
        //library.DisplayAllMedia();


        //User Menu

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n---Library Menu---");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add DVD");
            Console.WriteLine("3. Remove Media");
            Console.WriteLine("4. Borrow Media");
            Console.WriteLine("5. Return Media");
            Console.WriteLine("6. Display All Media");
            Console.WriteLine("7. Add Borrower");
            Console.WriteLine("8. Display All Borrowers");
            Console.WriteLine("9. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter Book Title: ");
                    string bookTitle = Console.ReadLine();

                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();

                    Console.Write("Enter Genre: ");
                    string genre = Console.ReadLine();

                    var newBook = library.AddBook(bookTitle, author, genre);
                    Console.WriteLine($"Book added with ID {newBook.Id} successfully.");
                    break;

                case "2":
                    Console.Write("Enter DVD Title: ");
                    string dvdTitle = Console.ReadLine();

                    Console.Write("Enter Director: ");
                    string director = Console.ReadLine();

                    Console.Write("Enter Runtime (in minutes): ");
                    int runtime = int.Parse(Console.ReadLine());

                    var newDVD = library.AddDVD(dvdTitle, director, runtime);
                    Console.WriteLine($"DVD added with ID {newDVD.Id} successfully.");
                    break;

                case "3":
                    Console.Write("Enter Media ID to remove: ");
                    int mediaId = int.Parse(Console.ReadLine());
                    if (library.RemoveMedia(mediaId))
                        Console.WriteLine("Media removed successfully.");
                    else
                        Console.WriteLine("Media not found.");
                    break;

                case "4":
                    Console.Write("Enter Media ID to borrow: ");
                    int borrowId = int.Parse(Console.ReadLine());
                    if (library.BorrowMedia(borrowId))
                        Console.WriteLine("Media borrowed successfully.");
                    else
                        Console.WriteLine("Media not found or already borrowed.");
                    break;

                case "5":
                    Console.Write("Enter Media ID to return: ");
                    int returnId = int.Parse(Console.ReadLine());
                    if (library.ReturnMedia(returnId))
                        Console.WriteLine("Media returned successfully.");
                    else
                        Console.WriteLine("Media not found.");
                    break;

                case "6":
                    Console.WriteLine("---All Media in the library---");
                    library.DisplayAllMedia();
                    break;

                case "7":
                    Console.Write("Enter Borrower Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Address: ");
                    string address = Console.ReadLine();

                    Console.Write("Enter Phone Number: ");
                    string phoneNo = Console.ReadLine();

                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();

                    var borrower = library.AddBorrower(name, address, phoneNo, email);
                    Console.WriteLine($"Borrower added with ID {borrower.Id} successfully.");
                    break;

                case "8":
                    Console.WriteLine("---All Borrowers in the library---");
                    library.DisplayAllBorrowers();
                    break;

                case "9":
                    running = false;
                    Console.WriteLine("Exiting the library system. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        }
    }
}