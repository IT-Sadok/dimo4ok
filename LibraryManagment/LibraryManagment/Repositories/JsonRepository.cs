using LibraryManagment.Interfaces;
using LibraryManagment.Models;
using System.Text.Json;

namespace LibraryManagment.Repositories;

public class JsonRepository : IDataStorage
{
    private readonly string filePath;
    private readonly List<Book> books;

    public JsonRepository(string filePath)
    {
        this.filePath = filePath;
        books = ReadFile(filePath);
    }

    private List<Book> ReadFile(string filePath)
    {
        if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
        {
            File.WriteAllText(filePath, "[]");
            return new List<Book>();
        }
        else
        {
            var booksJson = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Book>>(booksJson);
        }
    }

    public void SaveToFile(IEnumerable<Book> books)
    {
        var booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, booksJson);
    }

    public IEnumerable<Book> GetAll()
    {
        return books;
    }
}
