using LibraryManagment.Models;
using LibraryManagment.Models.dto;

namespace LibraryManagment.Interfaces;

public interface ILibraryService
{
    void Add(CreateBookDTO dto);
    void Delete(Guid id);
    void ChangeStatus(Guid id);

    IEnumerable<BookDto> SearchByTitle(string title);
    IEnumerable<BookDto> SearchByAuthor(string author);
    IEnumerable<BookDto> GetAll();
    IEnumerable<BookDto> GetAllAvaliable();
}
