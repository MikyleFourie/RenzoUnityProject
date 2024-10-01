using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PhotoViewer : MonoBehaviour
{

    GameObject[] imageObjs; // Array of GameObjects to display images on
    Texture2D[] textures; // Array to store loaded textures
    List<string> validImages = new List<string>();

    //string[] files;
    //string pathPreFix;

    // Use this for initialization
    void Start()
    {
    }


    void Update()
    {

    }

    //public void LoadImagesAfterStart(string folderPath)
    //{
    //    imageObjs = GameObject.FindGameObjectsWithTag("Image");

    //    if (!Directory.Exists(folderPath))
    //    {
    //        Debug.LogError("Directory does not exist: " + folderPath);
    //        return;
    //    }

    //    // Get all .png and .jpg files from the directory
    //    string[] imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);



    //    // Filter by supported extensions
    //    foreach (string file in imageFiles)
    //    {
    //        if (file.EndsWith(".png") || file.EndsWith(".jpg"))
    //        {
    //            validImages.Add(file);
    //        }
    //    }

    //    textures = new Texture2D[validImages.Count];
    //    StartCoroutine(LoadImages(validImages.ToArray()));
    //}

    //private IEnumerator LoadImages(string[] filePaths)
    //{

    //    for (int i = 0; i < filePaths.Length && i < imageObjs.Length; i++)
    //    {
    //        string filePath = "file:///" + filePaths[i];  // File URI format
    //        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath))
    //        {
    //            yield return uwr.SendWebRequest();

    //            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
    //            {
    //                Debug.LogError("Error loading image: " + uwr.error);
    //            }
    //            else
    //            {
    //                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
    //                textures[i] = texture;

    //                // Apply the texture to the respective GameObject's material
    //                imageObjs[i].GetComponent<Renderer>().material.mainTexture = texture;

    //                // Set the name of the GameObject to the filename of the loaded image
    //                imageObjs[i].name = Path.GetFileName(filePaths[i]);  // Set the name to the filename (e.g., OriginsCentreEntrance.JPG)

    //                // Get the aspect ratio of the image
    //                float imageAspectRatio = (float)texture.width / texture.height;

    //                // Assume the plane's default size is 1x1 (square). Adjust the local scale to maintain the aspect ratio.
    //                Vector3 scale = imageObjs[i].transform.localScale;

    //                if (imageAspectRatio > 1f) // Landscape orientation
    //                {
    //                    scale.x = scale.y * imageAspectRatio;  // Adjust width based on aspect ratio
    //                }
    //                else // Portrait or square orientation
    //                {
    //                    scale.y = scale.x / imageAspectRatio;  // Adjust height based on aspect ratio
    //                }

    //                // Set the new scale to the image plane
    //                imageObjs[i].transform.localScale = scale;
    //            }
    //        }
    //    }
    //}

    public void LoadImagesAfterStart(string folderPath)
    {
        // Find all GameObjects with the "Image" tag
        imageObjs = GameObject.FindGameObjectsWithTag("Image");

        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("Directory does not exist: " + folderPath);
            return;
        }

        // Get all .png and .jpg files from the directory
        string[] imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

        // Filter by supported extensions
        foreach (string file in imageFiles)
        {
            if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".JPG"))
            {
                validImages.Add(file);
            }
        }

        textures = new Texture2D[validImages.Count];
        StartCoroutine(LoadImages(validImages.ToArray()));
    }

    private IEnumerator LoadImages(string[] filePaths)
    {
        // Get all Image GameObjects and their count
        imageObjs = GameObject.FindGameObjectsWithTag("Image");
        int totalImageObjects = imageObjs.Length;
        Debug.Log("imageObjs.Count total: " + totalImageObjects);
        // Ensure we only consider the valid image file count
        int maxImagesToLoad = Mathf.Min(filePaths.Length, totalImageObjects, 723); // Limit to 723 basically
        Debug.Log("maxImagestoLoad: " + maxImagesToLoad);


        // Shuffle the filePaths array for randomness
        List<string> shuffledPaths = new List<string>(filePaths);
        for (int i = 0; i < shuffledPaths.Count; i++)
        {
            string temp = shuffledPaths[i];
            int randomIndex = Random.Range(i, shuffledPaths.Count);
            shuffledPaths[i] = shuffledPaths[randomIndex];
            shuffledPaths[randomIndex] = temp;
        }

        Debug.Log("shuffledPaths.Count: " + shuffledPaths.Count);

        // Load the first batch of images into the first 723 image objects
        for (int i = 0; i < maxImagesToLoad; i++)
        {
            string filePath = "file:///" + shuffledPaths[i];  // File URI format
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error loading image: " + uwr.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                    textures[i] = texture;

                    // Apply the texture to the respective GameObject's material
                    imageObjs[i].GetComponent<Renderer>().material.mainTexture = texture;

                    // Set the name of the GameObject to the filename of the loaded image
                    imageObjs[i].name = Path.GetFileName(shuffledPaths[i]);

                    // Get the aspect ratio of the image
                    float imageAspectRatio = (float)texture.width / texture.height;

                    // Adjust the local scale to maintain the aspect ratio
                    Vector3 scale = imageObjs[i].transform.localScale;

                    if (imageAspectRatio > 1f) // Landscape orientation
                    {
                        scale.x = scale.y * imageAspectRatio;  // Adjust width based on aspect ratio
                    }
                    else // Portrait or square orientation
                    {
                        scale.y = scale.x / imageAspectRatio;  // Adjust height based on aspect ratio
                    }

                    // Set the new scale to the image plane
                    imageObjs[i].transform.localScale = scale;
                }
            }
        }

        Debug.Log("First Batch Complete");

        // Identify image objects that still do not have textures
        List<GameObject> untexturedImages = new List<GameObject>();

        for (int i = 0; i < totalImageObjects; i++)
        {
            if (imageObjs[i].GetComponent<Renderer>().material.mainTexture == null)
            {
                untexturedImages.Add(imageObjs[i]);
            }
        }
        Debug.Log("untexturedimages.Count: " + untexturedImages.Count);
        // Shuffle the paths again for randomness
        shuffledPaths.Clear();
        shuffledPaths = new List<string>(filePaths);
        for (int i = 0; i < shuffledPaths.Count; i++)
        {
            string temp = shuffledPaths[i];
            int randomIndex = Random.Range(i, shuffledPaths.Count);
            shuffledPaths[i] = shuffledPaths[randomIndex];
            shuffledPaths[randomIndex] = temp;
        }
        Debug.Log("shuffledPaths.Count: " + shuffledPaths.Count);

        // Randomly assign the same 723 low-res images to the untextured image objects
        for (int i = 0; i < Mathf.Min(723, untexturedImages.Count); i++)
        {
            string filePath = "file:///" + shuffledPaths[i];  // File URI format
            //Debug.Log("shuffledPaths at: " + i);
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error loading image: " + uwr.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                    //textures[maxImagesToLoad + i] = texture; // Store the duplicate textures in the textures array

                    // Apply the texture to the respective GameObject's material
                    untexturedImages[i].GetComponent<Renderer>().material.mainTexture = texture;

                    // Set the name of the GameObject to the filename of the loaded image
                    untexturedImages[i].name = Path.GetFileName(shuffledPaths[i]);

                    // Get the aspect ratio of the image
                    float imageAspectRatio = (float)texture.width / texture.height;

                    // Adjust the local scale to maintain the aspect ratio
                    Vector3 scale = untexturedImages[i].transform.localScale;

                    if (imageAspectRatio > 1f) // Landscape orientation
                    {
                        scale.x = scale.y * imageAspectRatio;  // Adjust width based on aspect ratio
                    }
                    else // Portrait or square orientation
                    {
                        scale.y = scale.x / imageAspectRatio;  // Adjust height based on aspect ratio
                    }

                    // Set the new scale to the image plane
                    untexturedImages[i].transform.localScale = scale;
                }
            }
        }

        Debug.Log("Second Batch Complete(?)");

    }



}
