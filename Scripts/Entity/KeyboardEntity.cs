using System.Collections.Generic;
using System.Linq;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Utility;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class KeyboardEntity : IKeyboardEntity
    {
        public string Text { get; private set; } = "";
        public int UnlockKeyIndex { get; private set; }

        private int MaxLength { get; }
        private List<int> OwnerIndexList = new List<int>();
        private int OwnerEnableIndex;

        public KeyboardEntity(int maxLength)
        {
            MaxLength = maxLength;
        }

        public void Add(string text)
        {
            if (Text.Length >= MaxLength) return;
            Text += text;
        }

        public void Delete()
            => Text = "";

        public bool IsEmpty()
            => Text == "";

        public void InitializeOwner(int keyCount)
        {
            OwnerIndexList = Enumerable.Range(0, keyCount).Shuffle().ToList();
            UnlockKeyIndex = 0;
        }

        public void IncreaseUnlockKeyIndex()
            => UnlockKeyIndex++;

        public List<int> EnableOwner(int count)
        {
            var list = OwnerIndexList.Skip(OwnerEnableIndex).Take(count).ToList();
            OwnerEnableIndex += count;
            return list;
        }
    }
}