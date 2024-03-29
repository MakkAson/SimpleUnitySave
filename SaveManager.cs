using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script gives a way to save data into a JSON file for your Unity games. As example,
this script saves the player's position, however you can customize the variables and expand
to your hearts content*/
public class SaveManager : MonoBehaviour
{
    //Declare variables and references
    [SerializeField] private Transform playerTransform;
    Vector3 playerCoordinates;
    public SaveData saveData = new SaveData();

    //Updates the saved data, then calls the function to log into a json file
    //Call this function in game to save the data
    public void UpdateSaveData(){
        //Make sure to reference the variables defined in the saveData class when saving values
        saveData.playerCoordinates[0] = playerTransform.position.x;
        saveData.playerCoordinates[1] = playerTransform.position.y;
        saveData.playerCoordinates[2] = playerTransform.position.z;
        SaveIntoJson();
    }

    //Saves the data into a json file
    //Should not be called on its own, rather through the UpdateSaveData function
    private void SaveIntoJson(){

        string data = JsonUtility.ToJson(saveData);
        /*This is the path that the file is written to, recommended to leave as is but feel free
        to change if you know what you're doing*/
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveData.json", data);
    }

    //Loads the data from a json file
    //Call this function in game to load the data
    public void LoadFromJson(){
        string data = System.IO.File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        saveData = JsonUtility.FromJson<SaveData>(data);
        LoadSaveData();
    }

    //Loads the save data into the game
    //Call the LoadFromJson function to call this one, usually don't want to call this one on its own
    private void LoadSaveData()
    {
        playerCoordinates = new Vector3(saveData.playerCoordinates[0], saveData.playerCoordinates[1], saveData.playerCoordinates[2]);
        playerTransform.position = playerCoordinates;
    }
}

//Holds the save data
[System.Serializable]
public class SaveData
{
    //Put anything you would like to save into here. playerCoordinates is used as example
    public float[] playerCoordinates = new float[3];
}