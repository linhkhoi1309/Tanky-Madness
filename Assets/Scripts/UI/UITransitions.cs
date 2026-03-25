using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public static class UITransitions
{

    public static UITransition Fade(float startOpacity, float endOpacity, int durationMs)
    {
        return async (element) =>
        {
            if (startOpacity >= 0 && startOpacity <= 1)
            {
                element.style.opacity = startOpacity;
            }

            element.experimental.animation.Start(new StyleValues()
            {
                opacity = endOpacity
            }, durationMs);

            await Task.Delay(durationMs);
        };
    }

    public static UITransition SlideRelative(Vector2 startScale, Vector2 endScale, int durationMs)
    {
        return async (element) =>
        {
            float width = element.resolvedStyle.width;
            float height = element.resolvedStyle.height;

            float startX = startScale.x * width;
            float startY = startScale.y * height;
            float endX = endScale.x * width;
            float endY = endScale.y * height;

            element.style.left = startX;
            element.style.top = startY;

            await Task.Yield();

            element.experimental.animation.Start(new StyleValues
            {
                left = endX,
                top = endY,
            }, durationMs);

            await Task.Delay(durationMs);
        };
    }

}
