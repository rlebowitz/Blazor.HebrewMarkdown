using System.Text.RegularExpressions;

namespace Blazor.HebrewMarkdown.Components
{
    //https://stackoverflow.com/questions/4330951/how-to-detect-whether-a-character-belongs-to-a-right-to-left-language
    public static partial class StringExtensions
    {
        [GeneratedRegex("^\\p{IsHebrew}", RegexOptions.Compiled)]
        private static partial Regex HebrewRegex();
        private static Regex Hebrew { get; set; } = HebrewRegex();
        public static bool IsHebrew(this string s)
        {
            return !string.IsNullOrEmpty(s) && Hebrew.IsMatch(s);
        }

    }

}