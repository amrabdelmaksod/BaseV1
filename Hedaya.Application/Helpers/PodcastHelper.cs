using Hedaya.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NAudio.Wave;

namespace Hedaya.Application.Helpers
{
    public class PodcastHelper
    {
        public static async Task<string> SavePodcastAudio(IFormFile audioFile, IHostingEnvironment hostingEnvironment)
        {
            // Create the directory if it does not exist
            if (!Directory.Exists(hostingEnvironment.WebRootPath + "\\uploads\\audio\\"))
            {
                Directory.CreateDirectory(hostingEnvironment.WebRootPath + "\\uploads\\audio\\");
            }

            // Generate a unique filename for the audio file
            string fileName = RandomHelper.GenerateUniqueID(25) + Path.GetExtension(audioFile.FileName).ToLower();

            // Build the full file path
            string filePath = "\\uploads\\audio\\" + fileName;
            string fullPath = Path.Combine(hostingEnvironment.WebRootPath, filePath);

          

            using (FileStream filestream = File.Create(hostingEnvironment.WebRootPath + filePath))
            {
                audioFile.CopyTo(filestream);
                filestream.Flush();
                var fileExtention = Path.GetExtension(audioFile.FileName).ToLower();
            }

            return filePath;
        }

        public static TimeSpan GetAudioDuration(string audioFilePath)
        {
            try
            {
                using (var audioFileReader = new AudioFileReader(audioFilePath))
                {
                    return audioFileReader.TotalTime;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
    }
}
