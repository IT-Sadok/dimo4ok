using LibraryManagment.Models;
using LibraryManagment.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Mappings;

public static class BookMappingExtensions
{
    public static BookModel ToBookModel(this Book book)
    {
        return new BookModel
            (
                book.Id,
                book.Title,
                book.Author,
                book.DatePublished,
                book.BookStatus
            );
    }

    public static IEnumerable<BookModel> ToBookModels(this IEnumerable<Book> books) 
        => books.Select(x => x.ToBookModel());
}