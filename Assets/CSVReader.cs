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

            // Debugging the first 50 rows
            int count = 0;
            for (int i = 0; i < Mathf.Min(data.Count, 50); i++) // Process only the first 50 rows
            {
                Debug.Log($"Item {i + 1}:");
                foreach (var header in data[i])
                {
                    Debug.Log($"{header.Key}: {header.Value}");
                }
                count++;
            }
            Debug.Log("Total Count: " + count);
        }
        else
        {
            Debug.LogError("Failed to load CSV file. Please check the file name and path.");
        }
    }

    List<Dictionary<string, string>> ReadCSV(string csvText)
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        // Use a StringReader to process the CSV line by line
        using (StringReader reader = new StringReader(csvText))
        {
            string line;
            // Read the header line
            string headerLine = reader.ReadLine();
            if (headerLine != null)
            {
                headers = headerLine.Split(';'); // Use semicolon for values
            }

            // Read each subsequent line
            int lineNumber = 1;
            while ((line = reader.ReadLine()) != null && lineNumber <= 50) // Limit to 50 rows
            {
                // Debug the line being processed
                Debug.Log($"Processing row {lineNumber}: {line}");

                string[] values = ParseCSVLine(line);

                // Check if the values length matches headers to avoid out-of-bounds errors
                if (values.Length == headers.Length)
                {
                    Dictionary<string, string> entry = new Dictionary<string, string>();

                    for (int j = 0; j < headers.Length; j++)
                    {
                        entry[headers[j].Trim()] = values[j].Trim().Trim('"'); // Trim quotes and spaces
                    }

                    data.Add(entry);
                }
                else
                {
                    Debug.LogWarning($"Skipping line {lineNumber} due to inconsistent number of columns. {lineNumber} has {values.Length} columns, where there should be {headers.Length} columns");
                }
                lineNumber++;
            }
        }

        return data;
    }

    string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string currentValue = "";

        foreach (char c in line)
        {
            if (c == '"') // Handle quotes
            {
                inQuotes = !inQuotes; // Toggle inQuotes state
                                      // Only add quote to the value if it's the second quote (indicating it's part of the value)
                if (currentValue.Length > 0 && currentValue[currentValue.Length - 1] == '"')
                {
                    currentValue = currentValue.Remove(currentValue.Length - 1); // Remove the previous quote
                }
            }
            else if (c == ';' && !inQuotes) // Handle semicolon as separator if not within quotes
            {
                result.Add(currentValue.Trim());
                currentValue = "";
            }
            else
            {
                currentValue += c; // Add characters to current value
            }
        }

        result.Add(currentValue.Trim()); // Add the last value
        return result.ToArray();
    }
}
