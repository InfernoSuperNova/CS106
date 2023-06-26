using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSignUp.classes
{
    static class Config
    {
        public static readonly char[] DISALLOWED_CHARS = { ',', '\n' };

        public const int MAX_CHARS = 32;
        public const int MIN_CHARS = 5;

        public const string ROOT_FOLDER = @".\Database\Projects";
    }
}
