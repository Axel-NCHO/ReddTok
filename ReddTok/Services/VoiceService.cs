using ReddTok.Objects;

namespace ReddTok.Services
{
    /// <summary>
    /// Performs voice related tasks
    /// </summary>
    public class VoiceService
    {
        /// <summary>
        /// Choose a voice pattern
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public Voice SelectVoice(string? gender, string? language)
        {
            Console.WriteLine($"Choosing voice {gender} - {language} among system installed voices...");

            if (gender == "MALE" && language == "EN") return SelectMaleUsEnVoice();
            if (gender == "MALE" && language == "FR") return SelectMaleFrVoice();
            if (gender == "MALE" && language == "DE") return SelectMaleDeVoice();
            if (gender == "MALE" && language == "RU") return SelectMaleRuVoice();

            if (gender == "FEMALE" && language == "EN") return SelectFemaleUsEnVoice();
            if (gender == "FEMALE" && language == "FR") return SelectFemaleFrVoice();
            if (gender == "FEMALE" && language == "DE") return SelectFemaleDeVoice();
            if (gender == "FEMALE" && language == "RU") return SelectFemaleRuVoice();

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

        private Voice SelectMaleDeVoice()
        {
            return new Voice(Gender.MALE, Language.DEU);
        }

        private Voice SelectFemaleDeVoice()
        {
            return new Voice(Gender.FEMALE, Language.DEU);
        }

        private Voice SelectMaleRuVoice()
        {
            return new Voice(Gender.MALE, Language.RU);
        }

        private Voice SelectFemaleRuVoice()
        {
            return new Voice(Gender.FEMALE, Language.RU);
        }

        private Voice SelectDefaultVoice()
        {
            return new Voice();
        }
    }
}
