using LibraryManagment.Interfaces;
using LibraryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!repository.BookExists(id))
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

            var newBook = new Book
            {
                Title = title,
                Author = author,
                Year = year
            };

            repository.Add(newBook);
        }

        public void ChangeStatus(Guid id)
        {
            if(!BookExists(id))
                return;

            repository.ChangeStatus(id);
        }

        public void Delete(Guid id)
        {
            if (!repository.BookExists(id))
                return;

            repository.Delete(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<Book> GetAllAvaliable()
        {
            return repository.GetAllAvaliable();
        }

        public IEnumerable<Book> SearchByAuthor(string author)
        {
            if (!ValidateAuthor(author))
                return null;

            var books = repository.SearchByAuthor(author);

            if (!books.Any())
                ReturnError("Books not found");

            return books;
        }

        public IEnumerable<Book> SearchByTitle(string title)
        {
            if (!ValidateTitle(title))
                return null;

            var books = repository.SearchByTitle(title);

            if (!books.Any())
                ReturnError("Books not found");

            return books;
        }
    }
}
