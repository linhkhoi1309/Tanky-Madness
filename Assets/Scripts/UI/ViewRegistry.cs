using Assets.Scripts.UI.Screens.MainMenu.GameSetup;
using Assets.Scripts.UI.Screens.MainMenu.Home;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "ViewRegistry", menuName = "UI/ViewRegistry")]
    public class ViewRegistry : ScriptableObject
    {

        public VisualTreeAsset homeViewAsset;
        public VisualTreeAsset gameSetupViewAsset;
        public VisualTreeAsset hudViewAsset;
        public VisualTreeAsset pauseViewAsset;

        public VisualTreeAsset GetViewAsset<T>() where T : UIView
        {
            switch (typeof(T).Name)
            {
                case nameof(HomeView):
                    return homeViewAsset;
                case nameof(GameSetupView):
                    return gameSetupViewAsset;
                //case nameof(HUDView):
                //    return hudViewAsset;
                //case nameof(PauseView):
                //    return pauseViewAsset;
                default:
                    Debug.LogError($"No view asset found for type {typeof(T).Name}");
                    return null;
            }
        }

    }
}