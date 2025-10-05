using Microsoft.EntityFrameworkCore;
using ConsoleApp38.Entities;
using BookStoreApp;
namespace FinalEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LibraryContext())
            {
                db.Database.EnsureCreated();
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("BOOK MANAGEMENT MENU");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("1. Add new book");
                    Console.WriteLine("2. Show all books");
                    Console.WriteLine("3. Search books by genre");
                    Console.WriteLine("4. Update book price");
                    Console.WriteLine("5. Delete book");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nChoose an option: ");
                    string choice = Console.ReadLine()!;

                    switch (choice)
                    {
                        case "1":
                            AddBook(db);
                            break;
                        case "2":
                            ShowAllBooks(db);
                            break;
                        case "3":
                            SearchBooksByGenre(db);
                            break;
                        case "4":
                            UpdateBookPrice(db);
                            break;
                        case "5":
                            DeleteBook(db);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }
                    if (!exit)
                    {
                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                    }
                }
            }
        }
        static void AddBook(LibraryContext db)
        {
            Console.Clear();
            Console.WriteLine("Add new book\n");

            Console.Write("Enter title : ");
            string title = Console.ReadLine()!;

            Console.Write("Enter pages: ");
            int pages = int.Parse(Console.ReadLine()!);

            Console.Write("Enter year : ");
            int year = int.Parse(Console.ReadLine()!);

            Console.Write("Enter price (UAH): ");
            decimal price = decimal.Parse(Console.ReadLine()!);

            Console.Write("Enter author name : ");
            string authorName = Console.ReadLine()!;

            Console.Write("Enter publisher name : ");
            string publisherName = Console.ReadLine()!;

            Console.Write("Enter genre: ");
            string genreName = Console.ReadLine()!;

            var author = db.Authors.FirstOrDefault(a => a.FullName == authorName)
                         ?? new Author { FullName = authorName };
            var publisher = db.Publishers.FirstOrDefault(p => p.Name == publisherName)
                            ?? new Publisher { Name = publisherName };
            var genre = db.Genres.FirstOrDefault(g => g.Name == genreName)
                        ?? new Genre { Name = genreName };

            var book = new Book
            {
                Title = title,
                Pages = pages,
                Year = year,
                Price = price,
                Author = author,
                Publisher = publisher,
                Genre = genre
            };

            db.Books.Add(book);
            db.SaveChanges();

            Console.WriteLine("\nBook successfully added!");
        }
        static void ShowAllBooks(LibraryContext db)
        {
            Console.Clear();
            Console.WriteLine("Book Catalog\n");

            var books = db.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Genre)
                .ToList();

            if (books.Count == 0)
            {
                Console.WriteLine("No books in catalog yet.");
                return;
            }

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Id}. {b.Title} - {b.Author.FullName}, {b.Publisher.Name}, {b.Genre.Name}, {b.Year} [{b.Price}₴]");
            }
        }
        static void SearchBooksByGenre(LibraryContext db)
        {
            Console.Clear();
            Console.WriteLine("Search Books by Genre\n");

            Console.Write("Enter genre: ");
            string genreName = Console.ReadLine()!;

            var books = db.Books
                .Include(b => b.Genre)
                .Where(b => b.Genre.Name == genreName)
                .ToList();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found for this genre.");
                return;
            }

            Console.WriteLine($"\nBooks in genre '{genreName}':");
            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} ({b.Year}) - {b.Price}₴");
            }
        }
        static void UpdateBookPrice(LibraryContext db)
        {
            Console.Clear();
            Console.WriteLine("Update Book Price\n");

            ShowAllBooks(db);

            Console.Write("\nEnter book ID to update: ");
            int id = int.Parse(Console.ReadLine()!);

            var book = db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found!");
                return;
            }

            Console.Write($"Current price: {book.Price} Enter new price: ");
            decimal newPrice = decimal.Parse(Console.ReadLine()!);

            book.Price = newPrice;
            db.SaveChanges();

            Console.WriteLine("Price successfully updated!");
        }
        static void DeleteBook(LibraryContext db)
        {
            Console.Clear();
            Console.WriteLine("Delete Book\n");

            ShowAllBooks(db);

            Console.Write("Enter book ID to delete: ");
            int id = int.Parse(Console.ReadLine()!);

            var book = db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found!");
                return;
            }
            db.Books.Remove(book);
            db.SaveChanges();
            Console.WriteLine($"Book '{book.Title}' deleted!");
        }
    }
}
