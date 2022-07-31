using ReddTok.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReddTok.Services
{
    public class VoiceService
    {
        public Voice SelectVoice(string? gender, string? language)
        {
            if (gender == "MALE" && language == "EN") return SelectMaleUsEnVoice();
            if (gender == "MALE" && language == "FR") return SelectMaleFrVoice();
            if (gender == "FEMALE" && language == "EN") return SelectFemaleUsEnVoice();
            if (gender == "FEMALE" && language == "FR") return SelectFemaleFrVoice();
            else return SelectDefaultVoice();

        }
        private Voice SelectFemaleUsEnVoice()
        {
            return new Voice(Gender.FEMALE, Language.EN_US);
        }

        private Voice SelectFemaleFrVoice()
        {
            return new Voice(Gender.FEMALE, Language.FR);
        }

        private Voice SelectMaleUsEnVoice()
        {
            return new Voice(Gender.MALE, Language.EN_US);
        }

        private Voice SelectMaleFrVoice()
        {
            return new Voice(Gender.MALE, Language.FR);
        }

        private Voice SelectDefaultVoice()
        {
            Voice voice = new();
            voice.AutoConfig();
            return voice;
        }
    }
}
