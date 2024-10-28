using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public Manager Manager;
    //public string csvFilePath; // Path to the CSV file
    public List<ImageData> imageDataList = new List<ImageData>(); //List of each images data
    public List<string[]> parsedRows;
    //public CSVUpdaterTest csvUpdater;
    string[] csvLines;
    public List<string> cappaFileNames;
    public List<string> lascaFileNames;

    [System.Serializable]
    // A class to store data for each image entry. With each type as a string
    public class ImageData
    {
        public string itemId;
        public string fileType;
        public string fileSpec;
        public string titleEn;
        public string descriptionEn;
        public string creatorEn;
        public string locationCreated;
        public string dateCreated;
        public string timePeriod;
        public string type;
        public string medium;
        public string photographer;
        public string rights;
    }

    void Start()
    {

    }

    // Reading CSV manually. This is to deal with Multi-paragraph fields
    public List<string[]> ParseCSV(string fileName)
    {
        string csvFilePath = Path.Combine(Application.streamingAssetsPath, fileName);

        Manager.UpdateDebug("ParseCSV Start");
        List<string[]> parsedRows = new List<string[]>();
        bool insideQuotedField = false;
        StringBuilder fieldBuilder = new StringBuilder();
        List<string> currentRow = new List<string>();

        // Read CSV line by line
        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fields = line.Split(';');

                foreach (string field in fields)
                {
                    if (insideQuotedField)
                    {
                        // Continue building the current field until closing quote is found
                        fieldBuilder.Append("\n" + field);

                        // If this field ends with a quote, we're done
                        if (field.EndsWith("\""))
                        {
                            insideQuotedField = false;
                            currentRow.Add(fieldBuilder.ToString());
                            fieldBuilder.Clear();
                        }
                    }
                    else
                    {
                        // Normal case: field is either not quoted or quotes are balanced
                        if (field.StartsWith("\"") && !field.EndsWith("\""))
                        {
                            // Start of a multi-paragraph field
                            insideQuotedField = true;
                            fieldBuilder.Append(field);
                        }
                        else
                        {
                            // Standard field, just add it
                            currentRow.Add(field);
                        }
                    }
                }

                // Once the row is complete, add it to the list of rows
                if (!insideQuotedField)
                {
                    parsedRows.Add(currentRow.ToArray());
                    currentRow.Clear();
                }
            }
        }

        //Debug Code to show all the parsed Rows in console------------
        //Debug.Log("Showing Parsed Rows:");
        //string displayString = "";
        //foreach (string[] row in parsedRows)
        //{
        //    displayString = "";
        //    foreach (string item in row)
        //    {
        //        displayString += item + "#";
        //    }
        //    displayString += "\n";
        //    Debug.Log(displayString);
        //}
        //-------------------------------------------------------------

        Debug.Log("Number of images with information from CSV: " + parsedRows.Count);
        Manager.UpdateDebug("Number of images with information from CSV: " + parsedRows.Count);
        saveParsedRows(parsedRows);
        return parsedRows;
    }

    void saveParsedRows(List<string[]> parsedRows)
    {

        for (int i = 1; i < parsedRows.Count; i++)
        {
            string[] row = parsedRows[i];
            ImageData imageData = new ImageData
            {
                itemId = row[0],
                fileType = row[1],
                fileSpec = row[2],
                titleEn = row[3],
                descriptionEn = row[4],
                creatorEn = row[5],
                locationCreated = row[6],
                dateCreated = row[7],
                timePeriod = row[8],
                type = row[9],
                medium = row[10],
                photographer = row[11],
                rights = row[12]
            };

            // Add the parsed ImageData to the list
            imageDataList.Add(imageData);
        }

        string displayText = "";
        foreach (var image in imageDataList)
        {
            displayText += "itemId: " + image.itemId + "\n";
            displayText += "fileType: " + image.fileType + "\n";
            displayText += "fileSpec: " + image.fileSpec + "\n";
            displayText += "titleEn: " + image.titleEn + "\n";
            displayText += "descriptionEn: " + image.descriptionEn + "\n";
            displayText += "creatorEn: " + image.creatorEn + "\n";
            displayText += "location: " + image.locationCreated + "\n";
            displayText += "dateCreated: " + image.dateCreated + "\n";
            displayText += "timePeriod: " + image.timePeriod + "\n";
            displayText += "type: " + image.type + "\n";
            displayText += "medium: " + image.medium + "\n";
            displayText += "photographer: " + image.photographer + "\n";
            displayText += "rights: " + image.rights + "\n";
            displayText += "\n";
        }


        //csvUpdater.UpdateText(displayText);
        Debug.Log("Done Loading CSV");
        Manager.UpdateDebug("Done loading CSV");
    }

    // Searches through the List of Images for the matching one
    public string GetDescription(string imageName)
    {
        string displayText = "";
        bool imageFound = false;

        foreach (ImageData image in imageDataList)
        {
            if (imageName.Equals(image.fileSpec))
            {
                imageFound = true;

                // Helper function to replace empty values with "N/A"
                string CheckValue(string value) => string.IsNullOrEmpty(value) ? "N/A" : value;

                displayText += CheckValue(image.titleEn) + "\n\n";
                displayText += image.descriptionEn + "\n\n";
                displayText += "Created by: " + CheckValue(image.creatorEn) + "\n";
                displayText += "Location: " + CheckValue(image.locationCreated) + "\n";
                displayText += "Date Displayed: " + CheckValue(image.dateCreated) + "\n";
                displayText += "Time Period: " + CheckValue(image.timePeriod) + "\n";
                displayText += "Type: " + CheckValue(image.type) + "\n";
                displayText += "Medium: " + CheckValue(image.medium) + "\n";
                displayText += "Photographer: " + CheckValue(image.photographer) + "\n";
                displayText += "Rights: " + CheckValue(image.rights) + "\n";

                return displayText; // Exit after finding the first match
            }
        }

        foreach (string fileName in cappaFileNames)
        {
            if (imageName == fileName)
            {
                imageFound = true;

                displayText += "Cranio de Prozostrodon brasiliensis \n\n";
                displayText += "Foto de Leonardo Kerber, do Programa de Pós-Graduação em Biodiversidade Animal da UFSM \n\n";
                displayText += "Image provided by CAPPA - Centro de Apoio à Pesquisa Paleontológica da Quarta Colônia, Universidade Federal de Santa Maria";
            }
        }

        foreach (string fileName in lascaFileNames)
        {
            if (imageName == fileName)
            {
                imageFound = true;

                displayText += "Cerâmica Tupi-Guarani \n\n";
                displayText += "vasilha cerâmica com tratamento de superfície corrugado  \n\n";
                displayText += "Image provided by LASCA - Laboratório de Arqueologia, Sociedades e Culturas das Américas, Universidade Federal de Santa Maria\n";
                displayText += "Found and/or Studied in São João do Polêsine & Santa Maria ";
            }
        }

        // If no image was found, determine the default message
        if (!imageFound)
        {
            displayText = "Information about this Image is not available. \nThe Image was provided by the Origin Centre of the University of the Witwatersrand";
        }

        return displayText;
    }
}
