using LibraryManagment.Models;

namespace LibraryManagment.Interfaces;

public interface IDataStorage
{
    void SaveToFile(IEnumerable<Book> books);
    IEnumerable<Book> GetAll();
}
