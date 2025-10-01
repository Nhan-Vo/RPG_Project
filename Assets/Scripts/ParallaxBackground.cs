using UnityEngine;

// This is a controller class that manages multiple parallax layers.
public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastMainCameraPositionX;
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLength();
    }

    private void FixedUpdate()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;  
        float distanceToMove = currentCameraPositionX - lastMainCameraPositionX;
        lastMainCameraPositionX = currentCameraPositionX;

        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.loopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }
    private void CalculateImageLength()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}

