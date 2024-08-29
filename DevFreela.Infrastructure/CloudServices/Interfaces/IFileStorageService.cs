namespace DevFreela.Infrastructure.CloudServices.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(byte[] bytes, string fileName);
    }
}
