using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public DataSave dataSave;
    public bool loadSave;

    private string file = "mygFirstProjectSave.json";


    public GameObject continueButtonMainMenu;
    public TransitionPoint transitionPointContinue;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Load();

        if(dataSave.username == "")
        {
            dataSave = new DataSave();
            Save();
        }

        if(!dataSave.isSaved)
        {
            continueButtonMainMenu.SetActive(false);
        }
    }

    public static void ResetSave()
    {
        if(instance.dataSave.isSaved)
        {
            instance.ClearFile(instance.file);

            instance.dataSave = new DataSave();
            instance.Save();

            instance.Load();

            if (!instance.dataSave.isSaved && instance.continueButtonMainMenu != null)
            {
                instance.continueButtonMainMenu.SetActive(false);
            }
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(dataSave);
        WriteToFile(file, json);
    }

    public void Load()
    {
        string json = ReadFromFile(file);

        JsonUtility.FromJsonOverwrite(json, dataSave);
        loadSave = true;

        DefineContinueParameter();
    }

    private void ClearFile(string fileName)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write("");
        }
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Save();
            Debug.LogWarning("File Not Found");
            return "";
        }
    }

    private string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    private void DefineContinueParameter()
    {
        if(dataSave.isSaved)
        {
            transitionPointContinue.newSceneName = dataSave.enumScene.ToString();
            transitionPointContinue.transitionDestinationTag = (SceneTransitionDestination.DestinationTag)dataSave.indexCheckPoint;
        }
    }
}
