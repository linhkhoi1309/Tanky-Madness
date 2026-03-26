using Assets.Scripts.UI.Screens.Gameplay.HUD;
using Assets.Scripts.UI.Screens.Gameplay.PauseMenu;
using Assets.Scripts.UI.Screens.MainMenu.GameSetup;
using Assets.Scripts.UI.Screens.MainMenu.Home;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Gameplay
{
    public class GameplayUIManager : UIManager
    {

        override protected void SetupViews()
        {
            ShowView<HUDView>();
        }

        protected override void BindExternalEvents()
        {
            HUDEvents.PauseButtonPressed += OnPauseButtonPressed;
            PauseMenuEvents.ResumeButtonPressed += OnResumeButtonPressed;
            PauseMenuEvents.SettingsButtonPressed += OnSettingsButtonPressed;
        }

        private void OnDisable()
        {
            HUDEvents.PauseButtonPressed -= OnPauseButtonPressed;
            PauseMenuEvents.ResumeButtonPressed -= OnResumeButtonPressed;
            PauseMenuEvents.SettingsButtonPressed -= OnSettingsButtonPressed;
        }

        private void OnPauseButtonPressed()
        {
            ShowView<PauseMenuView>();
        }

        private void OnResumeButtonPressed()
        {
            ShowView<HUDView>();
        }

        private void OnSettingsButtonPressed()
        {
            
        }

    }
}