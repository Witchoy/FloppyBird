using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    private const float TargetAspect = 9f / 16f;

    private void Start()
    {
        var windowAspect = (float)Screen.width / Screen.height;
        var scaleWidth = windowAspect / TargetAspect;

        var cam = GetComponent<Camera>();
        var rect = cam.rect;

        if (scaleWidth < 1f)
        {
            rect.width = 1f;
            rect.height = scaleWidth;
            rect.x = 0f;
            rect.y = (1f - scaleWidth) / 2f;
        }
        else
        {
            var scaleHeight = 1f / scaleWidth;
            rect.width = scaleHeight;
            rect.height = 1f;
            rect.x = (1f - scaleHeight) / 2f;
            rect.y = 0f;
        }

        cam.rect = rect;
    }
}