using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using NetKiwi;
using NetKiwi.Backend;
using static Kutil.Datas;


namespace Kutil.Formats
{
    public class Word
    {
        /// <summary>
        ///  Becomes true when initialized with a valid word.
        /// </summary>
        bool IsValid = false;
        Result res;

        private string word;
        public Hangeul[] Hangeuls;

        class WordHasSpaceException : Exception
        {
            public WordHasSpaceException(string targetWord): base($"[Kutil] Word '{targetWord}' has spaces in it.")
            {

            }
        }

        public Word(string word, string? kiwiModelPath=null)
        {
            if (word.Split(' ').Length == 1)
            {
                this.word = word;
                Hangeuls = new Hangeul[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    Hangeuls[i] = new Hangeul(word[i]);
                }
                using (SharpKiwi Kiwi = new SharpKiwi(kiwiModelPath))
                {
                    res = Kiwi.Analyze(word)[0];
                }

                IsValid = true;
            }
            else
            {
                IsValid = false;
                throw new WordHasSpaceException(word);
            }
        }

        /// <summary>
        /// 해당 어절이 용언 어간을 가졌는지 확인합니다.
        /// </summary>
        /// <returns></returns>
        public bool HasYongeonEogan(out int YongeonNum)
        {
            if (IsValid)
            {
                if (res.morphs.Length < 2)
                {
                    YongeonNum = 0;
                    return false;
                }
                else if (res.morphs.Length > 2) {
                    List<int> Yongeons = res.morphs.Where(m => m.tag.StartsWith("V")).Select((y, index) => index).ToList();
                    if (Yongeons.Count > 0)
                    {
                        YongeonNum = Yongeons.Count;
                        return true;
                    }
                    else
                    {
                        YongeonNum = 0;
                        return false;
                    }
                }
                else
                {
                    YongeonNum = 0;
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException("Word is not valid.");
            }
        }


        /// <summary>
        /// target을 첫소리로 가지고 있는지 확인합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool CheckFirstSoundIs(char target)
        {
            if (IsValid)
            {
                return Hangeuls[0].IsFirstIs(target);
            }
            else
            {
                throw new InvalidOperationException("Word is not valid.");
            }
        }


    }
}
