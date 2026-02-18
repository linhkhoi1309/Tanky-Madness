using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private UIDocument _uiDocument;
    private BaseScreenController _currentController;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    public void ShowScreen<T>(VisualTreeAsset uxml, Func<VisualElement, T> factory)
        where T : BaseScreenController
    {
        _currentController.Dispose();
        _uiDocument.rootVisualElement.Clear();

        VisualElement tree = uxml.CloneTree();
        _uiDocument.rootVisualElement.Add(tree);

        _currentController = factory(tree);
    }
}
