using System;
using System.Collections.Generic;

class Library
{
    public static string LibraryName = "Городская библиотека";
    public static int TotalBooks = 0;

    const int LoanDays = 14;
    const int FinePerDay = 10;

    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();

    // Задачи 2, 11
    public void ShowWelcomeMessage() =>
        Console.WriteLine($"Добро пожаловать в «{LibraryName}»!");

    public static void ShowLibraryName() =>
        Console.WriteLine($"Библиотека: {LibraryName}");

    // Задачи 4, 15
    public void AddBook(string title, string author, int year)
    {
        books.Add(new Book { Title = title, Author = author, Year = year });
        TotalBooks++;
    }

    // Задачи 6, 14
    public int GetBookCount() => books.Count;

    public int GetAvailableBooks()
    {
        int n = 0;
        foreach (var b in books) if (b.IsBookAvailable()) n++;
        return n;
    }

    public static int GetMaxBooks(int first, int second) => first > second ? first : second;

    // Задача 13
    public static int CalculateFine(int overdueDays) =>
        overdueDays > 0 ? overdueDays * FinePerDay : 0;

    // Задача 12
    public void ShowBooks()
    {
        if (books.Count == 0) { Console.WriteLine("В библиотеке пока нет книг."); return; }
        int i = 1;
        foreach (var b in books)
        {
            Console.WriteLine($"--- Книга {i++} ---");
            b.DisplayInfo();
        }
    }

    // Задачи 9, 16
    public Book FindBook(string title)
    {
        foreach (var b in books)
            if (b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)) return b;
        return null;
    }

    public List<Book> SearchBook(string text, bool byTitle)
    {
        var result = new List<Book>();
        foreach (var b in books)
        {
            string field = byTitle ? b.Title : b.Author;
            if (field.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) result.Add(b);
        }
        return result;
    }

    public List<Book> SearchBook(int year)
    {
        var result = new List<Book>();
        foreach (var b in books) if (b.Year == year) result.Add(b);
        return result;
    }

    // Задачи 10, 17
    public bool IsReaderRegistered(string name)
    {
        foreach (var r in readers)
            if (r.FullName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                r.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    public void AddReader(string name) =>
        readers.Add(new Reader { Name = name });

    public void AddReader(string name, string surname) =>
        readers.Add(new Reader { Name = name, Surname = surname });

    public void AddReader(string name, string phone, bool isPhone) =>
        readers.Add(new Reader { Name = name, Phone = phone });

    public void ShowReaders()
    {
        if (readers.Count == 0) { Console.WriteLine("Читателей пока нет."); return; }
        int i = 1;
        foreach (var r in readers) Console.WriteLine($"{i++}. {r}");
    }

    // Задача 19
    public int CountBooksRecursive() => CountRecursive(0);

    private int CountRecursive(int index) =>
        index >= books.Count ? 0 : 1 + CountRecursive(index + 1);

    // Задача 20
    public void SortBooksByYear() => books.Sort((a, b) => a.Year.CompareTo(b.Year));

    // Задача 21
    public bool BorrowBook(string title, string readerName)
    {
        Book book = FindBook(title);
        if (book == null) { Console.WriteLine("Книга не найдена."); return false; }
        if (!IsReaderRegistered(readerName)) { Console.WriteLine("Читатель не зарегистрирован."); return false; }
        if (!book.IsAvailable) { Console.WriteLine("Книга уже выдана."); return false; }

        book.IsAvailable = false;
        book.BorrowedBy = readerName;
        book.DueDate = DateTime.Now.AddDays(LoanDays);
        Console.WriteLine($"Книга выдана. Вернуть до {book.DueDate:dd.MM.yyyy}.");
        return true;
    }

    // Задачи 18, 22
    public bool ReturnBook(string title)
    {
        Book book = FindBook(title);
        if (book == null) { Console.WriteLine("Такой книги нет в библиотеке."); return false; }
        if (book.IsAvailable) { Console.WriteLine("Эта книга не была выдана."); return false; }

        int overdue = (int)(DateTime.Now.Date - book.DueDate.Date).TotalDays;
        int fine = CalculateFine(overdue);

        book.IsAvailable = true;
        book.BorrowedBy = null;
        Console.WriteLine("Книга возвращена.");
        if (fine > 0) Console.WriteLine($"Просрочка {overdue} дн. Штраф: {fine} тг.");
        return true;
    }

    // Задача 23
    public void ShowStatistics()
    {
        int total = GetBookCount();
        int available = GetAvailableBooks();
        Console.WriteLine("=== Статистика библиотеки ===");
        Console.WriteLine($"Всего книг: {total}");
        Console.WriteLine($"Доступно: {available}");
        Console.WriteLine($"Выдано: {total - available}");
        Console.WriteLine($"Зарегистрировано читателей: {readers.Count}");
    }

    // Задача 24
    public static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("УПРАВЛЕНИЕ БИБЛИОТЕКОЙ");
        Console.WriteLine("==============================");
        Console.WriteLine("1. Добавить книгу");
        Console.WriteLine("2. Показать все книги");
        Console.WriteLine("3. Найти книгу");
        Console.WriteLine("4. Добавить читателя");
        Console.WriteLine("5. Показать читателей");
        Console.WriteLine("6. Выдать книгу");
        Console.WriteLine("7. Вернуть книгу");
        Console.WriteLine("8. Статистика библиотеки");
        Console.WriteLine("0. Выход");
        Console.WriteLine();
        Console.Write("Выберите действие: ");
    }
}