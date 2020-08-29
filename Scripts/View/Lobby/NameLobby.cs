using Lycoris102.Unity1Week202008.View.Lobby.Interface;
using TMPro;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class NameLobby : UIBehaviour, INameRenderer
    {
        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());

        public void Render(string Name)
            => Text.text = $"ようこそ {Name} さん";
    }
}