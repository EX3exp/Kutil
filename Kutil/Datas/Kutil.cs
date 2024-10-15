using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutil
{
    
    public static class Datas
    {
        /// <summary>
        /// First hangeul consonants, ordered in unicode sequence.
        /// <br/><br/>유니코드 순서대로 정렬된 한국어 초성들입니다.
        /// </summary>
        public const string FIRST_CONSONANTS = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        /// <summary>
        /// Middle hangeul vowels, ordered in unicode sequence.
        /// <br/><br/>유니코드 순서대로 정렬된 한국어 중성들입니다.
        /// </summary>
        public const string MIDDLE_VOWELS = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";

        /// <summary>
        /// Last hangeul consonants, ordered in unicode sequence.
        /// <br/><br/>유니코드 순서대로 정렬된 한국어 종성들입니다.
        /// </summary>
        public const string LAST_CONSONANTS = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ"; // The first blank(" ") is needed because Hangeul may not have lastConsonant.

        /// <summary>
        /// unicode index of 가
        /// </summary>
        public const ushort HANGEUL_UNICODE_START = 0xAC00;

        /// <summary>
        /// unicode index of 힣
        /// </summary>
        public const ushort HANGEUL_UNICODE_END = 0xD79F;

        /// <summary>
        /// Confirms if input string is hangeul.
        /// <br/><br/>입력 문자열이 한글인지 확인합니다.
        /// </summary>
        /// <param name = "character"> A string of Hangeul character. 
        /// <br/>(Example: "가")</param>
        /// <returns> Returns true when input string is Hangeul, otherwise false. </returns>
        public static bool IsHangeul(char character)
        {
            ushort unicodeIndex;
            bool isHangeul;
            if (character != '\0')
            {
                // Automatically deletes ! from start.
                // Prevents error when user uses ! as a phonetic symbol.  
                unicodeIndex = Convert.ToUInt16(character);
                isHangeul = !(unicodeIndex < HANGEUL_UNICODE_START || unicodeIndex > HANGEUL_UNICODE_END);
            }
            else
            {
                isHangeul = false;
            }

            return isHangeul;
        }
    }
}
