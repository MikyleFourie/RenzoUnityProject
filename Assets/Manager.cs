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

    public int repitions = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (repitions < 1) repitions = 1;

        CSVReader.ParseCSV(csvFilePath);
        ImagePlacer.GetNumberOfImageFilesInFolder(Path.Combine(Application.streamingAssetsPath, "Images", "ImagesLow"));
        for (int i = 0; i < repitions; i++)
        {
            ImagePlacer.PlaceImages();
        }
        PhotoViewer.LoadImagesAfterStart(Path.Combine(Application.streamingAssetsPath, "Images", "ImagesLow"), repitions);
    }

    // Updates the image description
    public void UpdateDescription(string imageName)
    {
        textMeshPro.text = CSVReader.GetDescription(imageName);
    }
}
