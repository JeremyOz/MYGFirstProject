using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DataInteractableSave
{
    public int indexInteractable;

    public bool isUsed;

    public float savePosX;
    public float savePosY;
}

public class DataInteractableObjects : MonoBehaviour
{
    public static int nextIndexInteractable;
    public EnumTypeOfInteractable enumTypeOfInteractable;

    public DataInteractableSave dataInteractableSave;

    private void Start()
    {
        UpdateDataSave();

        Begin();
    }

    public void UpdateDataSave()
    {
        bool isSaved = false;

        if (SaveManager.instance.dataSave.isSaved)
        {
            foreach (DataInteractableSave dataInteractableSave in DataScene.instance.listDataInteractableSaves)
            {
                if (this.dataInteractableSave.indexInteractable == dataInteractableSave.indexInteractable)
                {
                    isSaved = true;
                    this.dataInteractableSave = dataInteractableSave;
                    continue;
                }
            }
        }     

        if(!isSaved)
        {
            DataScene.instance.listDataInteractableSaves.Add(dataInteractableSave);
        }
    }

    public void Begin()
    {
        if (dataInteractableSave.isUsed && enumTypeOfInteractable == EnumTypeOfInteractable.Switch)
        {
            gameObject.GetComponent<InteractOnTrigger2D>().OnEnter.Invoke();
        }

        else if (dataInteractableSave.isUsed && enumTypeOfInteractable == EnumTypeOfInteractable.Health)
        {
            gameObject.SetActive(false);
        }

        else if (dataInteractableSave.isUsed && enumTypeOfInteractable == EnumTypeOfInteractable.PushableBox)
        {
            Vector3 posSaved = new Vector3(dataInteractableSave.savePosX, dataInteractableSave.savePosY, 0);
            gameObject.transform.localPosition = posSaved;
        }

        else if (dataInteractableSave.isUsed && enumTypeOfInteractable == EnumTypeOfInteractable.ItemInventory)
        {
            gameObject.GetComponent<InventoryItem>().LoadingSaveForceAddItem();
            gameObject.GetComponent<InteractOnTrigger2D>().OnEnter.Invoke();
            gameObject.GetComponent<InteractOnTrigger2D>().OnExit.Invoke();
        }

        else if (dataInteractableSave.isUsed && enumTypeOfInteractable == EnumTypeOfInteractable.PlayableDirector)
        {
            gameObject.GetComponent<PlayableDirector>().enabled = false;
        }

        if (enumTypeOfInteractable == EnumTypeOfInteractable.PushableBox)
        {
            StartCoroutine(UpdatePositionPushableBox());
        }
    }


    public void SetIndex()
    {
        dataInteractableSave.indexInteractable = nextIndexInteractable;
        nextIndexInteractable++;
    }

    public void Used()
    {
        dataInteractableSave.isUsed = true;
    }

    private IEnumerator UpdatePositionPushableBox()
    {
        yield return new WaitUntil(() => dataInteractableSave.savePosX != gameObject.transform.position.x || dataInteractableSave.savePosY != gameObject.transform.position.y);

        dataInteractableSave.savePosX = gameObject.transform.localPosition.x;
        dataInteractableSave.savePosY = gameObject.transform.localPosition.y;

        StartCoroutine(UpdatePositionPushableBox());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacter c = collision.GetComponent<PlayerCharacter>();

        if (c != null)
        {
            Used();
        }
    }
}
