using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kutil.Formats.Result;
using Kutil.Analizer;



namespace Kutil.Analizer
{
    /// <summary>
    /// Kutil에서 사용 가능한 형태소 분석기의 종류들입니다. 각 상응하는 클래스명에서 MorphAnalizer를 제외한 부분의 문자열이어야 합니다.
    /// </summary>
    public enum AnalizerType
    {
        Kiwi
    }

    /// <summary>
    /// 실사용되는 형태소 분석기 클래스입니다.
    /// </summary>
    public class MorphAnalizer: IMorphAnalizer
    {

        /// <summary>
        /// 존재하지 않는 형태소 분석기를 생성 시도했을 시 발생하는 예외.
        /// </summary>
        private class NotImplementedAnalizerException : Exception
        {
            /// <summary>
            /// 존재하지 않는 형태소 분석기를 생성 시도했을 시 발생하는 예외.
            /// </summary>
            /// <param name="erroredType">오류가 발생한 Analizer의 종류</param>
            public NotImplementedAnalizerException(AnalizerType erroredType)
                : base($"{erroredType}MorphAnalizer not exists. {erroredType}MorphAnalizer는 존재하지 않는 형태소 분석기입니다.")
            {
            }
        }
        IMorphAnalizer _analizer;
        /// <summary>
        /// 형태소 분석기.
        /// </summary>
        /// <param name="analizerType">사용할 형태소 분석기의 종류. 해당 enum에 MorphAnalizer을 붙인 값이 생성할 형태소 분석기의 클래스명이 됩니다.</param>
        /// <param name="modelPath">형태소 분석기 모델의 경로. 별도로 지정할 필요가 없을 경우 null로 둡니다.</param>
        public MorphAnalizer(AnalizerType analizerType, string? modelPath=null)
        {

            Type type = Type.GetType($"Kutil.Analizer.{analizerType}MorphAnalizer");
            if (type is null)
            {
                throw new NotImplementedAnalizerException(analizerType);
            }
            else
            {
                _analizer = Activator.CreateInstance(type, modelPath) as IMorphAnalizer;
            }
            
        }

        public KResult[] Analyze(string sentence)
        {
            return _analizer.Analyze(sentence);
        }

        public void Dispose()
        {
            _analizer.Dispose();
        }
    }
}
