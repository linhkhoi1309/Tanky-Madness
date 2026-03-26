using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Screens.Gameplay.HUD
{
    public class HUDView : UIView
    {

        private const string _pauseButtonId = "pause-button";

        private Button _pauseButton;

        public HUDView(VisualElement parent, VisualTreeAsset asset) : base(parent, asset) { }

        protected override void SetVisualElements()
        {
            _pauseButton = Root.Q<Button>(_pauseButtonId);
        }

        protected override void BindInternalEvents()
        {
            BindClick(_pauseButton, () => HUDEvents.PauseButtonPressed?.Invoke());
        }

    }
}