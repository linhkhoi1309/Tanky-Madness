using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D crosshairTexture;

    void Start()
    {
        ChangeCursor(crosshairTexture);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotSpot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
    }
}
