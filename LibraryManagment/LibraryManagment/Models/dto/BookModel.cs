namespace LibraryManagment.Models.dto;

public record BookModel(
    Guid Id,
    string Title,
    string Author,
    DateOnly DatePublished,
    BookStatus BookStatus
);
