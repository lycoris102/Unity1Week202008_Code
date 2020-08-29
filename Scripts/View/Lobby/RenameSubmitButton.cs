using System;
using Lycoris102.Unity1Week202008.View.Lobby.Interface;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class RenameSubmitButton : UIBehaviour, INameHandler
    {
        [SerializeField] private TMP_InputField InputField = default;

        public IObservable<string> OnSetNameAsObservable()
            => this.OnPointerDownAsObservable().Select(_ => InputField.text);
    }
}