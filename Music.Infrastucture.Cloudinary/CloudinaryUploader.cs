using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Music.Infrastructure.Cloudinary
{
    public class CloudinaryUploader : ICloudinaryUploader
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryUploader(CloudinaryDotNet.Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadAudioAsync(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                throw new ArgumentException("Файл не выбран");

            await using var stream = audioFile.OpenReadStream();

            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(audioFile.FileName, stream),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            throw new Exception("Ошибка загрузки файла в Cloudinary");
        }
    }
}
