using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new PhotoProcessor();
            var filter = new PhotoFilters();
            
            // Custom Delegate
            PhotoProcessor.PhotoFilterHandler filterHanlder = filter.ApplyBrightness;
            filterHanlder += filter.ApplyContrast;
            filterHanlder += RemoveRedEyeFilter;
            Console.WriteLine("------------Custom Delegate------------");
            processor.Process("example.com", filterHanlder);

            // Action Delegate
            Action<Photo> filterHandler = filter.Resize;
            filterHandler += RemoveRedEyeFilter;
            Console.WriteLine("------------Action Delegate------------");
            processor.Process("example.com", filterHandler);
        }

        static void RemoveRedEyeFilter(Photo photo)
        {
            Console.WriteLine("Remove Red Eye");
        }
    }
}
