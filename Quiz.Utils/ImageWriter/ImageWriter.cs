using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace QuizUtils.ImageWriter
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadImage(IFormFile file, string imageID)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file, imageID);
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <param name="imageID"></param>
        /// <returns></returns>
        private async Task<string> WriteFile(IFormFile file, string imageID)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = imageID + extension; //Create a new Name for the file due to security reasons.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }
    }
}