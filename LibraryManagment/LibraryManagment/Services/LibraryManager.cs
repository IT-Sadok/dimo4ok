using LibraryManagment.Interfaces;
using LibraryManagment.Models;

namespace LibraryManagment.Services;

public class LibraryManager : ILibraryManager
{
    private readonly IDataRepository repository;
    private List<Book> books;

    public LibraryManager(IDataRepository repository)
    {
        this.repository = repository;
        books = repository.GetAll().ToList();
    }

    private Book GetById(Guid id)
    {
        return books.First(x => x.Id == id);
    }

    public bool BookExists(Guid id)
    {
        return books.Exists(x => x.Id == id);
    }

    public bool IsDuplicatedBook(string title, string author)
    {
        return books.Any(x => x.Title == title && x.Author == author);
    }

    public void Add(Book book)
    {
        books.Add(book);
        repository.SaveToFile(books);
    }

    public void ChangeStatus(Guid id)
    {
        var bookForChange = GetById(id);

        if (bookForChange.BookStatus == BookStatus.Available)
            bookForChange.BookStatus = BookStatus.Borrowed;
        else
            bookForChange.BookStatus = BookStatus.Available;

        repository.SaveToFile(books);
    }

    public void Delete(Guid id)
    {
        var bookToDelete = GetById(id);
        books.Remove(bookToDelete);
        repository.SaveToFile(books);
    }

    public IEnumerable<Book> GetAll()
    {
        return books;
    }

    public IEnumerable<Book> GetAllAvaliable()
    {
        return books.Where(x => x.BookStatus == BookStatus.Available);
    }

    public IEnumerable<Book> SearchByAuthor(string author)
    {
        return books.Where(x => x.Author == author);
    }

    public IEnumerable<Book> SearchByTitle(string title)
    {
        return books.Where(x => x.Title == title);
    }
}
