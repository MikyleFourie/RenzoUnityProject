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

    public void LoadImagesAfterStart(string folderPath)
    {
        //---------------OLD CODE
        //Change this to change pictures folder
        //string path = @"C:\Users\jeanf\Downloads\Images\";

        //pathPreFix = @"file://";

        //files = System.IO.Directory.GetFiles(path, "*.png");

        imageObjs = GameObject.FindGameObjectsWithTag("Image");

        //StartCoroutine(LoadImages());
        //-------------------------


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
            if (file.EndsWith(".png") || file.EndsWith(".jpg"))
            {
                validImages.Add(file);
            }
        }

        textures = new Texture2D[validImages.Count];
        StartCoroutine(LoadImages(validImages.ToArray()));
    }

    private IEnumerator LoadImages(string[] filePaths)
    {
        //-------------------OLD CODE
        //load all images in default folder as textures and apply dynamically to plane game objects.
        //6 pictures per page
        //textures = new Texture2D[files.Length];

        //int dummy = 0;
        //foreach (string tstring in files)
        //{

        //    string pathTemp = pathPreFix + tstring;
        //    WWW www = new WWW(pathTemp);
        //    yield return www;
        //    Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
        //    www.LoadImageIntoTexture(texTmp);

        //    textures[dummy] = texTmp;


        //    imageObjs[dummy].GetComponent<Renderer>().material.SetTexture("_MainTex", texTmp);
        //    dummy++;
        //}
        //---------------------------

        for (int i = 0; i < filePaths.Length && i < imageObjs.Length; i++)
        {
            string filePath = "file:///" + filePaths[i];  // File URI format
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

                    // Get the aspect ratio of the image
                    float imageAspectRatio = (float)texture.width / texture.height;

                    // Assume the plane's default size is 1x1 (square). Adjust the local scale to maintain the aspect ratio.
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
    }
}
