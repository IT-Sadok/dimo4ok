using LibraryManagment.Models;

namespace LibraryManagment.Interfaces;

public interface IDataRepository
{
    void SaveToFile(IEnumerable<Book> books);
    IEnumerable<Book> GetAll();
}
