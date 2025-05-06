using Plugin.Maui.Audio;

namespace MyIdentityApp.Services
{
    public class AudioService
    {
        private readonly IAudioManager _audioManager;
        private IAudioPlayer? _player;
        private readonly string[] _musicFiles = new[]
         {
            "music/peaceful-garden-healing-light-piano-for-meditation-zen-landscapes-112199.mp3",
            "music/please-calm-my-mind-125566.mp3",
            "music/just-relax-11157.mp3",
            "music/forest-melody-background-music-for-meditation-and-yoga-314446.mp3"
        };


        public AudioService(IAudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public async Task PlayBackgroundMusicAsync()
        {
            if (_player != null && _player.IsPlaying)
                return;

            var stream = await FileSystem.OpenAppPackageFileAsync("music/calming.mp3");
            _player = _audioManager.CreatePlayer(stream);
            _player.Loop = true;
            _player.Play();
        }

        public async Task PlayRandomBackgroundMusicAsync()
        {
            if (_player != null && _player.IsPlaying)
                _player.Stop();

            var random = new Random();
            var selected = _musicFiles[random.Next(_musicFiles.Length)];

            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync(selected);
                _player = _audioManager.CreatePlayer(stream);
                _player.Loop = true;
                _player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play music file {selected}: {ex.Message}");
            }
        }

        public void Stop()
        {
            _player?.Stop();
        }
    }
}
