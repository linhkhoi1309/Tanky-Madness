using Assets.Scripts.UI.Screens.MainMenu.GameSetup;
using Assets.Scripts.UI.Screens.MainMenu.Home;

namespace Assets.Scripts.UI.Screens.MainMenu
{
    public class MainMenuUIManager : UIManager
    {

        override protected void SetupViews()
        {
            ShowView<HomeView>();
        }

        protected override void BindExternalEvents()
        {
            HomeEvents.PlayButtonClicked += OnHomePlayButtonPressed;
            GameSetupEvents.BackButtonPressed += OnGameSetupBackButtonPressed;
        }

        private void OnDisable()
        {
            HomeEvents.PlayButtonClicked -= OnHomePlayButtonPressed;
            GameSetupEvents.BackButtonPressed -= OnGameSetupBackButtonPressed;
        }

        private void OnHomePlayButtonPressed()
        {
            ShowView<GameSetupView>();
        }

        private void OnGameSetupBackButtonPressed()
        {
            ShowView<HomeView>();
        }

    }
}