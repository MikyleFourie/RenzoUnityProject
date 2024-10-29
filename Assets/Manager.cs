using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;  // Reference to the Description TextMeshPro component
    public TextMeshProUGUI debugText;  // Reference to the TextMeshPro component
    public string csvFileName; // Name of the CSV file. It MUST be in Streaming Assets
    public ImagePlacer ImagePlacer;
    public PhotoViewer PhotoViewer;
    public CSVReader CSVReader;

    public int repetitions = 1;
    // Start is called before the first frame update
    void Start()
    {
        //UpdateDebug("Manager Start Ran");
        if (repetitions < 1) repetitions = 1;
        //UpdateDebug("Manager reset repitions to 1");

        //UpdateDebug("Manager gets CSV Reader tries to read CSV");
        CSVReader.ParseCSV(csvFileName);
        //UpdateDebug("Manager says CSV Reader Successfully Parsed");

        //UpdateDebug("Manager tries to get ImagePlacer to get Number of Images");
        ImagePlacer.GetNumberOfImageFilesInFolder(Path.Combine(Application.streamingAssetsPath, "Images", "ImagesLow"));
        //UpdateDebug("Manager says it got number of Images");

        //UpdateDebug("Manager got the image placer to place");
        for (int i = 0; i < repetitions; i++)
        {
            ImagePlacer.PlaceImages();
        }
        //UpdateDebug("Manager says all images placed");

        //UpdateDebug("Manager tries to populate images");
        PhotoViewer.LoadImagesAfterStart(Path.Combine(Application.streamingAssetsPath, "Images", "ImagesLow"), repetitions);
        //UpdateDebug("Manager successfully had all images placed");
    }


    // Updates the image description
    public void UpdateDescription(string imageName)
    {
        descriptionText.text = CSVReader.GetDescription(imageName);
    }

    public void UpdateDebug(string debug)
    {
        debugText.text += "\n" + debug;
    }
}
