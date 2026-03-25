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
        HomeEvents.PlayButtonClicked += () => ShowView<GameSetupView>();
    }

}
