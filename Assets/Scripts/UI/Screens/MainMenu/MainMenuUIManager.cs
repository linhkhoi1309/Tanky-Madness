using UnityEngine;
using UnityEngine.UIElements;

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
