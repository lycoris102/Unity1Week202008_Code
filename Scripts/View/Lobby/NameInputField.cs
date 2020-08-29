using Lycoris102.Unity1Week202008.View.Lobby.Interface;
using TMPro;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class NameInputField : UIBehaviour, INameRenderer
    {
        private TMP_InputField inputField = default;
        private TMP_InputField InputField => inputField ?? (inputField = GetComponent<TMP_InputField>());

        public void Render(string Name)
            => InputField.text = Name;
    }
}