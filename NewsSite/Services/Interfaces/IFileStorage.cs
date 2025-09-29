namespace NewsSite.Services.Interfaces
{
    public interface IFileStorage
    {
        
        Task<string> SaveFileAsync(IFormFile file);

        void DeleteFile(string fileName);
    }
}
