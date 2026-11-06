using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static string Ask(string prompt)
    {
        Console.Write(prompt);
        return (Console.ReadLine() ?? "").Trim();
    }

    static int AskInt(string prompt)
    {
        while (true)
        {
            if (int.TryParse(Ask(prompt), out int v)) return v;
            Console.WriteLine("Введите целое число.");
        }
    }

    static void PrintFound(List<Book> found)
    {
        if (found.Count == 0) { Console.WriteLine("Ничего не найдено."); return; }
        foreach (var b in found) { b.DisplayInfo(); Console.WriteLine(); }
    }

    static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Library lib = new Library();
        lib.ShowWelcomeMessage();
        Library.ShowLibraryName();

        bool running = true;
        while (running)
        {
            Library.ShowMenu();
            string choice = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    string title = Ask("Название: ");
                    string author = Ask("Автор: ");
                    int year = AskInt("Год: ");
                    lib.AddBook(title, author, year);
                    Console.WriteLine();
                    Console.WriteLine("Книга успешно добавлена");
                    break;

                case "2":
                    lib.SortBooksByYear();
                    lib.ShowBooks();
                    break;

                case "3":
                    Console.WriteLine("Искать по: 1 - названию, 2 - автору, 3 - году");
                    string mode = Ask("Выбор: ");
                    if (mode == "1") PrintFound(lib.SearchBook(Ask("Название: "), true));
                    else if (mode == "2") PrintFound(lib.SearchBook(Ask("Автор: "), false));
                    else if (mode == "3") PrintFound(lib.SearchBook(AskInt("Год: ")));
                    else Console.WriteLine("Неверный выбор.");
                    break;

                case "4":
                    Console.WriteLine("Добавить: 1 - только имя, 2 - имя и фамилия, 3 - имя и телефон");
                    string m = Ask("Выбор: ");
                    string name = Ask("Имя: ");
                    if (m == "1") lib.AddReader(name);
                    else if (m == "2") lib.AddReader(name, Ask("Фамилия: "));
                    else if (m == "3") lib.AddReader(name, Ask("Телефон: "), true);
                    else { Console.WriteLine("Неверный выбор."); break; }
                    Console.WriteLine("Читатель зарегистрирован.");
                    break;

                case "5":
                    lib.ShowReaders();
                    break;

                case "6":
                    lib.BorrowBook(Ask("Название книги: "), Ask("Имя читателя: "));
                    break;

                case "7":
                    lib.ReturnBook(Ask("Название книги: "));
                    break;

                case "8":
                    lib.ShowStatistics();
                    Console.WriteLine($"Книг в списке (рекурсивный подсчёт): {lib.CountBooksRecursive()}");
                    Console.WriteLine($"Всего добавлено за сессию (TotalBooks): {Library.TotalBooks}");
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("До свидания!");
                    break;

                default:
                    Console.WriteLine("Неверный пункт меню, попробуйте снова.");
                    break;
            }
        }
    }
}