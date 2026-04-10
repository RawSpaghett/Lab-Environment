using UnityEngine;
using System.IO; //for in and out
using System.Text; //for txt

/*
    Check for current text file
    Create new text file if one does not already exists
    Extract previous timer
    Start application timer using saved time as the start
    OnApplicationQuit save current time to text file
*/

public class Playtime : MonoBehaviour
{
    private string _dataPath;
    private string _textFile;
    private float totalPlaytime;
    private float playtimeBase;
    private float timer = 0f;

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Playtime_Data/"; //system finds a spot to store save data
        Debug.Log(_dataPath);

        if(!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }

        _textFile = _dataPath + "Playtime_SaveData"; //attatches file name to the data path
        Debug.Log(_textFile);

        if(File.Exists(_textFile))
        {
            Debug.Log($"{_textFile} already exists");
            if (float.TryParse(File.ReadAllText(_textFile), out playtimeBase))
            {
                totalPlaytime = playtimeBase;
            }
            return;
        }

        File.WriteAllText(_textFile,string.Empty, Encoding.UTF8); //if the file does not already exist
        Debug.Log($"New {_textFile} created");
    }   

    void Update()
    {
        totalPlaytime += Time.deltaTime;

        timer += Time.deltaTime;

        if(timer >= 1f)
        { 
            Debug.Log($"{totalPlaytime.ToString()}");
            timer = 0f;
        }
    }

    void OnApplicationQuit()
    {
        File.WriteAllText(_textFile,totalPlaytime.ToString(), Encoding.UTF8);
        Debug.Log("<color=green> Data saved! </color>");
    }
}
