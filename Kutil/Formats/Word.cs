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
        ///  속도 확보를 위해 res는 이 클래스에서 한 번만 생성, 이후 Sentence, Word 클래스들을 재귀적으로 생성.
        /// </summary>
        bool IsValid = false;
        KResult Result;
        Sentence[] Sentences;
        string paragraph;
        public Paragraph(string paragraph, AnalizerType analizerType = AnalizerType.Kiwi, string? modelPath = null)
        {
            this.paragraph = paragraph;
            if (paragraph.Split(' ').Length >= 1)
            {
                using (MorphAnalizer Analizer = new MorphAnalizer(analizerType, modelPath))
                {
                    Result = Analizer.Analyze(paragraph);
                }
                IsValid = true;
                Sentences = new Sentence[Result.SentLength];
                
                int maxSentenceLen = Result.SentLength;

                foreach (var k in Result.SentResults.Select((value, index)=>(value, index)))
                {
                    if (string.IsNullOrWhiteSpace(paragraph))
                    {
                        Console.WriteLine("문단이 빈 문자열입니다.");
                    }
                    else
                    {
                        string sent = "";
                        int firstPos = k.value.KTokens[0].charPosition;
                        // 첫 번째 문장의 시작 인덱스 ~ 다음 문장의 시작 인덱스 - 1 로 각 문장들을 수집합니다.
                        if (k.index == maxSentenceLen - 1)
                        {
                            sent = paragraph.Substring(firstPos, paragraph.Length - 1 - firstPos + 1);
                        }
                        else
                        {
                            int nextSentStartPos = Result.SentResults[k.index + 1].KTokens[0].charPosition;
                            sent = paragraph.Substring(firstPos, nextSentStartPos - 1 - firstPos + 1);
                        }

                            Sentences[k.index] = new Sentence(sent.Trim(), k.value);
                    }
                    
                }

            }
            else
            {
                IsValid = false; // empty sentence
            }

        }

        public void Print() // 디버그용 함수.
        {
            if (!IsValid)
            {
                Console.WriteLine("Result가 null입니다");
            }
            else
            {
                int i = 1;
                foreach (KSmallResult k in Result.SentResults)
                {
                    Console.WriteLine($"-- {i}번째 문장");
                    Console.WriteLine(Sentences[i-1].ToString());
                    foreach (KToken t in k.KTokens)
                    {
                        
                        Console.Write($"form={t.form}   ");
                        Console.Write($"morph={t.morph}   ");
                        Console.Write($"lineNo={t.lineNo}   ");
                        Console.Write($"sentPos={t.sentPosition}   ");
                        Console.Write($"wordPos={t.wordPosition}   ");
                        Console.Write($"charPos={t.charPosition}   ");
                        Console.Write($"length={t.length}");
                        Console.Write("\n");

                    }
                    Console.WriteLine("---\n");
                    i++;
                }
            }
                
        }

    }

    public class Sentence
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsValid = false;
        KSmallResult res;
        Word[] Words;
        string sentence;
        public Sentence()
        {
            IsValid=false;

        }
        public Sentence(string sentence, KSmallResult res)
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

        public override string ToString()
        {
            return sentence;
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
            YongeonNum = 0;

            if (IsValid)
            {
                //if (res.length < 2)
                //{
                //    YongeonNum = 0;
                //    return false;
                //}
                //else if (res.morph.length > 2) {
                //    List<int> Yongeons = res.morphs.Where(m => m.tag.StartsWith("V")).Select((y, index) => index).ToList();
                //    if (Yongeons.Count > 0)
                //    {
                //        YongeonNum = Yongeons.Count;
                //        return true;
                //    }
                //    else
                //    {
                //        YongeonNum = 0;
                //        return false;
                //    }
                //}
                //else
                //{
                //    YongeonNum = 0;
                //    return false;
                //}
                //TODO
                return true;
                
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
