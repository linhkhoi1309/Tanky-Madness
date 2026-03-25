using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIView : IDisposable
{

    private readonly VisualElement _root;
    public VisualElement Root => _root;

    private readonly VisualTreeAsset _asset;

    private readonly List<Action> _unsubscriptionActions = new();

    public UIView(VisualElement parent, VisualTreeAsset asset)
    {
        _root = new()
        {
            name = GetType().Name
        };

        _asset = asset;

        parent.Add(_root);

        Initialize();
    }

    public virtual void Initialize()
    {
        if (_asset != null) _asset.CloneTree(_root);

        ApplyLayout();
        SetVisualElements();
        BindExternalEvents();
        BindInternalEvents();
    }

    protected virtual void ApplyLayout()
    {
        _root.style.position = Position.Absolute;
        _root.style.width = Length.Percent(100);
        _root.style.height = Length.Percent(100);
    }

    protected virtual void SetVisualElements() { }

    protected virtual void BindExternalEvents() { }

    protected virtual void BindInternalEvents() { }

    protected void BindClick(Button button, Action callback)
    {
        if (button == null) return;

        button.clicked += callback;

        _unsubscriptionActions.Add(() => button.clicked -= callback);
    }

    protected void BindChange<T>(INotifyValueChanged<T> field, EventCallback<ChangeEvent<T>> callback)
    {
        if (field == null) return;
        field.RegisterValueChangedCallback(callback);
        _unsubscriptionActions.Add(() => field.UnregisterValueChangedCallback(callback));
    }

    public void Dispose()
    {
        foreach (var unsubscribe in _unsubscriptionActions)
        {
            unsubscribe?.Invoke();
        }
        _unsubscriptionActions.Clear();

        _root?.RemoveFromHierarchy();

        OnDispose();
    }

    protected virtual void OnDispose() { }

}
