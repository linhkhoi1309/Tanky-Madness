using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField] private ViewRegistry _viewRegistry;

    private UIDocument _uiDocument;
    protected UIDocument UiDocument => _uiDocument;

    private UIView _currentView;
    protected UIView CurrentView => _currentView;

    private readonly List<Action> _eventUnsubscriptions = new();

    private readonly Dictionary<Type, (UITransition In, UITransition Out)> _transitionRegistry = new();

    private readonly UITransition _defaultTransitionIn = UITransitions.SlideRelative(new Vector2(-1f, 0), new Vector2(0, 0), 600);
    //private readonly UITransition _defaultTransitionIn = UITransitions.Fade(0, 1, 600);
    private readonly UITransition _defaultTransitionOut = UITransitions.SlideRelative(new Vector2(0, 0), new Vector2(-1f, 0), 600);

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        RegisterTransitions();
        SetupViews();
        BindExternalEvents();
        BindInternalEvents();
    }

    private void RegisterTransitions() { }

    protected virtual void SetupViews() { }

    protected virtual void BindExternalEvents() { }

    protected virtual void BindInternalEvents() { }

    protected void BindEvent(Action eventHandler, Action callback)
    {
        eventHandler += callback;
        _eventUnsubscriptions.Add(() => eventHandler -= callback);
    }

    public async void ShowView<T>() where T : UIView
    {
        if (_currentView != null)
        {
            _transitionRegistry.TryGetValue(_currentView.GetType(), out var oldTrans);
            var transitionOut = oldTrans.Out ?? _defaultTransitionOut;

            await transitionOut(_currentView.Root);
            _currentView.Dispose();
        }

        VisualTreeAsset currentViewAsset = _viewRegistry.GetViewAsset<T>();
        if (currentViewAsset == null) return;

        _currentView = (T)Activator.CreateInstance(typeof(T), _uiDocument.rootVisualElement, currentViewAsset);

        _currentView.Initialize();

        while (float.IsNaN(_currentView.Root.layout.width) || _currentView.Root.layout.width <= 0)
        {
            await Task.Yield();
        }

        _transitionRegistry.TryGetValue(typeof(T), out var newTrans);
        var transitionIn = newTrans.In ?? _defaultTransitionIn;

        await transitionIn(_currentView.Root);
    }

    private void OnDestroy()
    {
        foreach (var unsubscribe in _eventUnsubscriptions)
        {
            unsubscribe?.Invoke();
        }
        _eventUnsubscriptions.Clear();
    }

}
