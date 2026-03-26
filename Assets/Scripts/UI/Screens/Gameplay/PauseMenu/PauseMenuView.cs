using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Screens.Gameplay.PauseMenu
{
    public class PauseMenuView : UIView
    {

        private const string _resumeButtonId = "resume-button";
        private const string _settingsButtonId = "settings-button";
        private const string _mainMenuButtonId = "main-menu-button";

        private Button _resumeButton;
        private Button _settingsButton;
        private Button _mainMenuButton;

        public PauseMenuView(VisualElement parent, VisualTreeAsset asset) : base(parent, asset) { }

        protected override void SetVisualElements()
        {
            _resumeButton = Root.Q<Button>(_resumeButtonId);
            _settingsButton = Root.Q<Button>(_settingsButtonId);
            _mainMenuButton = Root.Q<Button>(_mainMenuButtonId);
        }

        protected override void BindInternalEvents()
        {
            BindClick(_resumeButton, () => PauseMenuEvents.ResumeButtonPressed?.Invoke());
            BindClick(_settingsButton, () => PauseMenuEvents.SettingsButtonPressed?.Invoke());
            BindClick(_mainMenuButton, () => PauseMenuEvents.MainMenuButtonPressed?.Invoke());
        }

    }
}