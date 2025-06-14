namespace Music.Application.ExternalSystems.Cloudinary;

public interface ICloudinaryUploader
{
    Task<FileSong> UploadAudioAsync(IFileData audioFile);
}