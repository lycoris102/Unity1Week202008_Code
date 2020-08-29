using System.Collections.Generic;

namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface IKeyboardEntity
    {
        string Text { get; }
        int UnlockKeyIndex { get; }

        void Add(string text);
        void Delete();
        bool IsEmpty();
        void InitializeOwner(int keyCount);
        void IncreaseUnlockKeyIndex();
        List<int> EnableOwner(int count);
    }
}