using LibraryManagment.Models;
using LibraryManagment.Models.dto;

namespace LibraryManagment.Interfaces;

public interface ILibraryService
{
    Task<Result> AddAsync(CreateBookModel dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result> ChangeStatusAsync(Guid id);

    Result<IEnumerable<BookModel>> SearchByTitle(string title);
    Result<IEnumerable<BookModel>> SearchByAuthor(string author);
    Result<IEnumerable<BookModel>> GetAll();
    Result<IEnumerable<BookModel>> GetAllAvaliable();
}
