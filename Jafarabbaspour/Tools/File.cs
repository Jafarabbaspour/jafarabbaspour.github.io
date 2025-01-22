
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using System.Text.RegularExpressions;
using KajeAbi.Application.Convertors;

namespace KajeAbi.Application.Tools;

public static class FileTools
{

    public const int ImageMinimumBytes = 512;

    public static void AddImageToServer(this IFormFile image, string fileName, string originalPath, int? width,
        int? height, string? thumbPath = null, string? deletefileName = null, bool checkImageContent = true)
    {
        if (image != null && image.IsImage(checkImageContent))
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

            // Convert the file name to have a .webp extension
            fileName = Path.ChangeExtension(fileName, "webp");

            // checks is thumb need to be added or not
            if (thumbPath is not null)
                thumbPath = Directory.GetCurrentDirectory() + "/wwwroot" + thumbPath;


            if (!Directory.Exists(originalPath))
                Directory.CreateDirectory(originalPath);

            if ((!string.IsNullOrEmpty(deletefileName)) && deletefileName != "upload.svg")
            {
                if (File.Exists(originalPath + deletefileName))
                    File.Delete(originalPath + deletefileName);

                if (!string.IsNullOrEmpty(thumbPath))
                {
                    if (File.Exists(thumbPath + deletefileName))
                        File.Delete(thumbPath + deletefileName);
                }
            }

            string finalPath = originalPath + fileName;

            using (var imageSharpImage = Image.Load(image.OpenReadStream()))
            {
                using (var outputStream = new FileStream(finalPath, FileMode.Create))
                {
                    var options = new WebpEncoder
                    {
                        Quality = 100
                    };

                    imageSharpImage.Save(outputStream, options);
                }
            }

            if (!string.IsNullOrEmpty(thumbPath))
            {
                if (!Directory.Exists(thumbPath))
                    Directory.CreateDirectory(thumbPath);

                ImageOptimizer resizer = new ImageOptimizer();

                if (width != null && height != null)
                    resizer.ImageResizer(originalPath + fileName, thumbPath + fileName, width, height);
            }
        }

        else if (image != null && image.IsSVG(checkImageContent))
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

            if (!Directory.Exists(originalPath))
                Directory.CreateDirectory(originalPath);

            if ((!string.IsNullOrEmpty(deletefileName)) && deletefileName != "upload.svg")
            {
                if (File.Exists(originalPath + deletefileName))
                    File.Delete(originalPath + deletefileName);
            }

            string finalPath = originalPath + fileName;

