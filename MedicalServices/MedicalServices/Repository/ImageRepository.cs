using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MedicalServices.Repository
{
    public class ImageRepository: IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        #region InsertImage
        public async Task<bool> SaveImageInWroot(IHasImage hasImage,IFormFile ImageFile,string Location= "Images/Imagesadmin/")
        {

            if (ImageFile != null)
            {
                string folder = Location;
                folder += Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string serverfolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(serverfolder));
                    using (var fileStream = new FileStream(serverfolder, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }
                    hasImage.Image = folder;
                    return true;
                }
                catch (Exception ex)
                {
                    
                   return false;
                }
            }
            return false;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateImageInWroot(IHasImage hasImage, string Location = "Images/Imagesadmin/")
        {
            IFormFile newImageFile = GetImageAsFormFile(hasImage.Image, webHostEnvironment.WebRootPath);
            if (newImageFile == null)
                return false;
            if (!string.IsNullOrEmpty(hasImage.Image))
            {
                DeleteImageInWroot(hasImage.Image);
            }
            return await SaveImageInWroot(hasImage, newImageFile, Location);
        }

        public IFormFile GetImageAsFormFile(string imagePath, string webRootPath)
        {
            if (string.IsNullOrEmpty(imagePath) || string.IsNullOrEmpty(webRootPath))
            {
                return null;
            }

            string fullPath = Path.Combine(webRootPath, imagePath);

            if (!File.Exists(fullPath))
            {
                return null;
            }

            try
            {
                var fileInfo = new FileInfo(fullPath);
                var stream = new FileStream(fullPath, FileMode.Open);
                return new FormFile(
                    baseStream: stream,
                    baseStreamOffset: 0,
                    length: fileInfo.Length,
                    name: "file", // This is the form field name
                    fileName: Path.GetFileName(fullPath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(fullPath)
                };
            }
            catch
            {
                return null;
            }
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream" // default content type
            };
        }
        #endregion

        #region Delete
        //Delete Image
        public  void DeleteImageInWroot(string Image)
        {

            if (Image != null)
            {
                if (!string.IsNullOrEmpty(Image))
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, Image);

                    if (System.IO.File.Exists(filePath))
                    {
                         System.IO.File.Delete(filePath);

                    }
                }

            }

        }


        #endregion


    }
}
