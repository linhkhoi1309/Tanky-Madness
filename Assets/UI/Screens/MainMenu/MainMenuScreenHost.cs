using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreenHost : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    private MainMenuController controller;

    private void Awake()
    {
        controller = new MainMenuController(document.rootVisualElement);
    }
}
