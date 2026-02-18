using System;
using UnityEngine.UIElements;

public abstract class BaseScreenController : IDisposable
{
    protected VisualElement Root;

    protected BaseScreenController(VisualElement root)
    {
        Root = root;
        InitializeUI();
        RegisterEvents();
        SetInitialState();
    }

    protected abstract void InitializeUI();
    protected abstract void RegisterEvents();
    protected abstract void SetInitialState();

    public abstract void Dispose(); 
}
