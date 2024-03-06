using NUnit.Framework;
using System;
using System.Linq;

namespace NunitAutomationFramework.Helper.Generators
{
    public static class TextGenerator
    {
        private static Random _random = new Random();
        const string UPPER_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string LOWER_ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        const string NUMBER = "0123456789";
        const string SPECIAL_CHARACTER = "@#$%^";

        public static string GenerateTextBasedOnAvailableCharacter(int length, string chars)
        {
             return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        public static string GenerateAlphaText(int length)
        {
            const string chars = UPPER_ALPHABET + LOWER_ALPHABET;
            return GenerateTextBasedOnAvailableCharacter(length, chars);
        }

        public static string GeneratePassword(int length)
        {
            var upperCaseChar = GenerateTextBasedOnAvailableCharacter(1, UPPER_ALPHABET);
            var lowerCaseChar = GenerateTextBasedOnAvailableCharacter(1, LOWER_ALPHABET);
            var specialChar = GenerateTextBasedOnAvailableCharacter(1, SPECIAL_CHARACTER);
            var numbericChar = GenerateTextBasedOnAvailableCharacter(1, NUMBER);
            var remainingChars = GenerateTextBasedOnAvailableCharacter(length -4, UPPER_ALPHABET + LOWER_ALPHABET + NUMBER);
            return $"{remainingChars}{upperCaseChar}{specialChar}{lowerCaseChar}{numbericChar}";
        }
    }
}