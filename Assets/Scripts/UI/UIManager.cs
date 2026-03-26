using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private ViewRegistry _viewRegistry;
        [SerializeField] private TransitionSettings _defaultTransitionInSettings;
        [SerializeField] private TransitionSettings _defaultTransitionOutSettings;

        private UIDocument _uiDocument;
        protected UIDocument UiDocument => _uiDocument;

        private UIView _currentView;
        protected UIView CurrentView => _currentView;
        private bool _isTransitioning;

        private readonly List<Action> _eventUnsubscriptions = new();

        private readonly Dictionary<Type, (UITransition In, UITransition Out)> _transitionRegistry = new();

        public UITransition _defaultTransitionIn { get; private set; }
        public UITransition _defaultTransitionOut { get; private set; }

        //private readonly UITransition _defaultTransitionIn = UITransitions.SlideRelative(new Vector2(-1f, 0), new Vector2(0, 0), 600);
        //private readonly UITransition _defaultTransitionOut = UITransitions.SlideRelative(new Vector2(0, 0), new Vector2(-1f, 0), 600);

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();

            _defaultTransitionIn = _defaultTransitionInSettings.Create();
            _defaultTransitionOut = _defaultTransitionOutSettings.Create();

            RegisterTransitions();
            SetupViews();
            BindExternalEvents();
            BindInternalEvents();
        }

        private void RegisterTransitions() { }

        protected virtual void SetupViews() { }

        protected virtual void BindExternalEvents() { }

        protected virtual void BindInternalEvents() { }

        public async void ShowView<T>() where T : UIView
        {
            if (_isTransitioning) return;

            VisualTreeAsset nextViewAsset = _viewRegistry.GetViewAsset<T>();
            if (nextViewAsset == null)
            {
                Debug.LogError($"[UIManager] View Asset for {typeof(T).Name} is missing!");
                return;
            }

            _isTransitioning = true;

            try
            {
                if (_currentView != null)
                {
                    _transitionRegistry.TryGetValue(_currentView.GetType(), out var oldTrans);
                    var transitionOut = oldTrans.Out ?? _defaultTransitionOut;

                    await transitionOut(_currentView.Root);
                    _currentView.Dispose();
                    _currentView = null;
                }

                _currentView = (T)Activator.CreateInstance(typeof(T), _uiDocument.rootVisualElement, nextViewAsset);
                _currentView.Root.style.visibility = Visibility.Hidden;

                await _currentView.WaitForLayout();

                _transitionRegistry.TryGetValue(typeof(T), out var newTrans);
                var transitionIn = newTrans.In ?? _defaultTransitionIn;

                _currentView.Root.style.visibility = Visibility.Visible;
                await transitionIn(_currentView.Root);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                _isTransitioning = false;
            }
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
}