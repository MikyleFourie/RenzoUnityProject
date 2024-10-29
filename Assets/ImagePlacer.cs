using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ImagePlacer : MonoBehaviour
{
    public Manager Manager;
    public GameObject imagePrefab;  // Prefab representing the image (or an empty GameObject with Image attached)
    int imageCount = 723;   // Number of images to place (should be 2000)
    public Vector2 roomSize = new Vector2(30, 30);  // Room dimensions (Width x Length) (should be 100x100 for full room)
    public float minSpacing = 2.0f; // Minimum spacing between images
    public float minHeight = 1.0f;  // Minimum height off the floor
    public float maxHeight = 4.0f; // Maximum height the images can be placed at

    private List<Vector3> placedPositions = new List<Vector3>();
    public int numberOfPictures = 0;

    public PhotoViewer photoViewer;

    // Function that returns the number of image files in a folder (e.g., jpg, png)
    public void GetNumberOfImageFilesInFolder(string folderPath)
    {
        // Define allowed image file extensions
        string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

        // Check if the directory exists
        if (Directory.Exists(folderPath))
        {
            // Get all the files in the directory
            string[] allFiles = Directory.GetFiles(folderPath);

            // Filter the files based on their extensions
            int imageFileCount = 0;
            foreach (string file in allFiles)
            {
                if (validExtensions.Contains(Path.GetExtension(file).ToLower()))
                {
                    imageFileCount++;
                }
            }

            imageCount = imageFileCount; // Return the number of valid image files found
        }
        else
        {
            Debug.LogError("Directory does not exist: " + folderPath);
            Manager.UpdateDebug("Directory does not exist: " + folderPath);
        }

        Debug.Log("Number of Images found in the ImagesLow folder: " + imageCount);
        Manager.UpdateDebug("Number of Images found in the ImagesLow folder: " + imageCount);
    }

    public void PlaceImages()
    {
        Debug.Log("Placing " + imageCount + " frames");
        Manager.UpdateDebug("Placing " + imageCount + " frames");
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

