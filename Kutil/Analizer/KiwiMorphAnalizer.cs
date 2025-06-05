using NetKiwi;
using Kutil.Formats.Result;
using NetKiwi.Backend;
using System.Text;

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
        public KResult Analyze(string text)
        {
            using (SharpKiwi kiwi = new SharpKiwi())
            {
                Result[] res = kiwi.Analyze(text);
                List<KSmallResult> smallResults = new List<KSmallResult>();

                int currentSentPos = 0;
                int prevsentPos = 0;
                foreach (var item in res.Select((value, index)=> (value, index)))
                {
                    List<KToken> tokens = new List<KToken> ();

                    for (int i = 0; i < item.value.morphs.Length; i++)
                    {
                        Token t = item.value.morphs[i];
                        currentSentPos = unchecked((int)t.sentPosition);

                        if (currentSentPos != prevsentPos || i == item.value.morphs.Length - 1)
                        {
                            smallResults.Add(new KSmallResult(tokens.ToArray()));
                            tokens.Clear();
                        }
                        
                        tokens.Add(new KToken(t.form, unchecked((int)t.lineNumber), unchecked((int)t.sentPosition), unchecked((int)t.wordPosition), unchecked((int)t.chrPosition), unchecked((int)t.length), KiwiMorphs2Kutil(t.tag)));
                        prevsentPos = currentSentPos;
                    }

                    
                    
                }
                KResult result = new KResult(text, smallResults.ToArray());

                return result;
            }
        }

        public void Dispose()
        {
            
        }



        private static readonly Dictionary<string, Morphs> ConvTableKiwi = new Dictionary<string, Morphs>()
        {
            {"NNG", Morphs.S_GeneralNoun},
            {"NNP", Morphs.S_ProperNoun },
            {"NNB", Morphs.S_BoundNoun },
            {"NR" , Morphs.S_Numeral},
            {"NP", Morphs.S_Pronoun },

            {"VV", Morphs.P_Verb },
            {"VA", Morphs.P_Adjective },
            {"VV-R", Morphs.P_Verb },
            {"VA-R", Morphs.P_Adjective },
            {"VV-I", Morphs.P_Verb },
            {"VA-I", Morphs.P_Adjective },

            {"VX", Morphs.P_AuxiliaryVerb },
            {"VX-R", Morphs.P_AuxiliaryVerb },
            {"VX-I", Morphs.P_AuxiliaryVerb },
            {"VCP", Morphs.P_PositiveDemonstrative },
            {"VCN", Morphs.P_NegativeDemonstrative },

            {"MM", Morphs.M_Determiner },

            {"MAG", Morphs.M_GeneralAdverb },
            {"MAJ", Morphs.M_ConjunctiveAdverb },

            {"IC", Morphs.O_Exclamation },

            {"JKS", Morphs.T_SubjectMarker },
            {"JKC", Morphs.T_SubjectMarker },
            {"JKG", Morphs.T_DeterminerMarker },
            {"JKO", Morphs.T_ObjectMarker },
            {"JKB", Morphs.T_AdverbialMarker },
            {"JKV", Morphs.T_VocativeMarker },
            {"JKQ", Morphs.T_QuotationMarker },
            {"JX", Morphs.T_AuxiliaryMarker },
            {"JC", Morphs.T_ConjunctiveMarker },

            {"EP", Morphs.E_PrefinalEnding},
            {"EF", Morphs.E_FinalEnding },
            {"EC", Morphs.E_ConjunctiveEnding },
            {"ETN", Morphs.E_NounTransitiveEnding },
            {"ETM", Morphs.E_DeterminerTransitiveEnding },

            {"XPN", Morphs.A_SubstantivePrefix },

            {"XSN", Morphs.A_NounDevirationalSuffix },
            {"XSV", Morphs.A_VerbDevirationalSuffix },
            {"XSA", Morphs.A_AdjectiveDevirationalSuffix },
            {"XSA-R", Morphs.A_AdjectiveDevirationalSuffix },
            {"XSA-I", Morphs.A_AdjectiveDevirationalSuffix },
            {"XSM", Morphs.A_AdverbialDerivationalSuffix },

            {"XR", Morphs.R_Root },

            {"SF", Morphs.M_Terminal},
            {"SP", Morphs.M_Separator },
            {"SS", Morphs.M_Quotation },
            {"SSO", Morphs.M_Quotation },
            {"SSC", Morphs.M_Quotation },
            {"SE", Morphs.M_Elipsis },
            {"SO", Morphs.M_Dash },
            {"SW", Morphs.M_Others },
            {"SL", Morphs.M_Alphabet },
            {"SH", Morphs.M_ChineseCharacter },
            {"SN", Morphs.M_Number },
            {"SB", Morphs.M_BulletPoint },

            {"UN", Morphs.N_Unknown },
            {"W_URL", Morphs.N_Others },
            {"W_EMAIL", Morphs.N_Others },
            {"W_HASHTAG", Morphs.N_Others },
            {"W_MENTION", Morphs.N_Others },
            {"W_SERIAL", Morphs.N_Others },
            {"W_EMOJI", Morphs.N_Others },

            {"Z_CODA", Morphs.N_Others },
            {"Z_SIOT", Morphs.N_Siot }




        };

        /// <summary>
        /// 변환 테이블에 존재하지 않는 형태소 태그를 변환 시도했을 때 발생하는 예외.
        /// </summary>
        private class InValidKiwiTagException : Exception
        {
            /// <param name="erroredTag">오류가 발생한 Tag</param>
            public InValidKiwiTagException(string erroredTag)
                : base($"Tag '{erroredTag}' not exists in Kutil's conversion table. '{erroredTag}'는 Kutil 내의 변환 테이블에 존재하지 않는 태그입니다.")
            {
            }
        }

        /// <summary>
        /// 키위의 형식으로 표현된 품사 태그를 Kutil 공용으로 변환.
        /// </summary>
        /// <returns></returns>
        private static Morphs KiwiMorphs2Kutil(string tag)
        {
            if (ConvTableKiwi.TryGetValue(tag, out var converted))
            {
                return converted;
            }
            else
            {
                throw new InValidKiwiTagException(tag);
            }
             
        }
    }
}
