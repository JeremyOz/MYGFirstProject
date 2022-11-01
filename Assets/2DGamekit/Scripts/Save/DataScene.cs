using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DataScene : MonoBehaviour
{
    public static DataScene instance;

    public DataSceneToSave dataSceneToSave;

    public EnumSceneName enumSceneName;

    public List<Checkpoint> listCheckpoint = new List<Checkpoint>();

    public List<Damageable> listObjectNeedId = new List<Damageable>();
    public List<DataInteractableObjects> listDataInteractableObjects = new List<DataInteractableObjects>();

    public List<int> listIndexDeadEnemy = new List<int>();
    public List<int> listIndexDestroyDestructable = new List<int>();
    public List<DataInteractableSave> listDataInteractableSaves = new List<DataInteractableSave>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        if(enumSceneName != EnumSceneName.Undefined)
        {
            DefineCheckpointScene();
            DefineIdObjectInScene();
            DefineIdInteractableObjectInScene();

            Damageable.nextIndexDamageable = 0;
            DataInteractableObjects.nextIndexInteractable = 0;

            RestoreSave();

            ClearEnemyInList();
            ClearDestructibleInList();

            ApplyEllenDataSaveOnStartGame();
        }
    }

    private void Start()
    {
        if(enumSceneName != EnumSceneName.Undefined)
            SaveManager.instance.dataSave.enumScene = enumSceneName;
    }

    public void ApplyEllenDataSaveOnStartGame()
    {
        Damageable damageableEllen = GameObject.Find("Ellen").GetComponent<Damageable>();
        damageableEllen.ForceSetHealth(SaveManager.instance.dataSave.Life);


        PlayerCharacter playerCharacterEllen = GameObject.Find("Ellen").GetComponent<PlayerCharacter>();
        playerCharacterEllen.UpdatePlayerCharacterStatistiqueOnEvolve(SaveManager.instance.dataSave.IndexEvolveEllen);


        PlayerInput playerInputEllen = GameObject.Find("Ellen").GetComponent<PlayerInput>();

        if (SaveManager.instance.dataSave.MeleeAttackIsActive)
        {
            playerInputEllen.EnableMeleeAttacking();
        }
        
        if (SaveManager.instance.dataSave.RangedAttackIsActive)
        {
            playerInputEllen.EnableRangedAttacking();
        }
    }

    public void DefineCheckpointScene()
    {
        int index = 0;
        foreach (Checkpoint checkpoint in listCheckpoint)
        {
            checkpoint.indexSceneCheckPoint = index;

            SceneTransitionDestination sceneTransitionDestination = checkpoint.gameObject.GetComponentInChildren<SceneTransitionDestination>();
            sceneTransitionDestination.destinationTag = (SceneTransitionDestination.DestinationTag)checkpoint.indexSceneCheckPoint;
            sceneTransitionDestination.transitioningGameObject = GameObject.Find("Ellen");

            index++;
        }
    }

    public void DefineIdObjectInScene()
    {
        foreach (Damageable damageable in listObjectNeedId)
        {
            damageable.SetIndex();
        }
    }

    public void DefineIdInteractableObjectInScene()
    {
        foreach (DataInteractableObjects dataInteractableObjects in listDataInteractableObjects)
        {
            dataInteractableObjects.SetIndex();
        }
    }


    public void UpdateDataSceneSave()
    {
        dataSceneToSave.enumSceneName = enumSceneName;

        dataSceneToSave.listEnemyIsDead = listIndexDeadEnemy;
        dataSceneToSave.listDestructibleIsDestroy = listIndexDestroyDestructable;
        dataSceneToSave.listDataInteractableSave = listDataInteractableSaves;

        SaveManager.instance.dataSave.UpdateDataSave(dataSceneToSave);
    }

    public void RestoreSave()
    {
        DataSceneToSave dataSceneToSave = SaveManager.instance.dataSave.GetDataSave(enumSceneName);
        listIndexDeadEnemy = dataSceneToSave.listEnemyIsDead;
        listIndexDestroyDestructable = dataSceneToSave.listDestructibleIsDestroy;
        listDataInteractableSaves = dataSceneToSave.listDataInteractableSave;
    }

    public void AddEnemyIsDead(int index)
    {
        if(!listIndexDeadEnemy.Contains(index))
            listIndexDeadEnemy.Add(index);
    }

    public void AddDestructibleIsDestroy(int index)
    {
        if (!listIndexDestroyDestructable.Contains(index))
            listIndexDestroyDestructable.Add(index);
    }
    

    public void ClearEnemyInList()
    {
        foreach (int index in listIndexDeadEnemy)
        {
            foreach (Damageable damageable in listObjectNeedId)
            {
                if (damageable.indexDamageable == index)
                    damageable.gameObject.SetActive(false);
            }
        }
    }

    public void ClearDestructibleInList()
    {
        foreach (int index in listIndexDestroyDestructable)
        {
            foreach (Damageable damageable in listObjectNeedId)
            {
                if (damageable.indexDamageable == index)
                    damageable.SetHealth(0);
            }
        }
    }

    public static void ResetDataScene()
    {
        SaveManager.instance.dataSave.IndexCheckPoint = 0;

        instance.listIndexDeadEnemy.Clear();
        instance.listIndexDestroyDestructable.Clear();
        instance.listDataInteractableSaves.Clear();

        instance.dataSceneToSave.listEnemyIsDead.Clear();
        instance.dataSceneToSave.listDestructibleIsDestroy.Clear();
        instance.dataSceneToSave.listDataInteractableSave.Clear();

        SaveManager.instance.Save();
    }

    public static void ResetAllData()
    {
        SaveManager.ResetSave();
    }
}
