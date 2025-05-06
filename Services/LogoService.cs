using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyIdentityApp.Services
{
    public class LogoService
    {
        public async Task<string> GetLogoPathAsync()
        {

            var destPath = Path.Combine(FileSystem.AppDataDirectory, "MyIdentityAppLogo.png");

            if (!File.Exists(destPath))
            {
                using var source = await FileSystem.OpenAppPackageFileAsync("images/MyIdentityAppLogo.png");
                using var dest = File.Create(destPath);
                await source.CopyToAsync(dest);
            }

            return destPath;
        }
    }

}
