using Lycoris102.Unity1Week202008.Entity.Interface;
using naichilab;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class NameEntity : INameEntity
    {
        private const string NameKey = "Name";

        public string Load()
            => EncryptedPlayerPrefs.LoadString(NameKey, $"ゲスト{Random.Range(0, 1000):000}");

        public void Save(string name)
            => EncryptedPlayerPrefs.SaveString(NameKey, name);
    }
}