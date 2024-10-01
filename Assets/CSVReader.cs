using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public string fileName; // Name of the CSV file without extension
    public string[] headers;
    public List<Dictionary<string, string>> data;
    void Start()
    {
        // Load the CSV data as a TextAsset
        TextAsset csvData = Resources.Load<TextAsset>("GAC_CSV - Origins Centre Wits University"); // Specify your subfolder name if needed

        // Check if the CSV data was loaded successfully
        if (csvData != null)
        {
            data = ReadCSV(csvData.text);

            // Log headers for debugging
            string[] headers = csvData.text.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None)[0].Split(',');
            foreach (var header in headers)
            {
                Debug.Log("Header: " + header);
            }

            int count = 0;
            foreach (var entry in data)
            {
                Debug.Log("Item title: " + entry["titleEn"]); // This line may still throw an error
                count++;
            }
            Debug.Log("Count: " + count);
        }
        else
        {
            Debug.LogError("Failed to load CSV file. Please check the file name and path.");
        }
    }

    List<Dictionary<string, string>> ReadCSV(string csvText)
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        // Split the CSV text into lines
        string[] lines = csvText.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None);

        // Split the first line to get headers using semicolons
        headers = lines[0].Split(';');

        // Read each subsequent line
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(';');

            // Check if the number of values matches the number of headers
            if (values.Length != headers.Length)
            {
                Debug.LogWarning($"Line {i + 1} has an inconsistent number of columns. Skipping this line.");
                continue; // Skip lines with inconsistent columns
            }

            Dictionary<string, string> entry = new Dictionary<string, string>();

            // Populate the dictionary
            for (int j = 0; j < headers.Length; j++)
            {
                entry[headers[j]] = values[j];
            }

            data.Add(entry);
        }

        return data;
    }

}
