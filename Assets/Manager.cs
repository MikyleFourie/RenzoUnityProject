using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // Reference to the TextMeshPro component
    public string csvFilePath; // Path to the CSV file
    public ImagePlacer ImagePlacer;
    public PhotoViewer PhotoViewer;
    public CSVReader CSVReader;
    // Start is called before the first frame update
    void Start()
    {
        //ImagePlacer = this.ImagePlacer;
        //PhotoViewer = this.PhotoViewer;
        //CSVReader = this.CSVReader;

        CSVReader.ParseCSV(csvFilePath);
        ImagePlacer.PlaceImages();
        PhotoViewer.LoadImagesAfterStart(Path.Combine(Application.streamingAssetsPath, "Images", "ImagesLow"));
    }

    // Updates the image description
    public void UpdateDescription(string imageName)
    {
        textMeshPro.text = CSVReader.GetDescription(imageName);
    }
}