            using (var stream = new FileStream(finalPath, FileMode.Create))
            {
                if (!Directory.Exists(finalPath)) image.CopyTo(stream);
            }

        }
    }
    public static void AddImageToServerWithOutThumb(this IFormFile image, string fileName, string originalPath,
        string? deletefileName = null, bool checkImageContent = true)
    {
        if (image != null && image.IsImage(checkImageContent))
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

            if (!Directory.Exists(originalPath))
                Directory.CreateDirectory(originalPath);

            if ((!string.IsNullOrEmpty(deletefileName)) && deletefileName != "upload.svg")
            {
                if (File.Exists(originalPath + deletefileName))
                    File.Delete(originalPath + deletefileName);
            }

            string finalPath = originalPath + fileName;

            using (var stream = new FileStream(finalPath, FileMode.Create))
            {
                if (!Directory.Exists(finalPath)) image.CopyTo(stream);
            }
        }
    }

    public static void DeleteImage(this string imageName, string originalPath, string? thumbPath)
    {
        if ((!string.IsNullOrEmpty(imageName)) && imageName != "upload.svg")
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;
            thumbPath = Directory.GetCurrentDirectory() + "/wwwroot" + thumbPath;

            if (File.Exists(originalPath + imageName))
                File.Delete(originalPath + imageName);

            if (!string.IsNullOrEmpty(thumbPath))
            {
                if (File.Exists(thumbPath + imageName))
                    File.Delete(thumbPath + imageName);
            }
        }
    }
    public static void DeleteImage(this string imageName, string originalPath)
    {
        if ((!string.IsNullOrEmpty(imageName)) && imageName != "upload.svg")
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

            if (File.Exists(originalPath + imageName))
                File.Delete(originalPath + imageName);
        }
    }

    public static List<string> FetchLinksFromSource(this string htmlSource)
    {
        List<string> links = new List<string>();

        string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";

        MatchCollection matchesImgSrc =
            Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        foreach (Match m in matchesImgSrc)
        {
            string href = m.Groups[1].Value;

            links.Add(href);
        }

        return links;
    }

    public static async void AddFilesToServer(this IFormFile file, string fileName, string originalPath,
        string? deletefileName = null, bool checkFileExtension = true)
    {
        if ((file != null && file.IsFile(checkFileExtension)))
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

            if (!Directory.Exists(originalPath))
                Directory.CreateDirectory(originalPath);

            if (!string.IsNullOrEmpty(deletefileName))
            {
                if (File.Exists(originalPath + deletefileName))
                {
                    File.Delete(originalPath + deletefileName);
                }
            }

            if (!Directory.Exists(originalPath))
                Directory.CreateDirectory(originalPath);

            string finalPath = originalPath + fileName;

            using (var stream = new FileStream(finalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
       
    }

    public static void DeleteFile(this string fileName, string originalPath)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

            if (File.Exists(originalPath + fileName))
            {
                File.Delete(originalPath + fileName);
            }
        }
    }

    public static async Task AddFilesToServerWithNoFormatChecker(this IFormFile file, string fileName,
        string originalPath)
    {
        originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;

        if (!Directory.Exists(originalPath))
            Directory.CreateDirectory(originalPath);

        string finalPath = originalPath + fileName;

        using (var stream = new FileStream(finalPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

  
    public static bool IsFile(this IFormFile postedFile, bool checkFileExtension = true)
    {
        if (checkFileExtension)
        {
            if (Path.GetExtension(postedFile.FileName)?.ToLower() != ".rar" &&
                Path.GetExtension(postedFile.FileName)?.ToLower() != ".zip" &&
                Path.GetExtension(postedFile.FileName)?.ToLower() != ".pdf")
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsZip(this IFormFile postedFile, bool checkFileExtension = true)
    {
        if (checkFileExtension)
        {
            if (Path.GetExtension(postedFile.FileName)?.ToLower() != ".rar" &&
                Path.GetExtension(postedFile.FileName)?.ToLower() != ".zip")
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsZip(this string postedFile, bool checkFileExtension = true)
    {
        if (checkFileExtension)
        {
            if (Path.GetExtension(postedFile)?.ToLower() != ".rar" &&
                Path.GetExtension(postedFile)?.ToLower() != ".zip")
            {
                return false;
            }
        }

        return true;
    }

    public static bool HasLength(this IFormFile postedFile, int length)
    {
        if (postedFile.Length > length)
        {
            return false;
        }

        return true;
    }

    public static bool IsImage(this IFormFile postedFile, bool checkImage = true)
    {
        if (checkImage)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png" &&
                        postedFile.ContentType.ToLower() != "image/svg+xml" &&
                        postedFile.ContentType.ToLower() != "image/webp")

            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".svg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".webp")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }
                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------
            //try
            //{
            //    using (var bitmap = new Bitmap(postedFile.OpenReadStream())) { }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            //finally
            //{
            //    postedFile.OpenReadStream().Position = 0;
            //}

            return true;
        }

        return true;
    }

    public static bool IsImage(this string postedFile, bool checkImage = true)
    {
        if (checkImage)
        {
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile).ToLower() != ".jpg"
                && Path.GetExtension(postedFile).ToLower() != ".png"
                && Path.GetExtension(postedFile).ToLower() != ".jpeg"
                && Path.GetExtension(postedFile).ToLower() != ".webp"
                && Path.GetExtension(postedFile).ToLower() != ".svg")
            {
                return false;
            }

            return true;
        }

        return true;
    }

    public static bool IsVideo(this IFormFile postedFile)
    {
        //-------------------------------------------
        //  Check the video mime types
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".mp4"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".avi"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".wmv"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".mpeg-2"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".mov")
        {
            return false;
        }

        //-------------------------------------------
        //  Check the video extension
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".mp4"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".avi"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".wmv"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".mpeg-2"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".mov")
        {
            return false;
        }

        //-------------------------------------------
        //  Attempt to read the file and check the first bytes
        //-------------------------------------------
        try
        {
            if (!postedFile.OpenReadStream().CanRead)
            {
                return false;
            }
            //------------------------------------------
            //check whether the image size exceeding the limit or not
            //------------------------------------------ 
            //if (postedFile.Length < ImageMinimumBytes)
            //{
            //    return false;
            //}

            byte[] buffer = new byte[ImageMinimumBytes];
            postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
            string content = System.Text.Encoding.UTF8.GetString(buffer);
            if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public static bool IsVideo(this string postedFile)
    {
        //-------------------------------------------
        //  Check the video mime types
        //-------------------------------------------
        if (Path.GetExtension(postedFile).ToLower() != ".mp4"
            && Path.GetExtension(postedFile).ToLower() != ".avi"
            && Path.GetExtension(postedFile).ToLower() != ".wmv"
            && Path.GetExtension(postedFile).ToLower() != ".mpeg-2"
            && Path.GetExtension(postedFile).ToLower() != ".mov")
        {
            return false;
        }

        return true;
    }

    public static bool IsSVG(this IFormFile postedFile, bool checkImage = true)
    {
        if (checkImage)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/svg+xml")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".svg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        return true;
    }

    public static bool IsSVG(this string postedFile, bool checkImage = true)
    {
        if (checkImage)
        {
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile).ToLower() != ".svg")
            {
                return false;
            }

            return true;
        }

        return true;
    }

    public static bool IsFileOrImage(this IFormFile postedFile) => (postedFile.IsImage() || postedFile.IsFile());


}
