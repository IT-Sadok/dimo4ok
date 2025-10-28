using LibraryManagment.Models;

namespace LibraryManagment.Interfaces;

public interface IDataStorage
{
    Task SaveToFileAsync(IEnumerable<Book> books);
    IEnumerable<Book> GetAll();
}
