using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSignUp.classes
{
    static class Config
    {
        public static readonly char[] DisallowedCharacters = { ',' };

        public const int MaxChars = 32;
        public const int MinChars = 5;
    }
}
