using LibraryManagment.Models;
using LibraryManagment.Models.dto;

namespace LibraryManagment.Interfaces;

public interface ILibraryService
{
    Result Add(CreateBookModel dto);
    Result Delete(Guid id);
    Result ChangeStatus(Guid id);

    Result<IEnumerable<BookModel>> SearchByTitle(string title);
    Result<IEnumerable<BookModel>> SearchByAuthor(string author);
    Result<IEnumerable<BookModel>> GetAll();
    Result<IEnumerable<BookModel>> GetAllAvaliable();
}
