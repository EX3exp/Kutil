using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static Kutil.Datas;


namespace Kutil.Formats
{
    public class Hangeul
    {
        /// <summary>
        ///  Becomes true when initialized with a valid Hangeul character.
        /// </summary>
        bool IsValid = false;

        /// <summary>
        /// First hangeul consonant. e.g) 'ㅎ' in '한'
        /// </summary>
        char First; // ㅎ

        /// <summary>
        /// Middle hangeul vowel. e.g) 'ㅏ' in '한'
        /// </summary>
        char Middle; // ㅏ

        /// <summary>
        /// Last hangeul consonant. e.g) 'ㄴ' in '한'
        /// </summary>
        char Last; // ㄴ

        /// <summary>
        /// Returns complete Hangeul character. e.g) '한' <br></br>
        /// Only returns if Hangeul is valid, otherwise returns '\0'.
        /// </summary>
        public char Merged
        {
            get {
                if (IsValid)
                {
                    return Merge(First, Middle, Last);
                }
                else
                {
                    return char.MinValue;
                }
            }
            set { }
        }

        public bool IsFirstIs(char target)
        {
            return First == target;
        }

        public bool IsFirstIsIn(char[] targets)
        {
            return targets.Contains(First);
        }

        public bool IsMiddleIs(char target)
        {
            return Middle == target;
        }

        public bool IsMiddleIsIn(char[] targets)
        {
            return targets.Contains(Middle);
        }

        public bool IsLastIs(char target)
        {
            return Last == target;
        }

        public bool IsLastIsIn(char[] targets)
        {
            return targets.Contains(Last);
        }


        /// <summary>
        /// Separates complete hangeul string's first character in three parts - firstConsonant(초성), middleVowel(중성), lastConsonant(종성).
        /// <br/>입력된 문자열의 0번째 글자를 초성, 중성, 종성으로 분리합니다.
        /// </summary>
        /// <param name="character"> A string of complete Hangeul character.
        /// <br/>(Example: '냥') 
        /// </param>
        /// <returns>{firstConsonant(초성), middleVowel(중성), lastConsonant(종성)}
        /// (ex) {'ㄴ', 'ㅑ', 'ㅇ'}
        /// </returns>
        public static Hashtable Separate(char character)
        {

            int hangeulIndex; // unicode index of hangeul - unicode index of '가' (ex) '냥'

            int firstConsonantIndex; // (ex) 2
            int middleVowelIndex; // (ex) 2
            int lastConsonantIndex; // (ex) 21

            char firstConsonant; // (ex) 'ㄴ'
            char middleVowel; // (ex) 'ㅑ'
            char lastConsonant; // (ex) 'ㅇ;

            Hashtable separatedHangeul; // (ex) {[0]: 'ㄴ', [1]: 'ㅑ', [2]: 'ㅇ'}


            hangeulIndex = Convert.ToUInt16(character) - HANGEUL_UNICODE_START;

            // seperates lastConsonant
            lastConsonantIndex = hangeulIndex % 28;
            hangeulIndex = (hangeulIndex - lastConsonantIndex) / 28;

            // seperates middleVowel
            middleVowelIndex = hangeulIndex % 21;
            hangeulIndex = (hangeulIndex - middleVowelIndex) / 21;

            // there's only firstConsonant now
            firstConsonantIndex = hangeulIndex;

            // separates character
            firstConsonant = FIRST_CONSONANTS[firstConsonantIndex];
            middleVowel = MIDDLE_VOWELS[middleVowelIndex];
            lastConsonant = LAST_CONSONANTS[lastConsonantIndex];

            separatedHangeul = new Hashtable()
            {
                [0] = firstConsonant,
                [1] = middleVowel,
                [2] = lastConsonant
            };


            return separatedHangeul;
        }

        /// <summary>
        /// merges separated hangeul into complete hangeul. (Example: {[offset + 0]: "ㄱ", [offset + 1]: "ㅏ", [offset + 2]: " "} => "가"})
        /// <para>자모로 쪼개진 한글을 합쳐진 한글로 반환합니다.</para>
        /// </summary>
        /// <param name="separated">separated Hangeul. </param>
        /// <returns>Returns complete Hangeul Character.</returns>
        public static char Merge(char first, char mid, char last)
        {

            int firstConsonantIndex; // (ex) 2
            int middleVowelIndex; // (ex) 2
            int lastConsonantIndex; // (ex) 21

            if (first == ' ') { first = 'ㅇ'; }

            firstConsonantIndex = FIRST_CONSONANTS.IndexOf(first); // 초성 인덱스
            middleVowelIndex = MIDDLE_VOWELS.IndexOf(mid); // 중성 인덱스
            lastConsonantIndex = LAST_CONSONANTS.IndexOf(last); // 종성 인덱스

            int mergedCode = HANGEUL_UNICODE_START + (firstConsonantIndex * 21 + middleVowelIndex) * 28 + lastConsonantIndex;

            char result = Convert.ToChar(mergedCode);
            
            return result;
        }
        public Hangeul (char first, char mid, char last)
        {

            this.First = first;
            this.Middle = mid;
            this.Last = last;
            this.IsValid = true;
        }

        public Hangeul(char character)
        {
            Hashtable separated = Separate(character);
            this.First = (char)separated[0];
            this.Middle = (char)separated[1];
            this.Last = (char)separated[2];
            this.IsValid = true;
        }
    }
}
