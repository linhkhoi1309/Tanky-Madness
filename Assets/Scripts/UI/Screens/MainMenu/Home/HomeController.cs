using UnityEngine;

namespace Assets.Scripts.UI.Screens.MainMenu.Home
{
    public class HomeController : MonoBehaviour
    {

        private Gamemode _currentGamemode = Gamemode.Singleplayer;

        void OnEnable()
        {
            HomeEvents.ExitButtonClicked += OnExitButtonClicked;
        }

        void OnDestroy()
        {
            HomeEvents.ExitButtonClicked -= OnExitButtonClicked;
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

    }
}