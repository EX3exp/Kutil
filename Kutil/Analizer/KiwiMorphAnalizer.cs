using NetKiwi;
using Kutil.Formats.Result;
using NetKiwi.Backend;

namespace Kutil.Analizer
{


    /// <summary>
    /// Kiwi 형태소 분석기 클래스입니다.
    /// </summary>
    public class KiwiMorphAnalizer : IMorphAnalizer
    {
        public KiwiMorphAnalizer(string? modelPath=null)
        {
            
        }
        public KResult[] Analyze(string sentence)
        {
            using (SharpKiwi kiwi = new SharpKiwi())
            {
                Result[] res = kiwi.Analyze(sentence);
                KResult[] results = new KResult[res.Length];
                foreach (var item in res.Select((value, index)=> (value, index)))
                {
                    KToken[] tokens = new KToken[item.value.morphs.Length];
                    foreach (Token t in item.value.morphs)
                    {
                        results[item.index].KTokens = tokens;
                    }
                }
                return results;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
