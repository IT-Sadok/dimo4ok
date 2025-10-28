using LibraryManagment.Interfaces;
using LibraryManagment.Models;

namespace LibraryManagment.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDataStorage repository;
    private List<Book> books;
    private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);

    public BookRepository(IDataStorage repository)
    {
        this.repository = repository;
        books = repository.GetAll().ToList();
    }

    public bool BookExists(Guid id)
    {
        return books.Exists(x => x.Id == id);
    }

    public bool IsDuplicatedBook(string title, string author)
    {
        return books.Any(x => x.Title == title && x.Author == author);
    }

    public async Task AddAsync(Book book)
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            books.Add(book);
            await repository.SaveToFileAsync(books);
        }
        finally
        {
            semaphoreSlim.Release(); 
        }
    }

    public async Task ChangeStatusAsync(Guid id)
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            var bookForChange = GetById(id);

            if (bookForChange.BookStatus == BookStatus.Available)
                bookForChange.BookStatus = BookStatus.Borrowed;
            else
                bookForChange.BookStatus = BookStatus.Available;

            await repository.SaveToFileAsync(books);
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            var bookToDelete = GetById(id);
            books.Remove(bookToDelete);
            await repository.SaveToFileAsync(books);
        }
        finally
        {
            semaphoreSlim.Release();
        }
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
    private Book? GetById(Guid id)
    {
        return books.FirstOrDefault(x => x.Id == id);
    }
}
