using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Music.Application.ExternalSystems.Cloudinary;

namespace Music.Infrastructure.Cloudinary
{
    public class CloudinaryUploader : ICloudinaryUploader
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryUploader(CloudinaryDotNet.Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<FileSong> UploadAudioAsync(IFileData audioFile)
        {
            try
            {
                if (audioFile == null || audioFile.Length == 0)
                    throw new ArgumentException("Файл не выбран");

                await using var stream = audioFile.OpenReadStream();

                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(audioFile.FileName, stream),
                    PublicId = $"{Path.GetFileNameWithoutExtension(audioFile.FileName)}_{Guid.NewGuid()}"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception("Ошибка загрузки файла в Cloudinary");

                var song = new FileSong
                {
                    FileName = uploadResult.OriginalFilename,
                    Url = uploadResult.SecureUrl.ToString(),
                };
                return song;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
