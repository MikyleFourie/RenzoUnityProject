using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageInteraction : MonoBehaviour
{
    public float interactionDistance = 40f; // Max distance for interaction
    public LayerMask imageLayer = default; // Layer to identify images
    public GameObject descriptionPanel; // Reference to the description UI panel
    public GameObject aimPanel;
    public FirstPersonController firstPersonController;
    public Manager manager;
    //public GameObject refOrb;

    // Reference to the high-res image display in the description panel
    public Image highResImageDisplay; // UI Image component for displaying high-res images
    public TMPro.TMP_Text descriptionText; // Text component for displaying description
    [Header("CrossHair")]
    public GameObject HighlightCrosshair;
    void Start()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.red, 5f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            descriptionPanel.SetActive(false);
            aimPanel.SetActive(true);
            firstPersonController.enabled = true;
        }


        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            RaycastHit hit;

            // Visualize the raycast in the Scene view
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.red, 5f);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit /*, interactionDistance, imageLayer */))
            {
                //Debug.Log("Ray hit point: " + hit.point);
                //refOrb.transform.position = hit.point;


                // Check if the clicked object is an image
                if (hit.transform.CompareTag("Image"))
                {
                    //Debug line
                    //Debug.Log("Hit this object: " + hit.transform.parent.name);

                    // Call a method to handle the interaction
                    ShowImageDescription(hit.transform.gameObject);
                }
            }
        }

        RaycastHit hit2;

        // Visualize the raycast in the Scene view
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.red, 5f);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit2 /*, interactionDistance, imageLayer */))
        {
            //Debug.Log("Ray hit point: " + hit.point);
            //refOrb.transform.position = hit.point;


            // Check if the clicked object is an image
            if (hit2.transform.CompareTag("Image"))
            {
                //Debug line
                //Debug.Log("Hit this object: " + hit.transform.parent.name);

                // Call a method to handle the interaction
                HighlightCrosshair.SetActive(true);
            }

        }
        else
        {
            HighlightCrosshair.SetActive(false);
        }
    }





    void ShowImageDescription(GameObject image)
    {
        firstPersonController.enabled = false;
        aimPanel.SetActive(false);
        descriptionPanel.SetActive(true);

        // Get the name of the low-res image from the clicked GameObject
        string lowResImageName = image.name; // Make sure your GameObject's name is the same as the image file name

        manager.UpdateDescription(lowResImageName);


        // Construct the path to the corresponding high-res image
        string highResImagePath = Path.Combine(
            Application.streamingAssetsPath, // Use the StreamingAssets folder
            "Images",
            "ImagesHigh",                   // The subfolder for high-resolution images
            lowResImageName                 // The image name (e.g., "GC9A5979.jpg")
        );

        // Load the high-res image
        LoadHighResImage(highResImagePath);
    }


    void LoadHighResImage(string path)
    {
        // Assuming you have a way to display the image in the UI
        Texture2D highResTexture = new Texture2D(2, 2); // Create a temporary texture
        byte[] imageData = File.ReadAllBytes(path); // Read the high-res image file
        highResTexture.LoadImage(imageData); // Load the image data into the texture

        // Assume you have a reference to an Image component in your description panel
        //Image imageComponent = descriptionPanel.GetComponentInChildren<Image>();
        //imageComponent.sprite = Sprite.Create(highResTexture, new Rect(0, 0, highResTexture.width, highResTexture.height), new Vector2(0.5f, 0.5f));
        highResImageDisplay.sprite = Sprite.Create(highResTexture, new Rect(0, 0, highResTexture.width, highResTexture.height), new Vector2(0.5f, 0.5f));

        // Optionally, display the description text as well
        // Assume you have a way to set the description text
        // descriptionText.text = GetImageDescription(lowResImageName); // Add a method to fetch description
    }

}

