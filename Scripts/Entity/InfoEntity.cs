using System;
using Lycoris102.Unity1Week202008.Entity.Interface;
using UniRx;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class InfoEntity : IInfoEntity
    {
        private string InfoText { get; set; }
        private ISubject<string> TextSubject = new ReplaySubject<string>();

        public void Set(string text)
        {
            InfoText = text;
            TextSubject.OnNext(text);
        }

        public IObservable<string> OnUpdateAsObservable()
            => TextSubject;
    }
}