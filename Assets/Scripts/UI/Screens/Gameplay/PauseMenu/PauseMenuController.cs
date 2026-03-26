using System;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Gameplay.PauseMenu
{
    public class PauseMenuController : MonoBehaviour
    {

        void OnEnable()
        {
            PauseMenuEvents.MainMenuButtonPressed += OnMainMenuButtonPressed;
        }

        void OnDestroy()
        {
            PauseMenuEvents.MainMenuButtonPressed -= OnMainMenuButtonPressed;
        }

        private void OnMainMenuButtonPressed()
        {
            SceneController.Instance.Load("MainMenuScene");
        }

    }
}