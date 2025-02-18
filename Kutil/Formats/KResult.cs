using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutil.Formats.Result
{

    /// <summary>
    /// 형태소 분석 결과 태그. 
    /// 사용한 형태소 분석기가 다르더라도, 최종적으로 얻어진 형태소 분석 결과는 이 태그를 이용해야만 합니다.
    /// </summary>
    public enum Morphs
    {
        // 체언_Sustantive
        [Description("일반명사")] S_GeneralNoun,//일반명사
        [Description("고유명사")] S_ProperNoun,//고유명사
        [Description("의존명사")] S_BoundNoun,//의존명사
        [Description("대명사")] S_Pronoun,//대명사
        [Description("수사")] S_Numeral,//수사

        // 용언_Predicate
        [Description("동사")] P_Verb,//동사
        [Description("형용사")] P_Adjective,//형용사
        [Description("보조용언")] P_AuxiliaryVerb,//보조용언
        [Description("긍정지시사")] P_PositiveDemonstrative,//긍정지시사
        [Description("부정지시사")] P_NegativeDemonstrative,//부정지시사

        // 수식언_Modifier
        [Description("관형사")] M_Determiner,//관형사
        [Description("일반부사")] M_GeneralAdverb,//일반부사
        [Description("접속부사")] M_ConjunctiveAdverb,//접속부사

        // 독립언_Orthotone
        [Description("감탄사")] O_Exclamation,//감탄사

        // 관계언(조사+접속사)_Particle
        [Description("주격조사")] P_SubjectMarker,//주격조사
        [Description("보격조사")] P_ComplementMarker,//보격조사
        [Description("관형격조사")] P_DeterminerMarker,//관형격조사
        [Description("목적격조사")] P_ObjectMarker,//목적격조사
        [Description("부사격조사")] P_AdverbialMarker,//부사격조사
        [Description("호격조사")] P_VocativeMarker,//호격조사
        [Description("인용격조사")] P_QuotationMarker,//인용격조사
        [Description("보조격조사")] P_AuxiliaryMarker,//보조격조사
        [Description("접속조사")] P_ConjunctiveMarker,//접속조사

        // 어미_Ending
        [Description("선어말어미")] E_PrefinalEnding,//선어말어미
        [Description("종결어미")] E_FinalEnding,//종결어미
        [Description("접속어미")] E_ConjunctiveEnding,//접속어미
        [Description("명사형 전성어미")] E_NounTransitiveEnding,//명사형전성어미
        [Description("관형형 전성어미")] E_DeterminerTransitiveEnding,//관형형전성어미

        // 접사_Affix
        [Description("체언접두사")] A_SubstantivePrefix,//체언접두사
        [Description("명사파생접미사")] A_NounDevirationalSuffix,//명사파생접미사
        [Description("동사파생접미사")] A_VerbDevirationalSuffix,//동사파생접미사
        [Description("형용사파생접미사")] A_AdjectiveDevirationalSuffix,//형용사파생접미사
        [Description("부사파생접미사")] A_AdverbialDerivationalSuffix,//부사파생접미사

        // 어근_Root
        [Description("어근")] R_Root,//어근

        // 특수_Marks
        [Description("마침표")] M_Terminal,//마침표
        [Description("구분자")] M_Separator,//구분자
        [Description("인용부호")] M_Quotation,//인용부호
        [Description("인용부호 (여는 부호)")] M_OpenedQuotation,//인용부호(여는 부호)
        [Description("인용부호 (닫는 부호)")] M_ClosedQuotation,//인용부호(닫는 부호)
        [Description("줄임표")] M_Elipsis,//줄임표
        [Description("붙임표")] M_Dash,//붙임표
        [Description("기타 문자")] M_Others,//기타
        [Description("알파벳")] M_Alphabet,//알파벳
        [Description("숫자")] M_Number,//숫자
        [Description("한자")] M_ChineseCharacter,//한자
        [Description("글머리 기호")] M_BulletPoint,//글머리 기호

        // 기타_Non-specific
        [Description("알 수 없음")] N_Unknown,//알 수 없음
        [Description("기타")] N_Others,//기타

    }

    public struct KToken
    {
        /// <summary>
        /// 형태소 분석 결과의 형태소 문자열.
        /// </summary>
        public string form;
        /// <summary>
        /// 형태소 분석 결과.
        /// </summary>
        public Morphs morph;
        /// <summary>
        /// 형태소 분석 결과의 단어 인덱스.
        /// </summary>
        public uint wordPosition;
        /// <summary>
        /// 형태소 분석 결과의 문장 인덱스.
        /// </summary>
        public uint lineNo;

        public KToken(string form, uint lineNo, Morphs morph)
        {
            this.form = form;
            this.lineNo = lineNo;
            this.morph = morph;
            this.wordPosition = 0;
        }
    }
    /// <summary>
    /// 형태소 분석기의 결과 객체입니다.
    /// </summary>
    public struct KResult
    {
        public KToken[] KTokens;

        public KResult(KToken[] tokens)
        {
            KTokens = tokens;
        }
    }
}
