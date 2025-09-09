using LibraryManagment.Models;

namespace LibraryManagment.Interfaces
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Delete(Guid id);
        void ChangeStatus(Guid id);

        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetAllAvaliable();
        IEnumerable<Book> SearchByTitle(string title);
        IEnumerable<Book> SearchByAuthor(string author);
    }
}
