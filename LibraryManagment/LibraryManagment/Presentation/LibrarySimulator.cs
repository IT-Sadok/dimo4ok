using LibraryManagment.Interfaces;
using LibraryManagment.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Presentation;

public class LibrarySimulator
{
    private readonly ILibraryService libraryService;
    private readonly IBookRepository bookRepository;

    public LibrarySimulator(ILibraryService libraryService, IBookRepository bookRepository)
    {
        this.libraryService = libraryService;
        this.bookRepository = bookRepository;
    }

    public async Task RunAsync()
    {
        var tasks = new Task[100];
        var random = new Random();

        for (int i = 0; i < 100; i++)
        {
            tasks[i] = Task.Run(async () =>
            {
                int operation = random.Next(1, 5);
                var bookIds = bookRepository.GetAll().Select(x => x.Id).ToList();

                switch (operation)
                {
                    case 1:
                        var newBookModel = new CreateBookModel
                        {
                            Title = "Title" + random.Next(1000),
                            Author = "Book" + random.Next(1000),
                            DatePublished = DateOnly.FromDateTime(DateTime.UtcNow)
                        };

                        await libraryService.AddAsync(newBookModel);
                        Console.WriteLine($"Added {newBookModel.Title}");
                        break;
                    case 2: 
                        if (bookIds.Count > 0)
                        {
                            var id = bookIds[random.Next(bookIds.Count)];
                            await libraryService.ChangeStatusAsync(id);
                            Console.WriteLine($"Changed status of book {id}");
                        }
                        break;

                    case 3: 
                        if (bookIds.Count > 0)
                        {
                            var id = bookIds[random.Next(bookIds.Count)];
                            await libraryService.DeleteAsync(id);
                            Console.WriteLine($"Deleted book {id}");
                        }
                        break;
                    case 4: 
                        var allBooks = libraryService.GetAll();
                        Console.WriteLine($"GetAll returned {allBooks.Data.Count()} books");
                        break;
                }
            });
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("All tasks completed.");
    }
}
