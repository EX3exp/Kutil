using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Kutil.Analizer;
using NetKiwi;
using NetKiwi.Backend;
using static Kutil.Datas;
using Kutil.Formats.Result;

// <summary>
//  문장 분석을 위한 클래스들을 포함합니다.
// </summary>
namespace Kutil.Formats.Korean
{
    public class Paragraph
    {
        /// <summary>
        ///  속도 확보를 위해 res는 이 클래스에서 한 번만 생성, 이후 Sentence, Word 클래스들을 재귀적으로 생성. 생성된 
        /// </summary>
        bool IsValid = false;
        KResult[] KResults;
        Sentence[] Sentences;
        string paragraph;
        public Paragraph(string paragraph, AnalizerType analizerType = AnalizerType.Kiwi, string? modelPath = null)
        {
            this.paragraph = paragraph;
            if (paragraph.Split(' ').Length == 1)
            {
                using (MorphAnalizer Analizer = new MorphAnalizer(analizerType, modelPath))
                {
                    KResults = Analizer.Analyze(paragraph);
                }
                IsValid = true;
                Sentence[] Sentences = new Sentence[KResults.Length];

                for (int i = 0; i < KResults.Length; i++)
                {
                    Sentences[i] = new Sentence(KResults[i]);
                }
            }
            else
            {
                IsValid = false;
            }

        }

    }

    public class Sentence
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsValid = false;
        KResult res;
        Word[] Words;
        string sentence;
        public Sentence(string sentence, KResult res)
        {
            this.sentence = sentence;
            if (sentence.Split(' ').Length == 1)
            {
                this.res = res;
                IsValid = true;
                int wordCount = sentence.Split().Length;
                // TODO Word 클래스에 어떻게 KToken을 넘겨줄 것인지 생각하기
                Word[] Words = new Word[wordCount];

                for (int i = 0; i < wordCount; i++)
                {
                    Words[i] = new Word(sentence.Split()[i], res.KTokens[i]);
                }

            }
            else
            {
                IsValid = false;
            }

        }
    }
    public class Word
    {
        /// <summary>
        ///  Becomes true when initialized with a valid word.
        /// </summary>
        bool IsValid = false;
        KToken res;

        private string word;
        public Hangeul[] Hangeuls;

        class WordHasSpaceException : Exception
        {
            public WordHasSpaceException(string targetWord): base($"[Kutil] Word '{targetWord}' has spaces in it.")
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word">분석 대상이 될 단어.</param>
        /// <param name="analizerType">형태소 분석기의 종류. 기본값은 키위 형태소 분석기(Kiwi)입니다.</param>
        /// <param name="modelPath">필요할 경우, 형태소 분석기 모델의 경로명. 딱히 필요 없을 경우 null로 두면 됩니다.</param>
        /// <exception cref="WordHasSpaceException"></exception>
        public Word(string word, KToken t)
        {
            if (word.Split(' ').Length == 1)
            {
                this.word = word;
                Hangeuls = new Hangeul[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    Hangeuls[i] = new Hangeul(word[i]);
                }
                res = t;

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
                if (res.KTokens.Length < 2)
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
