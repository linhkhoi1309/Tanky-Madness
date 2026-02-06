using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private VisualElement root;

    private Button playButton;
    private Button settingsButton;
    private Button exitButton;
    
    private VisualElement gameSettingsPanel;
    private Button startButton;

    public MainMenuController(VisualElement root)
    {
        this.root = root;
        InitializeUIElements();
        RegisterEventHandlers();
        SetInitialState();
    }

    private void InitializeUIElements()
    {
        playButton = root.Q<Button>("play-button");
        settingsButton = root.Q<Button>("settings-button");
        exitButton = root.Q<Button>("exit-button");

        gameSettingsPanel = root.Q<VisualElement>("game-settings-panel");
        startButton = gameSettingsPanel.Q<Button>("start-button");
    }
    
    private void RegisterEventHandlers()
    {
        playButton.clicked += PlayButton_clicked;
        settingsButton.clicked += SettingsButton_clicked;
        exitButton.clicked += ExitButton_clicked;

        startButton.clicked += StartButton_clicked;
    }

    private void SetInitialState()
    {
        gameSettingsPanel.AddToClassList("hidden-right");
    }

    private void PlayButton_clicked()
    {
        ShowGameSettingsPanel();
    }

    private void ShowGameSettingsPanel()
    {
        gameSettingsPanel.RemoveFromClassList("hidden-right");
    }

    private void SettingsButton_clicked()
    {
        throw new System.NotImplementedException();
    }

    private void ExitButton_clicked()
    {
        throw new System.NotImplementedException();
    }

    private void StartButton_clicked()
    {
        throw new System.NotImplementedException();
    }
}
