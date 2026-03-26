using UnityEngine;

namespace Assets.Scripts.UI.Screens.MainMenu.Home
{
    public class HomeController : MonoBehaviour
    {

        private Gamemode _currentGamemode = Gamemode.Singleplayer;

        void OnEnable()
        {
            HomeEvents.ExitButtonPressed += OnExitButtonPressed;
        }

        void OnDestroy()
        {
            HomeEvents.ExitButtonPressed -= OnExitButtonPressed;
        }

        private void OnExitButtonPressed()
        {
            Application.Quit();
        }

    }
}