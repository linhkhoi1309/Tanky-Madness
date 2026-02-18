using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HUDController : BaseScreenController
{
    IReadOnlyList<int> _players;

    public HUDController(VisualElement root, IReadOnlyList<int> players) : base(root)
    {
        _players = players;
    }

    protected override void InitializeUI()
    {

    }

    protected override void RegisterEvents()
    {

    }

    protected override void SetInitialState()
    {

    }

    public override void Dispose()
    {

    }
}
