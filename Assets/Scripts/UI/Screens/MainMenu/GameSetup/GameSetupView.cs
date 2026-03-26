using System;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Screens.MainMenu.GameSetup
{
    public class GameSetupView : UIView
    {

        private const string _gamemodeToggleGroupId = "gamemode-toggle-group";
        private const string _singleplayerButtonId = "singleplayer-button";
        private const string _localMultiplayerButtonId = "local-multiplayer-button";
        private const string _onlineMultiplayerButtonId = "online-multiplayer-button";
        private const string _backButtonId = "back-button";
        private const string _startButtonId = "start-button";

        private ToggleButtonGroup _gamemodeToggleGroup;
        private Button _backButton;
        private Button _startButton;

        private int[] _indexPool = new int[64];

        public GameSetupView(VisualElement parent, VisualTreeAsset asset) : base(parent, asset) { }

        protected override void SetVisualElements()
        {
            _gamemodeToggleGroup = Root.Q<ToggleButtonGroup>(_gamemodeToggleGroupId);
            _backButton = Root.Q<Button>(_backButtonId);
            _startButton = Root.Q<Button>(_startButtonId);
        }

        protected override void BindInternalEvents()
        {
            BindChange<ToggleButtonGroupState>(_gamemodeToggleGroup, OnGamemodeToggleGroupSelectionChanged);
            BindClick(_backButton, () => GameSetupEvents.BackButtonPressed?.Invoke());
            BindClick(_startButton, () => GameSetupEvents.StartButtonPressed?.Invoke());
        }

        private void OnGamemodeToggleGroupSelectionChanged(ChangeEvent<ToggleButtonGroupState> evt)
        {
            var selections = evt.newValue.GetActiveOptions(_indexPool);
            if (selections.Length == 0) return;

            int selection = selections[0];
            Gamemode gamemode = _gamemodeToggleGroup[selection].name switch
            {
                _singleplayerButtonId => Gamemode.Singleplayer,
                _localMultiplayerButtonId => Gamemode.LocalMultiplayer,
                _onlineMultiplayerButtonId => Gamemode.OnlineMultiplayer,
                _ => throw new ArgumentOutOfRangeException()
            };

            GameSetupEvents.GamemodeSelected?.Invoke(gamemode);
        }

    }
}