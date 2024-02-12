using eCommerceAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Infrastructure.Services
{
    //TODO : Dosya yukleme konusu atladım. Gerekli durumlarda kontrol.
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            ////wwwroot/resource/product-images
            //var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product/images");

            //// path mevcut değilse oluştur
            //if (!Directory.Exists(uploadPath))
            //{
            //    Directory.CreateDirectory(uploadPath);
            //}

            //List<(string fileName, string path)> datas = new List<(string fileName, string path)>();
            //var results = new List<bool>();

            //foreach (var file in files)
            //{
            //    string fileNewName = await FileRenameAsync(file.FileName);

            //    bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName})", file);

            //    datas.Add((fileNewName, $"{uploadPath}\\{fileNewName})"));
            //    results.Add(result);
            //}

            //if (results.TrueForAll(x => x.Equals(true)))ra
            //{
            //    return datas;
            //}

            return null;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private Task<string> FileRenameAsync(string fileName)
        //{
        //    var extension = Path.GetExtension(fileName);
        //    var oldName = Path.GetFileNameWithoutExtension(fileName);
        //    var newFileName = $"{FileRenamer(oldName)}{extension}";


        //}

        //public static string FileRenamer(string name)
        //{
        //    return name.Replace("\"", "")
        //        .Replace("!", "")
        //        .Replace("'", "")
        //        .Replace("^", "")
        //        .Replace("+", "")
        //        .Replace("%", "")
        //        .Replace("&", "")
        //        .Replace("/", "")
        //        .Replace("(", "")
        //        .Replace(")", "")
        //        .Replace("=", "")
        //        .Replace("?", "")
        //        .Replace("_", "")
        //        .Replace(" ", "-")
        //        .Replace("@", "")
        //        .Replace("€", "")
        //        .Replace("¨", "")
        //        .Replace("~", "")
        //        .Replace(",", "")
        //        .Replace(";", "")
        //        .Replace(":", "")
        //        .Replace(".", "-")
        //        .Replace("Ö", "o")
        //        .Replace("ö", "o")
        //        .Replace("Ü", "u")
        //        .Replace("ü", "u")
        //        .Replace("ı", "i")
        //        .Replace("İ", "i")
        //        .Replace("ğ", "g")
        //        .Replace("Ğ", "g")
        //        .Replace("æ", "")
        //        .Replace("ß", "")
        //        .Replace("â", "a")
        //        .Replace("î", "i")
        //        .Replace("ş", "s")
        //        .Replace("Ş", "s")
        //        .Replace("Ç", "c")
        //        .Replace("ç", "c")
        //        .Replace("<", "")
        //        .Replace(">", "")
        //        .Replace("|", "");
        //}
    }
}
