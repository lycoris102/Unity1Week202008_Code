using System.Collections.Generic;
using System.Linq;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Struct;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class ResultEntity : IResultEntity
    {
        public List<ResultData> List { get; } = new List<ResultData>();

        public void Add(ResultData result)
            => List.Add(result);

        public int CalcTotalTime()
            => List.Sum(x => x.Time);
    }
}