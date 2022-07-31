namespace ReddTok.Objects
{
    /// <summary>
    /// Represents a voice pattern
    /// </summary>
    public class Voice
    {
        public Gender Gender { get; set; }

        public Language Language { get; set; }


        /// <summary>
        /// Creates a new voice pattern with customsettings
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="language"></param>
        public Voice(Gender gender, Language language)
        {
            Gender = gender;
            Language = language;
        }


        /// <summary>
        /// Creates a new voice pattern with default settings
        /// </summary>
        public Voice() 
        {
            this.AutoConfig();
        }

        /// <summary>
        /// Set default gender and language
        /// </summary>
        public void AutoConfig()
        {
            this.Gender = Gender.MALE;
            this.Language = Language.EN_US;
        }
    }
}
