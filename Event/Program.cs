using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
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
}
