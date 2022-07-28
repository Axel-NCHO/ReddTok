using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReddTok.Objects
{
    public class Voice
    {
        public Gender Gender { get; set; }

        public Language Language { get; set; }

        private bool _isAuto;


        public Voice(Gender gender, Language language)
        {
            Gender = gender;
            Language = language;
            this._isAuto = false;
        }

        public Voice() { this._isAuto = true; }

        public void AutoConfig()
        {
            if (this._isAuto)
            {
                this.Gender = Gender.MALE;
                this.Language = Language.EN_US;
            } else
            {
                throw new InvalidOperationException("Manually configured Voice cannot perform AutoConfig method");
            }
        }
    }
}
