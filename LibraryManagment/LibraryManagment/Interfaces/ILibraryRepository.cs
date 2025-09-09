using LibraryManagment.Models;

namespace LibraryManagment.Interfaces
{
    public interface ILibraryRepository
    {
        void SaveToFile(IEnumerable<Book> books);
        IEnumerable<Book> GetAll();

    }
}
