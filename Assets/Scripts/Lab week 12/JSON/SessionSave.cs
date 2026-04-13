using UnityEngine;
using System.IO;

// JsonUtility doesn't serialize arrays or lists on their own, needs a wrapper class
[System.Serializable]
public class LoginSessionData
{
    public int totalPlayersLoggedIn;
    public int sessionCount;
}

/// <summary>
/// Saves and loads a running count of players logged in and how many sessions have been played.
/// </summary>
public class SessionSave : MonoBehaviour
{
    private string _dataPath;
    private string _jsonFile;

    public int totalPlayersLoggedIn;
    public int sessionCount;

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/LoginServer_Data/";

        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }

        _jsonFile = _dataPath + "LoginSessionData.json";

        if (File.Exists(_jsonFile))
        {
            string json = File.ReadAllText(_jsonFile);
            LoginSessionData state = JsonUtility.FromJson<LoginSessionData>(json);
            totalPlayersLoggedIn = state.totalPlayersLoggedIn;
            sessionCount = state.sessionCount;
            Debug.Log($"save loaded. sessions: {sessionCount}, total players: {totalPlayersLoggedIn}");
        }
        else
        {
            Debug.Log("no save found, starting fresh");
        }

        sessionCount++; // increment after loading so the loaded value isn't overwritten
        Debug.Log($"session count incremented, current session: {sessionCount}");
    }

    // call this when a player dequeues in Task1
    public void LogPlayerIn()
    {
        totalPlayersLoggedIn++;
    }

    void OnApplicationQuit()
    {
        LoginSessionData state = new LoginSessionData
        {
            totalPlayersLoggedIn = totalPlayersLoggedIn,
            sessionCount = sessionCount
        };

        string json = JsonUtility.ToJson(state, true);
        using (StreamWriter stream = File.CreateText(_jsonFile))
        {
            stream.WriteLine(json);
        }

        Debug.Log("<color=green>session data saved</color>");
    }
}