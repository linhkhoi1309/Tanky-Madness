using UnityEngine;
using UnityEngine.UIElements;

public class GameSetupView : UIView
{

    private const string _gamemodeToggleGroupId = "gamemode-toggle-group";
    private const string _startButtonId = "start-button";

    private ToggleButtonGroup _gamemodeToggleGroup;
    private Button _startButton;

    public GameSetupView(VisualElement parent, VisualTreeAsset asset) : base(parent, asset) { }

    protected override void SetVisualElements()
    {
        _gamemodeToggleGroup = Root.Q<ToggleButtonGroup>(_gamemodeToggleGroupId);
        _startButton = Root.Q<Button>(_startButtonId);
    }

    protected override void BindInternalEvents()
    {
        BindClick(_startButton, () => GameSetupEvents.StartButtonClicked?.Invoke());
    }

}
