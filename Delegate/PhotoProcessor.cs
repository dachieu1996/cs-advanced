using System;

namespace Delegate
{
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
}