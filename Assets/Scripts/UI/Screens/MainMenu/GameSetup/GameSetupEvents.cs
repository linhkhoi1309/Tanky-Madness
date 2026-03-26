using System;

namespace Assets.Scripts.UI.Screens.MainMenu.GameSetup
{
    public static class GameSetupEvents
    {

        public static Action<Gamemode> GamemodeSelected;
        public static Action BackButtonPressed;
        public static Action StartButtonPressed;

    }
}