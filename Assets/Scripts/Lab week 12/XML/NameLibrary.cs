using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

// XmlSerializer needs this to be its own public class
[System.Serializable]
public class NameData
{
    public List<string> firstNames;
    public List<string> lastInitials;
}

/// <summary>
/// Pulls the first names and last initials from an XML file so that adding more names would be easy in the future.
/// </summary>
public class NameLibrary : MonoBehaviour
{
    private string _dataPath;
    private string _xmlFile;

    public string[] firstNames;
    public string[] lastInitials;

    private static readonly string[] defaultFirstNames =
    {
        "James", "Mary", "Robert", "Patricia", "John", "Jennifer",
        "Michael", "Linda", "David", "Elizabeth", "William", "Barbara",
        "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah",
        "Christopher", "Karen", "Charles", "Nancy", "Lisa", "Daniel",
        "Betty", "Matthew", "Margaret", "Anthony", "Sandra", "Kevin"
    };

    private static readonly string[] defaultLastInitials =
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
        "U", "V", "W", "X", "Y", "Z"
    };

    void Awake()
    {
        _dataPath = Application.dataPath + "/Data/";
        _xmlFile = _dataPath + "names.xml";

        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }

        if (File.Exists(_xmlFile))
        {
            LoadNames();
        }
        else
        {
            // first run, write the defaults so the file exists for next time
            firstNames = defaultFirstNames;
            lastInitials = defaultLastInitials;
            SaveNames();
        }
    }

    private void SaveNames()
    {
        NameData data = new NameData
        {
            firstNames = new List<string>(firstNames),
            lastInitials = new List<string>(lastInitials)
        };

        var xmlSerializer = new XmlSerializer(typeof(NameData));
        using (FileStream stream = File.Create(_xmlFile))
        {
            xmlSerializer.Serialize(stream, data);
        }

        Debug.Log("names.xml created at " + _xmlFile);
    }

    private void LoadNames()
    {
        var xmlSerializer = new XmlSerializer(typeof(NameData));
        using (FileStream stream = File.OpenRead(_xmlFile))
        {
            NameData data = (NameData)xmlSerializer.Deserialize(stream);
            firstNames = data.firstNames.ToArray();
            lastInitials = data.lastInitials.ToArray();
        }

        Debug.Log("loaded " + firstNames.Length + " names from names.xml");
    }
}