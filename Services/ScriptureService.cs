// Updated ScriptureService.cs with async initialization and caching
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MyIdentityApp.Models;
using Microsoft.Maui.Storage;

namespace MyIdentityApp.Services
{
    public class ScriptureService
    {
        private List<Scripture>? _scriptures;
        private List<IdentityTheme>? _themes;
        private bool _isInitialized = false;

        public async Task InitializeAsync()
        {
            if (_isInitialized) return;

            if (_themes == null)
                await LoadThemesFromJson();

            if (_scriptures == null)
                await LoadScripturesFromJson();

            _isInitialized = true;
        }

        private async Task LoadScripturesFromJson()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Data/scriptures.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                _scriptures = JsonSerializer.Deserialize<List<Scripture>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Scripture>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load scriptures.json: {ex.Message}");
                _scriptures = new List<Scripture>();
            }
        }

        private async Task LoadThemesFromJson()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Data/themes.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                _themes = JsonSerializer.Deserialize<List<IdentityTheme>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<IdentityTheme>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load themes.json: {ex.Message}");
                _themes = new List<IdentityTheme>();
            }
        }

        public async Task<Scripture?> GetRandomAsync(string? theme = null)
        {
            await InitializeAsync();

            IEnumerable<Scripture> source = _scriptures ?? new List<Scripture>();

            if (!string.IsNullOrWhiteSpace(theme))
            {
                source = source.Where(s => s.IdentityTheme.Equals(theme, StringComparison.OrdinalIgnoreCase));
            }

            var list = source.ToList();
            if (!list.Any()) return null;

            var random = new Random();
            return list[random.Next(list.Count)];
        }

        public async Task<List<IdentityTheme>> GetAllThemesAsync()
        {
            await InitializeAsync();
            return _themes ?? new List<IdentityTheme>();
        }

        public async Task<List<Scripture>> GetAllScripturesAsync()
        {
            await InitializeAsync();
            return _scriptures ?? new List<Scripture>();
        }

        public async Task<List<Scripture>> GetScripturesByThemeAsync(string theme)
        {
            await InitializeAsync();
            return (_scriptures ?? new List<Scripture>()).Where(s => s.IdentityTheme == theme).ToList();
        }

        public async Task<List<Scripture>> SearchScripturesAsync(string query)
        {
            await InitializeAsync();

            if (string.IsNullOrWhiteSpace(query)) return new List<Scripture>();

            query = query.ToLower();

            return (_scriptures ?? new List<Scripture>()).Where(s =>
                s.Text.ToLower().Contains(query) ||
                s.Reference.ToLower().Contains(query) ||
                s.IdentityTheme.ToLower().Contains(query) ||
                s.Affirmation.ToLower().Contains(query)).ToList();
        }

        public void ToggleFavorite(int scriptureId)
        {
            var scripture = _scriptures?.FirstOrDefault(s => s.Id == scriptureId);
            if (scripture != null)
            {
                scripture.IsFavorite = !scripture.IsFavorite;
            }
        }

        public List<Scripture> GetFavorites()
        {
            return (_scriptures ?? new List<Scripture>()).Where(s => s.IsFavorite).ToList();
        }
    }
}
