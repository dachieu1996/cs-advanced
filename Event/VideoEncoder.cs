using System;
using System.Threading;

namespace Event
{
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
}