using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Handling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var api = new YoutubeApi();
                var videos = api.GetVideos("mosh");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public class YoutubeApi
    {
        public List<Video> GetVideos(string user)
        {
            try
            {
                // Access Youtube web service
                // Read the data
                // Create a list of video objects
                throw new Exception("Oops some low level Youtube error.");
            }
            catch (Exception ex)
            {
                throw new YoutubeException("Could not fetch the videos from Youtube", ex);
            }

            return new List<Video>();
        }
    }

    public class Video
    {
    }

    public class YoutubeException : Exception
    {
        public YoutubeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
