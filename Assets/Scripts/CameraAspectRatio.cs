using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    private const float TargetAspect = 9f / 16f;

    private Camera _cam;
    private int _lastWidth;
    private int _lastHeight;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Screen.width == _lastWidth && Screen.height == _lastHeight) return;

        _lastWidth = Screen.width;
        _lastHeight = Screen.height;
        ApplyAspect();
    }

    private void ApplyAspect()
    {
        var windowAspect = (float)Screen.width / Screen.height;
        var scaleWidth = windowAspect / TargetAspect;
        var rect = _cam.rect;

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

        _cam.rect = rect;
    }
}