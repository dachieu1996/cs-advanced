using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            string post = "This is supposed to be a very long blog post blah blah blah...";
            var shortenPost = post.Shorten(5);
            Console.WriteLine(shortenPost);
            Console.ReadLine();
        }
    }

    public static class StringExtensions
    {
        public static string Shorten(this String str, int numberOfWords)
        {
            if(numberOfWords < 0)
                throw new ArgumentOutOfRangeException("numberOfWords should be greater than or equal to 0");
            if (numberOfWords == 0)
                return "";

            var words = str.Split(' ');

            if (words.Length <= numberOfWords)
                return str;
            return string.Join(" ", words.Take(numberOfWords));
        }
    }
}