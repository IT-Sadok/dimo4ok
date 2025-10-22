using LibraryManagment.Models;
using LibraryManagment.Models.dto;
using LibraryManagment.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        var createBookModel = new CreateBookModel();

        Console.Write("Enter the book title: ");
        createBookModel.Title = Console.ReadLine();

        Console.Write("Enter the book author: ");
        createBookModel.Author = Console.ReadLine();

        Console.Write("Enter the book year: ");
        if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly year))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid year format.\n");
            Console.ResetColor();
            return;
        }

        createBookModel.DatePublished = year;

        Console.WriteLine();

        var result = libraryService.Add(createBookModel);
        DisplayResultMessage(result.IsSuccess, result.Message);
    }

    void DeleteBook()
    {
        Console.Write("Enter the book id: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid id format.\n");
            Console.ResetColor();
            return;
        }

        var result = libraryService.Delete(id);
        DisplayResultMessage(result.IsSuccess, result.Message);
    }

    void DisplayAllBooks()
    {
        var result = libraryService.GetAll();

        DisplayResultMessage(result.IsSuccess, result.Message);

        if(result.IsSuccess)
        {
            Console.WriteLine("The list of books:");
            DisplayBooks(result.Data);
        }
    }

    void DisplayBooksByAuthor()
    {
        Console.Write("Enter the book author: ");
        string author = Console.ReadLine();

        var result = libraryService.SearchByAuthor(author);

        DisplayResultMessage(result.IsSuccess, result.Message);

        if(result.IsSuccess)
        {
            Console.WriteLine("The list of books by author:");
            DisplayBooks(result.Data);
        }
    }

    void DisplayBooksByTitle()
    {
        Console.Write("Enter the book title: ");
        string title = Console.ReadLine();

        var result = libraryService.SearchByTitle(title);

        DisplayResultMessage(result.IsSuccess, result.Message);

        if (result.IsSuccess)
        {
            Console.WriteLine("The list of books by title:");
            DisplayBooks(result.Data);
        }
    }

    void DisplayAvaliableBooks()
    {
        var result = libraryService.GetAllAvaliable();

        DisplayResultMessage(result.IsSuccess, result.Message);

        if (result.IsSuccess)
        {
            Console.WriteLine("The list of available books:");
            DisplayBooks(result.Data);
        }
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

        var result = libraryService.ChangeStatus(id);

        DisplayResultMessage(result.IsSuccess, result.Message);

        if(result.IsSuccess)
            DisplayAllBooks();
    }

    void DisplayBooks(IEnumerable<BookModel> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void DisplayResultMessage(bool success, string message)
    {
        if (!success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.WriteLine();
        Console.ResetColor();
    }
}
