using LibraryManagment.Presentation;
using LibraryManagment.Repositories;
using LibraryManagment.Services;

var libraryRepository = new JsonRepository(@"D:\Answer\library.json");
var libraryManager = new LibraryManager(libraryRepository);
var libraryService = new LibraryService(libraryManager);
var consoleApp = new LibraryConsole(libraryService);

consoleApp.Run();