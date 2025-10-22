namespace LibraryManagment.Models.dto;

public class CreateBookModel
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateOnly DatePublished { get; set; }
}
