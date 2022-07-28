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
        public Voice SelectFemaleUsEnVoice()
        {
            return new Voice(Gender.FEMALE, Language.EN_US);
        }

        public Voice SelectFemaleFrVoice()
        {
            return new Voice(Gender.FEMALE, Language.FR);
        }

        public Voice SelectMaleUsEnVoice()
        {
            return new Voice(Gender.MALE, Language.EN_US);
        }

        public Voice SelectMaleFRVoice()
        {
            return new Voice(Gender.MALE, Language.FR);
        }

        public Voice SelectDefaultVoice()
        {
            Voice voice = new();
            voice.AutoConfig();
            return voice;
        }
    }
}
