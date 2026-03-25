using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Screens.MainMenu.GameSetup
{
    public class GameSetupController : MonoBehaviour
    {

        private Gamemode _currentGamemode = Gamemode.Singleplayer;

        void OnEnable()
        {
            GameSetupEvents.GamemodeSelected += OnGamemodeSelected;
            GameSetupEvents.StartButtonClicked += OnStartButtonClicked;
        }

        void OnDestroy()
        {
            GameSetupEvents.GamemodeSelected -= OnGamemodeSelected;
            GameSetupEvents.StartButtonClicked -= OnStartButtonClicked;
        }

        private void OnGamemodeSelected(Gamemode gamemode)
        {
            _currentGamemode = gamemode;
        }

        private void OnStartButtonClicked()
        {
            SceneManager.LoadScene("GameplayScene");
        }

    }
}