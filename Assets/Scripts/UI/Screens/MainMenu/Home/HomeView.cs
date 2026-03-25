using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Screens.MainMenu.Home
{
    public class HomeView : UIView
    {

        private string _playButtonId = "play-button";
        private string _settingsButtonId = "settings-button";
        private string _exitButtonId = "exit-button";

        private Button _playButton;
        private Button _settingsButton;
        private Button _exitButton;

        public HomeView(VisualElement parent, VisualTreeAsset asset) : base(parent, asset) { }

        protected override void SetVisualElements()
        {
            _playButton = Root.Q<Button>(_playButtonId);
            _settingsButton = Root.Q<Button>(_settingsButtonId);
            _exitButton = Root.Q<Button>(_exitButtonId);
        }

        protected override void BindInternalEvents()
        {
            BindClick(_playButton, () => HomeEvents.PlayButtonClicked?.Invoke());
            BindClick(_settingsButton, () => HomeEvents.SettingsButtonClicked?.Invoke());
            BindClick(_exitButton, () => HomeEvents.ExitButtonClicked?.Invoke());
        }

    }
}