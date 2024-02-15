using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode] 
public class CameraManager : MonoBehaviour
{
    private PixelPerfectCamera _pixelPerfectCamera;

    private void Start()
    {
        _pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        _pixelPerfectCamera.runInEditMode = true;
    }
}
