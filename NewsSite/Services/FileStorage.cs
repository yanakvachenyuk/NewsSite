using NewsSite.Services.Interfaces;

namespace NewsSite.Services
{
    public class FileStorage : IFileStorage
    {
        private readonly string _uploadFolder;

        public FileStorage(IWebHostEnvironment env)
        {
            // wwwroot/uploads/news
            _uploadFolder = Path.Combine(env.WebRootPath, "uploads", "news");

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Файл пустой");

            string extension = Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid() + extension;
            string fullPath = Path.Combine(_uploadFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            string fullPath = Path.Combine(_uploadFolder, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
