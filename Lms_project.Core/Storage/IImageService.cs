using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Storage
{
    public interface IImageService
    {
      public Task<string> SaveCourseImageAsync(Stream contnet, string fileName, long length);
        public void DeleteCourseImage(string? imageUrl);
    }
}
