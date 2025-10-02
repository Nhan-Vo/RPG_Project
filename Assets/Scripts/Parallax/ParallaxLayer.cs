using UnityEngine;

// This is a layer class that contains essential information for each parallax layer.
[System.Serializable]
public class ParallaxLayer 
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffSet = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
               imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
                imageHalfWidth = imageFullWidth / 2f;
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
        // could also be writen as: new Vector3(distanceToMove * parallaxMultiplier, 0f, 0f)
    }

    public void loopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffSet;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffSet;

        if (cameraLeftEdge > imageRightEdge)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if (cameraRightEdge < imageLeftEdge)
        {
            background.position -= Vector3.right * imageFullWidth;
        }
    }
}
