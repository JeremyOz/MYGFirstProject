using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DataSceneToSave
{
    public EnumSceneName enumSceneName;

    public List<int> listEnemyIsDead = new List<int>();
    public List<int> listDestructibleIsDestroy = new List<int>();
    public List<DataInteractableSave> listDataInteractableSave = new List<DataInteractableSave>();
}

