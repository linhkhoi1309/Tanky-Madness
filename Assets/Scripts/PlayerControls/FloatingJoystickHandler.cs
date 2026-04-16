using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

enum ScreenSide
{
    Left,
    Right
}

public class FloatingJoystickHandler : OnScreenControl
{

    [SerializeField]
    private FloatingJoystick floatingJoystick;

    [SerializeField]
    private ScreenSide joystickSide;

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string _controlPath;

    protected override string controlPathInternal
    {
        get => _controlPath;
        set => _controlPath = value;
    }

    private ETouch.Finger activeFinger;

    private void OnEnable()
    {
        ETouch.EnhancedTouchSupport.Enable();

        ETouch.Touch.onFingerDown += OnFingerDown;
        ETouch.Touch.onFingerMove += OnFingerMove;
        ETouch.Touch.onFingerUp += OnFingerUp;
    }

    private void OnFingerDown(Finger finger)
    {
        Vector2 fingerPosition = finger.screenPosition;
        ScreenSide fingerSide = fingerPosition.x <= Screen.width * 0.5f ? ScreenSide.Left : ScreenSide.Right;
        if (activeFinger != null || fingerSide != joystickSide) return;

        activeFinger = finger;
        floatingJoystick.gameObject.SetActive(true);
        floatingJoystick.RectTransform.anchoredPosition = ClampStartPosition(fingerPosition);
    }

    private Vector2 ClampStartPosition(Vector2 position)
    {
        float clampedX = Mathf.Clamp(position.x, floatingJoystick.Radius, Screen.width - floatingJoystick.Radius);
        float clampedY = Mathf.Clamp(position.y, floatingJoystick.Radius, Screen.height - floatingJoystick.Radius);
        return new Vector2(clampedX, clampedY);
    }

    private void OnFingerMove(Finger finger)
    {
        if (finger != activeFinger) return;

        Vector2 knobPosition;
        float joystickRadius = floatingJoystick.Radius;
        ETouch.Touch touch = finger.currentTouch;

        if (Vector2.Distance(touch.screenPosition, floatingJoystick.RectTransform.anchoredPosition) > joystickRadius)
        {
            Vector2 direction = (touch.screenPosition - floatingJoystick.RectTransform.anchoredPosition).normalized;
            knobPosition = direction * joystickRadius;
        }
        else
        {
            knobPosition = touch.screenPosition - floatingJoystick.RectTransform.anchoredPosition;
        }

        floatingJoystick.Knob.anchoredPosition = knobPosition;
        Vector2 inputVector = knobPosition / joystickRadius;
        SendValueToControl(inputVector);
    }

    private void OnFingerUp(Finger finger)
    {
        if (finger != activeFinger) return;

        activeFinger = null;
        floatingJoystick.Knob.anchoredPosition = Vector2.zero;
        floatingJoystick.gameObject.SetActive(false);

        SendValueToControl(Vector2.zero);
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnFingerDown;
        ETouch.Touch.onFingerMove -= OnFingerMove;
        ETouch.Touch.onFingerUp -= OnFingerUp;
        ETouch.EnhancedTouchSupport.Disable();
    }
}
