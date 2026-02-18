using System;
using UnityEngine.UIElements;

public class MainMenuController : BaseScreenController
{
    private Button _playButton;
    private Button _settingsButton;
    private Button _exitButton;
    private VisualElement _gameSettingsPanel;
    private Button _startButton;

    private Action _onExitRequested;

    public MainMenuController(VisualElement root, Action onExitRequested) : base(root)
    {
        _onExitRequested = onExitRequested;
    }

    protected override void InitializeUI()
    {
        _playButton = Root.Q<Button>("play-button");
        _settingsButton = Root.Q<Button>("settings-button");
        _exitButton = Root.Q<Button>("exit-button");

        _gameSettingsPanel = Root.Q<VisualElement>("game-settings-panel");
        _startButton = _gameSettingsPanel.Q<Button>("start-button");
    }

    protected override void RegisterEvents()
    {
        _playButton.clicked += OnPlayButtonClicked;
        _settingsButton.clicked += OnSettingsButtonClicked;
        _exitButton.clicked += OnExitButtonClicked;
        _startButton.clicked += OnStartButtonClicked;
    }

    protected override void SetInitialState()
    {
        _gameSettingsPanel.AddToClassList("hidden-right");
    }

    public override void Dispose()
    {
        _playButton.clicked -= OnPlayButtonClicked;
        _settingsButton.clicked -= OnSettingsButtonClicked;
        _exitButton.clicked -= OnExitButtonClicked;
        _startButton.clicked -= OnStartButtonClicked;
    }

    private void OnPlayButtonClicked() => ShowGameSettingsPanel();

    private void ShowGameSettingsPanel() => _gameSettingsPanel.RemoveFromClassList("hidden-right");

    private void OnExitButtonClicked() => _onExitRequested?.Invoke();

    private void OnSettingsButtonClicked() => throw new NotImplementedException();

    private void OnStartButtonClicked() => throw new NotImplementedException();
}