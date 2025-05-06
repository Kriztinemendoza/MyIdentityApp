
using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace MyIdentityApp.Services
{
    public class TtsService
    {
        private CancellationTokenSource? _ttsCancelTokenSource;

        public async Task StopAndSpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            _ttsCancelTokenSource?.Cancel();
            _ttsCancelTokenSource = new CancellationTokenSource();

            try
            {
                await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions
                {
                    Volume = 1.0f,
                    Pitch = 1.0f
                },
                _ttsCancelTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Speech was cancelled – safe to ignore
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TTS failed: {ex.Message}");
            }
        }

        public async Task SpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions
            {
                Volume = 1.0f,
                Pitch = 1.0f
            });
        }

        public void Stop()
        {
            _ttsCancelTokenSource?.Cancel();
        }
    }
}
