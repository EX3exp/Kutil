using Kutil.Analizer;
using Kutil.Formats.Korean;

namespace Kutil.G2p
{
    
    public static class Variator
    {
        /// <summary>
        /// Variates input word. e.g) "밥을" -> "바블" ,  "먹다" -> "먹따"
        /// </summary>
        /// <param name="word">음운변동된 단어.</param>
        /// <param name="analizerType">음운변동에 사용할 형태소 분석기의 종류. 기본값은 Kiwi(키위 형태소 분석기)입니다.</param>
        /// <returns></returns>
        public static string Variate(string word, AnalizerType analizerType=AnalizerType.Kiwi)
        {
            try
            {
                string text = word;
                //Word kWord = new Word(text, analizerType);
                return text;
                
            }
           
            catch (Exception e)
            {
                Console.WriteLine("Error in Variator.Variate()");
                Console.WriteLine("Input: " + word);
                
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return word;
            }
            

        }
    }
}
