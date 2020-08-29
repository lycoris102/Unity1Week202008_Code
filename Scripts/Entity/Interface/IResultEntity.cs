using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Struct;

namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface IResultEntity
    {
        List<ResultData> List { get; }
        void Add(ResultData result);
        int CalcTotalTime();
    }
}