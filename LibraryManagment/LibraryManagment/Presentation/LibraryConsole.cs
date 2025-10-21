using LibraryManagment.Models.dto;
using LibraryManagment.Services;

namespace LibraryManagment.Presentation;

public class LibraryConsole
{
    private readonly LibraryService libraryService;

    public LibraryConsole(LibraryService libraryService)
    {
        this.libraryService = libraryService;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n LibraryManagment");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Delete a book");
            Console.WriteLine("3. Search by title");
            Console.WriteLine("4. Search by author");
            Console.WriteLine("5. All avaliable books");
            Console.WriteLine("6. All books");
            Console.WriteLine("7. Borrow or return a book");
            Console.WriteLine("0. Exit");
            Console.Write("Select an action: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine();
                    AddBook();
                    DisplayAllBooks();
                    Console.WriteLine();
                    break;

                case "2":
                    Console.WriteLine();
                    DeleteBook();
                    DisplayAllBooks();
                    Console.WriteLine();
                    break;

                case "3":
                    Console.WriteLine();
                    DisplayBooksByTitle();
                    Console.WriteLine();
                    break;

                case "4":
                    Console.WriteLine();
                    DisplayBooksByAuthor();
                    Console.WriteLine();
                    break;

                case "5":
                    Console.WriteLine();
                    DisplayAvaliableBooks();
                    Console.WriteLine();
                    break;

                case "6":
                    Console.WriteLine();
                    DisplayAllBooks();
                    Console.WriteLine();
                    break;

                case "7":
                    Console.WriteLine();
                    ChangeBookStatus();
                    Console.WriteLine();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    void AddBook()
    {
        CreateBookDTO createBookDTO = new CreateBookDTO();

        Console.Write("Enter the book title: ");
        createBookDTO.Title = Console.ReadLine();

        Console.Write("Enter the book author: ");
        createBookDTO.Author = Console.ReadLine();

        Console.Write("Enter the book year: ");
        if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly year))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid year format.\n");
            Console.ResetColor();
            return;
        }

        createBookDTO.Date = year;

        Console.WriteLine();
        libraryService.Add(createBookDTO);
    }

    void DeleteBook()
    {
        Console.Write("Enter the book id: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid year format.\n");
            Console.ResetColor();
            return;
        }

        libraryService.Delete(id);
    }

    void DisplayBooks(IEnumerable<BookDto> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    void DisplayAllBooks()
    {
        var books = libraryService.GetAll();

        Console.WriteLine("The list of books:");
        DisplayBooks(books);
    }

    void DisplayBooksByAuthor()
    {
        Console.Write("Enter the book author: ");
        string author = Console.ReadLine();

        var booksByAthor = libraryService.SearchByAuthor(author);
        DisplayBooks(booksByAthor);
    }

    void DisplayBooksByTitle()
    {
        Console.Write("Enter the book title: ");
        string title = Console.ReadLine();

        var booksByTitle = libraryService.SearchByTitle(title);
        DisplayBooks(booksByTitle);
    }

    void DisplayAvaliableBooks()
    {
        var availableBooks = libraryService.GetAllAvaliable();

        Console.WriteLine("The list of available books:");
        DisplayBooks(availableBooks);
    }

    void ChangeBookStatus()
    {
        Console.Write("Enter a book id that you want to borrow/return: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid year format.\n");
            Console.ResetColor();
            return;
        }

        libraryService.ChangeStatus(id);
        DisplayAllBooks();
    }
}
