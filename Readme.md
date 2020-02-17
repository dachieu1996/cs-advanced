# Advanced C#
## Generic

## Delegate
* What is Delegate?
  * An object that knows how to call a method (or a group of methods).
  * A reference to a function.
* Why do we need delegate? 
  * For designing extensible and flexible applications (eg frameworks).
  * Alternative: Interfaces.
* Use a delegate when:
  * An evening design pattern is used.
  * The caller doesn't need to access other properties or methods on object implementing the method.
```cs
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
```
```cs
public class PhotoProcessor
{
    public delegate void PhotoFilterHandler(Photo photo);
    public void Process(string path, PhotoFilterHandler filterHandler)
    {
        var photo = Photo.Load(path);

        //var filter = new PhotoFilters();
        //filter.ApplyBrightness(photo);
        //filter.ApplyContrast(photo);
        //filter.Resize(photo);

        filterHandler(photo);
    }

    public void Process(string path, Action<Photo> filterHandler)
    {
        var photo = Photo.Load(path);

        //var filter = new PhotoFilters();
        //filter.ApplyBrightness(photo);
        //filter.ApplyContrast(photo);
        //filter.Resize(photo);

        filterHandler(photo);
    }
}
```


## Lamda Expressions
```cs
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
```

## Event
```cs
public class VideoEventArgs : EventArgs
{
    public Video Video { get; set; }
}
public class VideoEncoder
{
    // 1. Define a delegate
    // 2. Define an event based on that delegate
    // 3. Raise the event

    // - Non include Specific EventArgs
    //public delegate void VideoEncodedEventHandler(object source, EventArgs args); // 1
        
    // - Include VideoEventArgs
    //public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args); // 1
    //public event VideoEncodedEventHandler VideoEncoded; // 2

    // - Short way to define Event
    public event EventHandler<VideoEventArgs> VideoEncoded; 
    public void Encode(Video video)
    {
        Console.WriteLine("Encoding Video...");
        Thread.Sleep(2000);

        OnVideoEncoded(video);
    }

    protected virtual void OnVideoEncoded(Video video)
    {
        if(VideoEncoded != null)
            VideoEncoded(this, new VideoEventArgs{Video = video}); // 3
    }
}
```

```cs
public class MailService
{
    public void OnVideoEncoded(object source, VideoEventArgs e)
    {
        Console.WriteLine("MailService: Sending an email..." + e.Video.Title);
    }
}
public class MessageService
{
    public void OnVideoEncoded(object source, VideoEventArgs e)
    {
        Console.WriteLine("MessageService: Sending a text message..." + e.Video.Title);
    }
}
```

```cs
class Program
{
    static void Main(string[] args)
    {
        var video = new Video { Title = "Video 1" };
        // publisher
        var videoEncoder = new VideoEncoder(); 

        // subcriber
        var mailService = new MailService();
        var messageService = new MessageService();

        videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
        videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

        videoEncoder.Encode(video);
            
        Console.ReadKey();
    }
}
```

## Extension Method
```cs
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
```