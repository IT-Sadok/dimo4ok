namespace LibraryManagment.Models.dto;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateOnly Date { get; set; }
    public BookStatus BookStatus { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Author: {Author}, Year: {Date}, Available: {BookStatus}";
    }
}
