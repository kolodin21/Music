namespace Music.Application.ExternalSystems.Cloudinary;

public interface ICloudinaryUploader
{
    Task<UploadedFile> UploadFileAsync(IFileData file);
}