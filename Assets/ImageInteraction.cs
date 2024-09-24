using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageInteraction : MonoBehaviour
{
    public float interactionDistance = 40f; // Max distance for interaction
    public LayerMask imageLayer = default; // Layer to identify images
    public GameObject descriptionPanel; // Reference to the description UI panel
    public GameObject aimPanel;
    public FirstPersonController firstPersonController;
    //public GameObject refOrb;

    void Start()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionDistance, Color.red, 5f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
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


    }

    void ShowImageDescription(GameObject image)
    {
        firstPersonController.enabled = false;
        aimPanel.SetActive(false);
        descriptionPanel.SetActive(true);
        // Move the image close to the player and show the description
        // Assume image has a method or data to get its description
        // Display the description UI panel with the relevant text
    }
}

