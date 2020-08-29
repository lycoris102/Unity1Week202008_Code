using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class LoadLobby : MonoBehaviour
    {
        // Signalで読み込む
        public void Load()
        {
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }
    }
}