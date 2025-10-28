using LibraryManagment.Interfaces;
using LibraryManagment.Models.dto;

namespace LibraryManagment.Presentation;

public class LibraryConsole
{
    private readonly ILibraryService libraryService;

    public LibraryConsole(ILibraryService libraryService)
    {
        this.libraryService = libraryService;
    }

    public async Task RunAsync()
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
                    await AddBookAsync();
                    DisplayAllBooks();
                    Console.WriteLine();
                    break;

                case "2":
                    Console.WriteLine();
                    await DeleteBookAsync();
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
                    await ChangeBookStatusAsync();
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

    private async Task AddBookAsync()
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

        var result = await libraryService.AddAsync(createBookModel);
        DisplayResultMessage(result.IsSuccess, result.Message);
    }

    private async Task DeleteBookAsync()
    {
        Console.Write("Enter the book id: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid id format.\n");
            Console.ResetColor();
            return;
        }

        var result = await libraryService.DeleteAsync(id);
        DisplayResultMessage(result.IsSuccess, result.Message);
    }

    private void DisplayAllBooks()
    {
        var result = libraryService.GetAll();

        DisplayResultMessage(result.IsSuccess, result.Message);

        if (result.IsSuccess)
        {
            Console.WriteLine("The list of books:");
            DisplayBooks(result.Data);
        }
    }

    private void DisplayBooksByAuthor()
    {
        Console.Write("Enter the book author: ");
        string author = Console.ReadLine();

        var result = libraryService.SearchByAuthor(author);

        DisplayResultMessage(result.IsSuccess, result.Message);

        if (result.IsSuccess)
        {
            Console.WriteLine("The list of books by author:");
            DisplayBooks(result.Data);
        }
    }

    private void DisplayBooksByTitle()
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

    private void DisplayAvaliableBooks()
    {
        var result = libraryService.GetAllAvaliable();

        DisplayResultMessage(result.IsSuccess, result.Message);

        if (result.IsSuccess)
        {
            Console.WriteLine("The list of available books:");
            DisplayBooks(result.Data);
        }
    }

    private async Task ChangeBookStatusAsync()
    {
        Console.Write("Enter a book id that you want to borrow/return: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid year format.\n");
            Console.ResetColor();
            return;
        }

        var result = await libraryService.ChangeStatusAsync(id);

        DisplayResultMessage(result.IsSuccess, result.Message);

        if (result.IsSuccess)
            DisplayAllBooks();
    }

    private void DisplayBooks(IEnumerable<BookModel> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    private void DisplayResultMessage(bool success, string message)
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
