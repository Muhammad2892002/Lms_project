using Lms_project.Core.Exceptions;
using Lms_project.Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Storage
{
    public class ImageService : IImageService
    {
       
        private readonly string _folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "coureses");
        private static readonly string[] AllowedExtention = [".jpg", ".jpeg", ".png", ".webp"];
        public void DeleteCourseImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            var path=Path.Combine(_folder,Path.GetFileName(imageUrl));
            if (File.Exists(path))
                File.Delete(path);
        }

        public async Task<string> SaveCourseImageAsync(Stream contnet, string fileName, long length)
        {
            var seeWhatIsCurrentDirectory = Directory.GetCurrentDirectory();
            if (length == 0|| length>2*1024*1024)
                throw new InvalidFileException("File size must be between 1 byte and 2 MB");

            var extention = Path.GetExtension(fileName).ToLower();
            if(!AllowedExtention.Contains(extention))
                throw new InvalidFileException($"Invalid file extension(.jpg,.jpeg,.png, .webp) ");
            Directory.CreateDirectory(_folder); //does not effect if the directory already exists
            var name =$"{Guid.NewGuid()}{extention}";
            await using var file=File.Create(Path.Combine(_folder, name));
            await contnet.CopyToAsync(file);
            return "images/coureses/" + name;

         
        }
    }
}
