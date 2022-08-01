using ReddTok.Objects;
using System.Speech.Synthesis;
using System.Globalization;

namespace ReddTok.Factories
{
    /// <summary>
    /// Performs audio related composed tasks
    /// </summary>
    public class AudioFactory
    {
        readonly SpeechSynthesizer synthesizer = new();

        /// <summary>
        /// Generates audio file from a text sequence
        /// </summary>
        /// <param name="text"></param>
        /// <param name="voice"></param>
        /// <param name="outputPath"></param>
        public void GenerateAudioFromText(string text, Voice voice, string outputPath)
        {
            Console.WriteLine("Start generating audio track from text");
            // Config voice
            string correspondantVoiceName = this.GetCorrespondantVoiceName(voice);
            this.synthesizer.SelectVoice(correspondantVoiceName);

            // Set ouput file
            this.synthesizer.SetOutputToWaveFile(outputPath);

            this.synthesizer.Speak(text);
            Console.WriteLine("Audio track is ready.");

        }

        /// <summary>
        /// Get the name of the system installed that correspond to a specific description
        /// </summary>
        /// <param name="voice">The voice pattern that must be matched</param>
        /// <returns>The name of the correponding system voice</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        private string GetCorrespondantVoiceName(Voice voice)
        {
            List<VoiceInfo> installedVoiceInfoList = GetInstalledVoicesInfos();
            if (installedVoiceInfoList.Count == 0) throw new FileNotFoundException("No installed voice found");

            VoiceGender gender = voice.Gender == Gender.MALE ? VoiceGender.Male : VoiceGender.Female;
            CultureInfo language = null;
            switch (voice.Language)
            {
                case Language.EN_US:
                    language = new CultureInfo("en-US");
                    break;
                case Language.FR:
                    language = new CultureInfo("fr-FR");
                    break;
                case Language.DEU:
                    language = new CultureInfo("de-DE");
                    break;
                case Language.RU:
                    language = new CultureInfo("ru-RU");
                    break;
            }
            if (language == null) throw new ArgumentNullException("language in null");

            var correspondantVoices = from voiceInfo
                                      in installedVoiceInfoList
                                      where voiceInfo.Gender == gender && voiceInfo.Culture.Name == language.Name
                                      select voiceInfo;
            if (correspondantVoices.Count() == 0) throw new NullReferenceException("No correspondant voice found");

            return correspondantVoices.ElementAt<VoiceInfo>(0).Name;
        }

        /// <summary>
        /// Get informations about all installed voices
        /// </summary>
        /// <returns>Alist of the retrieved informations</returns>
        private List<VoiceInfo> GetInstalledVoicesInfos()
        {
            List<VoiceInfo> voices = new();
            foreach (InstalledVoice voice in this.synthesizer.GetInstalledVoices()) 
            { 
                if (voice.Enabled) voices.Add(voice.VoiceInfo);
            } 
            return voices;
        }
    }

   
}
