using LibraryManagment.Models;

namespace LibraryManagment.Interfaces;

public interface IBookRepository
{
    void Add(Book book);
    void Delete(Guid id);
    void ChangeStatus(Guid id);

    bool BookExists(Guid id);
    bool IsDuplicatedBook(string title, string author);

    IEnumerable<Book> GetAll();
    IEnumerable<Book> GetAllAvaliable();
    IEnumerable<Book> SearchByTitle(string title);
    IEnumerable<Book> SearchByAuthor(string author);
}
