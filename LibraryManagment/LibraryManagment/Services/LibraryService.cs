using LibraryManagment.Interfaces;
using LibraryManagment.Mappings;
using LibraryManagment.Models;
using LibraryManagment.Models.dto;

namespace LibraryManagment.Services;

public class LibraryService : ILibraryService
{
    private readonly IBookRepository bookRepository;

    public LibraryService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public async Task<Result> AddAsync(CreateBookModel model)
    {
        var titleValidation = ValidateTitle(model.Title);
        if (!titleValidation.isValid)
            return Result.Fail(titleValidation.Error);

        var authorValidaiton = ValidateAuthor(model.Author);
        if (!authorValidaiton.isValid)
            return Result.Fail(authorValidaiton.Error);

        var datePublishedValidaiton = ValidateDatePublished(model.DatePublished);
        if (!datePublishedValidaiton.isValid)
            return Result.Fail(datePublishedValidaiton.Error);

        if (bookRepository.IsDuplicatedBook(model.Title, model.Author))
            return Result.Fail("This book already exists");

        var newBook = new Book
        {
            Title = model.Title,
            Author = model.Author,
            DatePublished = model.DatePublished
        };

        await bookRepository.AddAsync(newBook);
        return Result.Success("The book added successful!");
    }

    public async Task<Result> ChangeStatusAsync(Guid id)
    {
        var bookExistsResult = BookExists(id);
        if (!bookExistsResult.isExists)
            return Result.Fail(bookExistsResult.Error);

        await bookRepository.ChangeStatusAsync(id);
        return Result.Success("The status of book was successfully changed");
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var bookExistsResult = BookExists(id);
        if (!bookExistsResult.isExists)
            return Result.Fail(bookExistsResult.Error);

        await bookRepository.DeleteAsync(id);
        return Result.Success("The book deleted successful!");
    }

    public Result<IEnumerable<BookModel>> GetAll()
    {
        var allBooks = bookRepository.GetAll();
        if (!allBooks.Any())
            return Result<IEnumerable<BookModel>>.Fail("No books found.");

        return Result<IEnumerable<BookModel>>.Success("The books was found.", allBooks.ToBookModels());
    }

    public Result<IEnumerable<BookModel>> GetAllAvaliable()
    {
        var avaliableBooks = bookRepository.GetAllAvaliable();
        if (!avaliableBooks.Any())
            return Result<IEnumerable<BookModel>>.Fail("No books avaliable.");

        return Result<IEnumerable<BookModel>>.Success("The avaliable books was found.", avaliableBooks.ToBookModels());
    }

    public Result<IEnumerable<BookModel>> SearchByAuthor(string author)
    {
        var authorValidaiton = ValidateAuthor(author);
        if (!authorValidaiton.isValid)
            return Result<IEnumerable<BookModel>>.Fail(authorValidaiton.Error);

        var books = bookRepository.SearchByAuthor(author);
        if (!books.Any())
            return Result<IEnumerable<BookModel>>.Fail("No books found for this author.");

        return Result<IEnumerable<BookModel>>.Success("The books was found!", books.ToBookModels());
    }

    public Result<IEnumerable<BookModel>> SearchByTitle(string title)
    {
        var titleValidation = ValidateTitle(title);
        if (!titleValidation.isValid)
            return Result<IEnumerable<BookModel>>.Fail(titleValidation.Error);

        var books = bookRepository.SearchByTitle(title);
        if (!books.Any())
            return Result<IEnumerable<BookModel>>.Fail("No books found for this title.");

        return Result<IEnumerable<BookModel>>.Success("The books was found!", books.ToBookModels());
    }

    private (bool isValid, string? Error) ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return (false, "Book title cannot be empty.");

        if (title.Length < 2 || title.Length > 100)
            return (false, "Book title must be between 2 and 100 characters.");

        return (true, null);
    }

    private (bool isValid, string? Error) ValidateAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
            return (false, "Book author cannot be empty.");

        if (author.Length < 2 || author.Length > 50)
            return (false, "Book author must be between 2 and 50 characters.");

        return (true, null);
    }

    private (bool isValid, string? Error) ValidateDatePublished(DateOnly datePublished)
    {
        if (datePublished > DateOnly.FromDateTime(DateTime.Now))
            return (false, "Book date cannot be in the future.");

        return (true, null);
    }

    private (bool isExists, string? Error) BookExists(Guid id)
    {
        if (!bookRepository.BookExists(id))
            return (false, "Book with the given Id was not found.");

        return (true, null);
    }
}
