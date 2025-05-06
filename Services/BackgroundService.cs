
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace MyIdentityApp.Services
{
    public class BackgroundService
    {
        private readonly string[] backgrounds = new[]
        {
            "images/backgrounds/andika-christian-FqIGF4EwNNY-unsplash.jpg",
            "images/backgrounds/andreas-rasmussen-dj2evWGM15U-unsplash.jpg",
            "images/backgrounds/ansh-jain-TiYexNeqCFI-unsplash.jpg",
            "images/backgrounds/antoine-rault-IhWRrZx4-kk-unsplash.jpg",
            "images/backgrounds/arctic-AO_Z_NBtGSE-unsplash.jpg",
            "images/backgrounds/arctic-qu-le6pveQerjo-unsplash.jpg",
            "images/backgrounds/austin-schmid-r7zjJ63kAPU-unsplash.jpg",
            "images/backgrounds/ben-curry-FtrvgO_CXac-unsplash.jpg",
            "images/backgrounds/benjamin-voros-phIFdC6lA4E-unsplash.jpg",
            "images/backgrounds/birger-strahl-b3ZpqtiSMus-unsplash.jpg",
            "images/backgrounds/bonnie-kittle-JfEGdpwQP9k-unsplash.jpg"
        };

        public string CurrentBackground { get; private set; } = string.Empty;

        public event Action OnBackgroundChanged;

        public async Task SetRandomBackgroundAsync()
        {
            var random = new Random();
            var selected = backgrounds[random.Next(backgrounds.Length)];

            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(selected);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                var base64 = Convert.ToBase64String(ms.ToArray());

                CurrentBackground = $"background: url('data:image/jpeg;base64,{base64}') no-repeat center center fixed; background-size: cover;";
                OnBackgroundChanged?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting background: {ex.Message}");
            }
        }
    }
}
