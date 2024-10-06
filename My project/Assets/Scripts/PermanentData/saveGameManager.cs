using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Add this to use Directory and File operations

public static class saveGameManager
{
    public static saveData currentSaveData = new saveData();

    public const string saveDirectory = "/SaveData/";
    public const string FileName = "SaveGame.sav";

    public static bool SaveGame()
    {
        var dir = Application.persistentDataPath + saveDirectory;

        if (!Directory.Exists(dir))  // Use 'Directory' (capital D)
            Directory.CreateDirectory(dir);  // Use 'Directory' (capital D)

        string json = JsonUtility.ToJson(currentSaveData, true);
        File.WriteAllText(dir + FileName, json);  // Use 'File' (capital F)
        GUIUtility.systemCopyBuffer = dir;

        return true;
    }

    public static void LoadGame(){
        string fullPath = Application.persistentDataPath + saveDirectory + FileName;
        saveData tempData = new saveData();

        if(File.Exists(fullPath)){
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<saveData>(json);
        }else{

            Debug.LogError("Non-existant File");
        }

        currentSaveData = tempData;
    }

    
}
