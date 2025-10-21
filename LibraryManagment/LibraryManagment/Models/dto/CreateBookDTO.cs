namespace LibraryManagment.Models.dto;

public class CreateBookDTO
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateOnly Date { get; set; }
}
