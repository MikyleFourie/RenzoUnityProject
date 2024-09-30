using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePlacer : MonoBehaviour
{
    public GameObject imagePrefab;  // Prefab representing the image (or an empty GameObject with Image attached)
    public int imageCount = 7;   // Number of images to place (should be 2000)
    public Vector2 roomSize = new Vector2(30, 30);  // Room dimensions (Width x Length) (should be 100x100 for full room)
    public float minSpacing = 2.0f; // Minimum spacing between images
    public float minHeight = 1.0f;  // Minimum height off the floor
    public float maxHeight = 4.0f; // Maximum height the images can be placed at

    private List<Vector3> placedPositions = new List<Vector3>();
    public int numberOfPictures = 0;

    public PhotoViewer photoViewer;

    void Start()
    {
        PlaceImages();
        photoViewer.LoadImagesAfterStart("C:\\Users\\mikyl\\OneDrive\\Desktop\\Images");
    }

    void PlaceImages()
    {
        for (int i = 0; i < imageCount; i++)
        {
            Vector3 randomPosition;

            // Try to find a position that doesn't overlap with others
            int attempts = 0;
            do
            {
                float xPos = Random.Range(-roomSize.x / 2, roomSize.x / 2);
                float zPos = Random.Range(-roomSize.y / 2, roomSize.y / 2);
                float yPos = Random.Range(minHeight, maxHeight); // Random height between min and max

                randomPosition = new Vector3(xPos, yPos, zPos);

                attempts++;
                if (attempts > 50) break; // Prevent infinite loops in crowded scenarios
            }
            while (!IsPositionValid(randomPosition));

            // Generate a random Y rotation
            float randomYRotation = Random.Range(0f, 360f); // Random rotation between 0 and 360 degrees

            // Instantiate the image prefab at the chosen position
            GameObject imageInstance = Instantiate(imagePrefab, randomPosition, Quaternion.Euler(0, randomYRotation, 0));
            numberOfPictures++;
            placedPositions.Add(randomPosition);
        }
    }

    // Check if the new image is far enough from previously placed images
    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 placed in placedPositions)
        {
            if (Vector3.Distance(placed, position) < minSpacing)
                return false;
        }
        return true;
    }
}

