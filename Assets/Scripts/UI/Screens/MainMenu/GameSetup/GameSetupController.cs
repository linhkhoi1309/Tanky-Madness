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
            GameSetupEvents.StartButtonPressed += OnStartButtonPressed;
        }

        void OnDestroy()
        {
            GameSetupEvents.GamemodeSelected -= OnGamemodeSelected;
            GameSetupEvents.StartButtonPressed -= OnStartButtonPressed;
        }

        private void OnGamemodeSelected(Gamemode gamemode)
        {
            _currentGamemode = gamemode;
        }

        private void OnStartButtonPressed()
        {
            SceneController.Instance.Load("GameplayScene", "GameplayUIScene");
        }

    }
}