using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kutil.Formats.Result;

namespace Kutil.Analizer
{
    /// <summary>
    /// 형태소 분석기 클래스의 인터페이스. 실사용되는 형태소 분석기의 클래스 MorphAnalizer는 이 인터페이스를 구현합니다.
    /// </summary>
    interface IMorphAnalizer : IDisposable
    {

        /// <summary>
        /// 형태소 분석기의 결과는 Kutil의 KResult로 변환되어 저장되어야 합니다.
        /// </summary>
        public KResult Analyze(string text);


    }
}
