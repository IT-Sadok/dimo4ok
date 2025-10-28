using LibraryManagment.Presentation;
using LibraryManagment.Repositories;
using LibraryManagment.Services;

var libraryRepository = new JsonRepository(@"D:\Answer\library.json");
var bookRepository = new BookRepository(libraryRepository);
var libraryService = new LibraryService(bookRepository);
var simulationApp = new LibrarySimulator(libraryService, bookRepository);
//var consoleApp = new LibraryConsole(libraryService);

//await consoleApp.RunAsync();

await simulationApp.RunAsync();
