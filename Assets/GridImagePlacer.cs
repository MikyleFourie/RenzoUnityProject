using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridImagePlacer : MonoBehaviour
{
    public GameObject imagePrefab;   // The prefab of the image object
    public Vector3 roomDimensions;   // X, Y, Z dimensions of the room
    public int numImages = 2000;     // Number of images to spawn
    public float minDistanceBetweenImages = 2f;  // Minimum distance between images
    public float ceilingHeight = 15f;  // Height to simulate "hanging from ceiling"

    private List<Vector3> placedPositions = new List<Vector3>();

    void Start()
    {
        PlaceImages();
    }

    void PlaceImages()
    {
        for (int i = 0; i < numImages; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (!IsTooCloseToOthers(randomPosition))
            {
                GameObject image = Instantiate(imagePrefab, randomPosition, Quaternion.identity);
                //AddRaycastForHanging(image);  // Simulate hanging the image
                placedPositions.Add(randomPosition);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // Random position within room dimensions, accounting for ceiling height
        float randomX = Random.Range(0, roomDimensions.x);
        float randomY = Random.Range(ceilingHeight * 0.5f, ceilingHeight);  // Place images in midair
        float randomZ = Random.Range(0, roomDimensions.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    bool IsTooCloseToOthers(Vector3 newPos)
    {
        foreach (Vector3 placedPos in placedPositions)
        {
            if (Vector3.Distance(newPos, placedPos) < minDistanceBetweenImages)
            {
                return true;
            }
        }
        return false;
    }

    //void AddRaycastForHanging(GameObject image)
    //{
    //    // Simulate hanging with a visible raycast or line renderer
    //    LineRenderer line = image.AddComponent<LineRenderer>();
    //    line.positionCount = 2;
    //    line.SetPosition(0, image.transform.position);
    //    line.SetPosition(1, new Vector3(image.transform.position.x, ceilingHeight, image.transform.position.z));
    //    line.startWidth = 0.02f;
    //    line.endWidth = 0.02f;
    //}
}

