using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamdaExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Normal: " + Square(5));

            // args => expression
            // () => ...
            // x => ...
            // (x, y, z) => ...
            Func<int, int> square = number => number * number;
            Console.WriteLine("Lamda Expression: " + square(5));

            // Get all books less than 10$
            var books = new BookRepository().GetBooks();

            // Normal:
            // var cheapBooks = books.FindAll(IsCheaperThan10Dollars);

            // LamdaExpression:
            // Predicate<Book> isCheaperThan10Dollars = book => book.Price < 10;
            // var cheapBooks = books.FindAll(isCheaperThan10Dollars);

            var cheapBooks = books.FindAll(b => b.Price < 10);

            foreach (var cheapBook in cheapBooks)
            {
                Console.WriteLine("\t" + cheapBook.Title + ": " + cheapBook.Price);
            }

            Console.ReadKey();
        }

        static int Square(int number)
        {
            return number * number;
        }

        static bool IsCheaperThan10Dollars(Book book)
        {
            return book.Price < 10;
        }
    }
}
