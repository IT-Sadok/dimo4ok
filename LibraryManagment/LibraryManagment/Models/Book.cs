namespace LibraryManagment.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateOnly Date { get; set; }
    public BookStatus BookStatus { get; set; } = BookStatus.Available;

    //public override string ToString()
    //{
    //    return $"Id: {Id}, Title: {Title}, Author: {Author}, Year: {Date}, Available: {BookStatus}";
    //}
}
