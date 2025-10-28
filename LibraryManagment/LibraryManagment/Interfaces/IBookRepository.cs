using LibraryManagment.Models;

namespace LibraryManagment.Interfaces;

public interface IBookRepository
{
    Task AddAsync(Book book);
    Task DeleteAsync(Guid id);
    Task ChangeStatusAsync(Guid id);

    bool BookExists(Guid id);
    bool IsDuplicatedBook(string title, string author);

    IEnumerable<Book> GetAll();
    IEnumerable<Book> GetAllAvaliable();
    IEnumerable<Book> SearchByTitle(string title);
    IEnumerable<Book> SearchByAuthor(string author);
}
