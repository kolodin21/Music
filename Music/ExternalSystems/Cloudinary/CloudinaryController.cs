using Microsoft.AspNetCore.Mvc;
using Music.Application.ExternalSystems.Cloudinary;

namespace Music.ExternalSystems.Cloudinary
{
    public class CloudinaryController : Controller
    {
        private readonly ICloudinaryUploader _cloudinaryUploader;

        public CloudinaryController(ICloudinaryUploader cloudinaryUploader)
        {
            _cloudinaryUploader = cloudinaryUploader;
        }

        [HttpPost]
        public async Task<IActionResult> Audio(List<IFormFile>? audioFiles)
        {
            if (audioFiles == null || !audioFiles.Any())
                return BadRequest("Файлы не выбраны");

            var uploadedSongs = new List<FileSong>();

            foreach (var file in audioFiles)
            {
                try
                {
                    var audio = new FormFileAdapter(file);
                    var uploadedUrl = await _cloudinaryUploader.UploadAudioAsync(audio);
                    uploadedSongs.Add(uploadedUrl);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Ошибка загрузки {file.FileName}: {ex.Message}");
                }
            }

            return Ok(uploadedSongs);
        }
    }
}
