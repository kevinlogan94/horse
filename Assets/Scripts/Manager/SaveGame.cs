using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveGame
{
    private static readonly string Path = Application.persistentDataPath + "/StableDashGame.data";
    
    public static void Save()
    {
        var formatter = new BinaryFormatter();
        // var path = Application.persistentDataPath + "/StableDashGame.data";
        var stream = new FileStream(Path, FileMode.Create);
        
        //store all the saved data we need here.
        var savedData = new SavedData();
        
        Debug.Log("Saving game at: " + Path);
        //store all the saved data on a new file on the path above.
        formatter.Serialize(stream, savedData);
        stream.Close();
    }

    public static void Load()
    {

        // SavedData.RefreshData();
        // return;
        
        if (File.Exists(Path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(Path, FileMode.Open);

            var savedData = formatter.Deserialize(stream) as SavedData;
            
            //take our load data and load it into the managers across the app that need this data.
            savedData?.DistributeLoadData();
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found at:" + Path);
        }
    }
}