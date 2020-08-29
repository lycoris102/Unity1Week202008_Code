using System;
using Lycoris102.Unity1Week202008.Struct;

namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IStageResultHandler
    {
        IObservable<ResultData> OnSetAsObservable();
    }
}