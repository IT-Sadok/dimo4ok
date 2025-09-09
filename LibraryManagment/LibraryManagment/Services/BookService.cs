using LibraryManagment.Interfaces;
using LibraryManagment.Models;
using LibraryManagment.Repositories;

namespace LibraryManagment.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;

        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }

        private void ReturnError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.WriteLine();
            Console.ResetColor();
        }

        private void ReturnSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        private bool ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                ReturnError("Book title cannot be empty.");
                return false;
            }

            if (title.Length < 2 || title.Length > 100)
            {
                ReturnError("Book title must be between 2 and 100 characters.");
                return false;
            }

            return true;
        }

        private bool ValidateAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                ReturnError("Book author cannot be empty.");
                return false;
            }

            if (author.Length < 2 || author.Length > 50)
            {
                ReturnError("Book author must be between 2 and 50 characters.");
                return false;
            }

            return true;
        }

        private bool ValidateYear(DateOnly year)
        {
            if (year > DateOnly.FromDateTime(DateTime.Now))
            {
                ReturnError("Book year cannot be in the future.");
                return false;
            }

            return true;
        }

        private bool ValidateData(string title, string author, DateOnly year)
        {
            if (!ValidateTitle(title) || !ValidateAuthor(author) || !ValidateYear(year))
                return false;

            return true;
        }

        private bool BookExists(Guid id)
        {
            if (!((BookRepository)repository).BookExists(id))
            {
                ReturnError("Book with the given Id was not found.");
                return false;
            }

            return true;
        }

        public void Add(string title, string author, DateOnly year)
        {
            if (!ValidateData(title, author, year))
                return;

            if(((BookRepository)repository).IsDuplicatedBook(title, author))
            {
                ReturnError("This book already exists");
                return;
            }

            var newBook = new Book
            {
                Title = title,
                Author = author,
                Year = year
            };

            repository.Add(newBook);
            ReturnSuccess("The book added successful!");
        }

        public void ChangeStatus(Guid id)
        {
            if (!BookExists(id))
                return;

            repository.ChangeStatus(id);
            ReturnSuccess("The status of book was successfully changed");
        }

        public void Delete(Guid id)
        {
            if (!BookExists(id))
                return;

            repository.Delete(id);
            ReturnSuccess("The book deleted successful!");
        }

        public IEnumerable<Book> GetAll()
        {
            var allBooks = repository.GetAll();
            if (!allBooks.Any())
                ReturnError("No books found.");
            else
                ReturnSuccess("The books was found.");

            return allBooks;
        }

        public IEnumerable<Book> GetAllAvaliable()
        {
            var avaliableBooks = repository.GetAllAvaliable();
            if (!avaliableBooks.Any())
                ReturnError("No books avaliable.");
            else
                ReturnSuccess("The avaliable books was found.");

            return avaliableBooks;
        }

        public IEnumerable<Book> SearchByAuthor(string author)
        {
            if (!ValidateAuthor(author))
                return null;

            var books = repository.SearchByAuthor(author);

            if (!books.Any())
                ReturnError("No books found for this author.");
            else
                ReturnSuccess("The books was found!");

            return books;
        }

        public IEnumerable<Book> SearchByTitle(string title)
        {
            if (!ValidateTitle(title))
                return null;

            var books = repository.SearchByTitle(title);

            if (!books.Any())
                ReturnError("No books found for this title.");
            else
                ReturnSuccess("The books was found!");

            return books;
        }
    }
}
