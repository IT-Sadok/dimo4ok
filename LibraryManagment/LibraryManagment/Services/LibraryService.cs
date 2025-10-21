using LibraryManagment.Interfaces;
using LibraryManagment.Models;
using LibraryManagment.Models.dto;

namespace LibraryManagment.Services;

public class LibraryService : ILibraryService
{
    private readonly ILibraryManager libraryManager;

    public LibraryService(ILibraryManager libraryManager)
    {
        this.libraryManager = libraryManager;
    }

    private void ReturnError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.WriteLine();
        Console.ResetColor();
    }

    private void ReturnSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.WriteLine();
        Console.ResetColor();
    }

    private bool ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            ReturnError("Book title cannot be empty.");
            return false;
        }

        if (title.Length < 2 || title.Length > 100)
        {
            ReturnError("Book title must be between 2 and 100 characters.");
            return false;
        }

        return true;
    }

    private bool ValidateAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            ReturnError("Book author cannot be empty.");
            return false;
        }

        if (author.Length < 2 || author.Length > 50)
        {
            ReturnError("Book author must be between 2 and 50 characters.");
            return false;
        }

        return true;
    }

    private bool ValidateDate(DateOnly date)
    {
        if (date > DateOnly.FromDateTime(DateTime.Now))
        {
            ReturnError("Book date cannot be in the future.");
            return false;
        }

        return true;
    }

    private bool BookExists(Guid id)
    {
        if (!((LibraryManager)libraryManager).BookExists(id))
        {
            ReturnError("Book with the given Id was not found.");
            return false;
        }

        return true;
    }

    public void Add(CreateBookDTO dto)
    {
        if (!ValidateTitle(dto.Title) || !ValidateAuthor(dto.Author) || !ValidateDate(dto.Date))
            return;

        if (((LibraryManager)libraryManager).IsDuplicatedBook(dto.Title, dto.Author))
        {
            ReturnError("This book already exists");
            return;
        }

        var newBook = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Date = dto.Date
        };

        libraryManager.Add(newBook);
        ReturnSuccess("The book added successful!");
    }

    public void ChangeStatus(Guid id)
    {
        if (!BookExists(id))
            return;

        libraryManager.ChangeStatus(id);
        ReturnSuccess("The status of book was successfully changed");
    }

    public void Delete(Guid id)
    {
        if (!BookExists(id))
            return;

        libraryManager.Delete(id);
        ReturnSuccess("The book deleted successful!");
    }

    public IEnumerable<BookDto> GetAll()
    {
        var allBooks = libraryManager.GetAll();
        if (!allBooks.Any())
            ReturnError("No books found.");
        else
            ReturnSuccess("The books was found.");

        return allBooks.Select(x => new BookDto
        {
            Id = x.Id,
            Title = x.Title,
            Author = x.Author,
            Date = x.Date,
            BookStatus = x.BookStatus
        });
    }

    public IEnumerable<BookDto> GetAllAvaliable()
    {
        var avaliableBooks = libraryManager.GetAllAvaliable();
        if (!avaliableBooks.Any())
            ReturnError("No books avaliable.");
        else
            ReturnSuccess("The avaliable books was found.");

        return avaliableBooks.Select(x => new BookDto
        {
            Id = x.Id,
            Title = x.Title,
            Author = x.Author,
            Date = x.Date,
            BookStatus = x.BookStatus
        });
    }

    public IEnumerable<BookDto> SearchByAuthor(string author)
    {
        if (!ValidateAuthor(author))
            return null;

        var books = libraryManager.SearchByAuthor(author);

        if (!books.Any())
            ReturnError("No books found for this author.");
        else
            ReturnSuccess("The books was found!");

        return books.Select(x => new BookDto
        {
            Id = x.Id,
            Title = x.Title,
            Author = x.Author,
            Date = x.Date,
            BookStatus = x.BookStatus
        });
    }

    public IEnumerable<BookDto> SearchByTitle(string title)
    {
        if (!ValidateTitle(title))
            return null;

        var books = libraryManager.SearchByTitle(title);

        if (!books.Any())
            ReturnError("No books found for this title.");
        else
            ReturnSuccess("The books was found!");

        return books.Select(x => new BookDto
        {
            Id = x.Id,
            Title = x.Title,
            Author = x.Author,
            Date = x.Date,
            BookStatus = x.BookStatus
        });
    }
}
