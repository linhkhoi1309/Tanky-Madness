using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : BaseScreenController
{
    Button _resumeButton;
    Button _settingsButton;
    Button _mainMenuButton;

    Action _onResumeRequested;
    Action _onSettingsRequested;
    Action _onMainMenuRequested;

    public PauseMenuController(VisualElement root, Action onResumeRequested, Action onSettingsRequested, Action onMainMenuRequested) : base(root)
    {
        _onResumeRequested = onResumeRequested;
        _onSettingsRequested = onSettingsRequested;
        _onMainMenuRequested = onMainMenuRequested;
    }

    protected override void InitializeUI()
    {
        _resumeButton = Root.Q<Button>("resume-button");
        _settingsButton = Root.Q<Button>("settings-button");
        _mainMenuButton = Root.Q<Button>("main-menu-button");
    }

    protected override void RegisterEvents()
    {
        _resumeButton.clicked += OnResumeButtonClicked;
        _settingsButton.clicked += OnSettingsButtonClicked;
        _mainMenuButton.clicked += OnMainMenuButtonClicked;
    }

    protected override void SetInitialState() {}

    public override void Dispose()
    {
        _resumeButton.clicked -= OnResumeButtonClicked;
        _settingsButton.clicked -= OnSettingsButtonClicked;
        _mainMenuButton.clicked -= OnMainMenuButtonClicked;
    }

    private void OnResumeButtonClicked() => _onResumeRequested?.Invoke();
    private void OnSettingsButtonClicked() => _onSettingsRequested?.Invoke();
    private void OnMainMenuButtonClicked() => _onMainMenuRequested?.Invoke();
}
