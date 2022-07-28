using ReddTok.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Globalization;

namespace ReddTok.Factories
{
    public class AudioFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        readonly SpeechSynthesizer synthesizer = new();

        
        public void GenerateAudioFromText(string text, Voice voice, string outputPath)
        {
            // Config voice
            string correspondantVoiceName = this.GetCorrespondantVoiceName(voice);
            this.synthesizer.SelectVoice(correspondantVoiceName);

            // Set ouput file
            this.synthesizer.SetOutputToWaveFile(outputPath);

            this.synthesizer.Speak(text);

        }

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
