namespace SpaceDynamicProject.Extensions
{
    public static class FileManager
    {
        public static bool IsValidType(this IFormFile formFile, string type)
            => formFile.ContentType.Contains(type);

        public static bool IsValidSize(this IFormFile formFile, int KByte)
            => formFile.Length <= 1024 * KByte;

        public static async Task<string> SaveMangeImage(this IFormFile formFile, string path)
        {
            string fileName = formFile.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;

            FileStream fileStream = new FileStream(Path.Combine(path,fileName),FileMode.Create);

            await formFile.CopyToAsync(fileStream);

            return fileName;
        }

        public static async Task Delete(this string formFile, string path)
        {
            File.Delete(Path.Combine(path,formFile));
        }
    }
}
