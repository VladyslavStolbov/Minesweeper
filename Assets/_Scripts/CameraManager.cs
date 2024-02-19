using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode] 
public class CameraManager : MonoBehaviour
{
    [SerializeField] private PixelPerfectCamera _pixelPerfectCamera;

    private void Start()
    {
        _pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        _pixelPerfectCamera.runInEditMode = true;
    }
}
