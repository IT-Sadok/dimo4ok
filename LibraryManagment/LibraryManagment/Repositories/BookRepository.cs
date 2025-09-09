using LibraryManagment.Interfaces;
using LibraryManagment.Models;
using System.Text.Json;

namespace LibraryManagment.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string filePath;
        private readonly List<Book> books;

        public BookRepository(string filePath)
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

        private void SaveToFile(List<Book> books)
        {
            var booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, booksJson);
        }

        private Book GetById(Guid id)
        {
            return books.First(x => x.Id == id);
        }

        public bool BookExists(Guid id)
        {
            return books.Exists(x => x.Id == id);
        }

        public bool IsDuplicatedBook(string title, string author)
        {
            return books.Any(x => x.Title == title && x.Author == author);
        }

        public void Add(Book book)
        {
            books.Add(book);
            SaveToFile(books);
        }

        public void ChangeStatus(Guid id)
        {
            var bookForChange = GetById(id);

            if(bookForChange.Available == true)
                bookForChange.Available = false;
            else
                bookForChange.Available = true;

            SaveToFile(books);
        }

        public void Delete(Guid id)
        {
            var bookToDelete = GetById(id);
            books.Remove(bookToDelete);
            SaveToFile(books);
        }

        public IEnumerable<Book> GetAll()
        {
            return books;
        }

        public IEnumerable<Book> GetAllAvaliable()
        {
            return books.Where(x => x.Available == true);
        }

        public IEnumerable<Book> SearchByAuthor(string author)
        {
            return books.Where(x => x.Author == author);
        }

        public IEnumerable<Book> SearchByTitle(string title)
        {
            return books.Where(x => x.Title == title);
        }
    }
}
