using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSave
{
    // Player Info
    public string username;
    public bool isSaved; // Indique si une sauvegarde existe

    // Actual Scene Info
    public EnumSceneName enumScene;
    public int indexCheckPoint;

    // Character Info
    public int life;
    public int indexEvolveEllen;
    public bool rangedAttackIsActive;
    public bool meleeAttackIsActive;

    // DataScene Info
    public DataSceneToSave dataSceneToSaveSceneB;
    public DataSceneToSave dataSceneToSaveSceneC;
    public DataSceneToSave dataSceneToSaveSceneD;
    public DataSceneToSave dataSceneToSaveSceneE;
    public DataSceneToSave dataSceneToSaveSceneF;
    public DataSceneToSave dataSceneToSaveSceneG;

    public int IndexCheckPoint
    {
        get { return indexCheckPoint; }

        set
        {
            indexCheckPoint = value;
        }
    }

    public bool RangedAttackIsActive
    {
        get { return rangedAttackIsActive; }

        set
        {
            rangedAttackIsActive = value;
        }
    }

    public bool MeleeAttackIsActive
    {
        get { return meleeAttackIsActive; }

        set
        {
            meleeAttackIsActive = value;
        }
    }

    public int Life
    {
        get { return life; }

        set
        {
            life = value;
        }
    }

    public int IndexEvolveEllen
    {
        get { return indexEvolveEllen; }

        set
        {
            indexEvolveEllen = value;
        }
    }

    public DataSave()
    {
        username = "Undefined";
        isSaved = false;

        enumScene = EnumSceneName.SceneBGrotte;
        indexCheckPoint = 0;

        rangedAttackIsActive = false;
        meleeAttackIsActive = false;
        life = 4;
        indexEvolveEllen = 0;

        dataSceneToSaveSceneB = new DataSceneToSave();
        dataSceneToSaveSceneC = new DataSceneToSave();
        dataSceneToSaveSceneD = new DataSceneToSave();
        dataSceneToSaveSceneE = new DataSceneToSave();
        dataSceneToSaveSceneF = new DataSceneToSave();
        dataSceneToSaveSceneG = new DataSceneToSave();

        dataSceneToSaveSceneB.enumSceneName = EnumSceneName.SceneBGrotte;
        dataSceneToSaveSceneC.enumSceneName = EnumSceneName.SceneCJungle;
        dataSceneToSaveSceneD.enumSceneName = EnumSceneName.SceneDRuine;
        dataSceneToSaveSceneE.enumSceneName = EnumSceneName.SceneEAscenseurRuine;
        dataSceneToSaveSceneF.enumSceneName = EnumSceneName.SceneFAntreMatriarche;
        dataSceneToSaveSceneG.enumSceneName = EnumSceneName.SceneGOutro;
    }

    public void UpdateDataSave(DataSceneToSave dataSceneToSave)
    {
        switch (dataSceneToSave.enumSceneName)
        {
            case EnumSceneName.SceneBGrotte:
                dataSceneToSaveSceneB = dataSceneToSave;
                break;

            case EnumSceneName.SceneCJungle:
                dataSceneToSaveSceneC = dataSceneToSave;
                break;

            case EnumSceneName.SceneDRuine:
                dataSceneToSaveSceneD = dataSceneToSave;
                break;

            case EnumSceneName.SceneEAscenseurRuine:
                dataSceneToSaveSceneE = dataSceneToSave;
                break;

            case EnumSceneName.SceneFAntreMatriarche:
                dataSceneToSaveSceneF = dataSceneToSave;
                break;

            case EnumSceneName.SceneGOutro:
                dataSceneToSaveSceneG = dataSceneToSave;
                break;
        }

        if (!isSaved)
            isSaved = true;

        SaveManager.instance.Save();
    }

    public DataSceneToSave GetDataSave(EnumSceneName enumSceneName)
    {
        switch (enumSceneName)
        {
            case EnumSceneName.SceneBGrotte:
                return dataSceneToSaveSceneB;
            case EnumSceneName.SceneCJungle:
                return dataSceneToSaveSceneC;
            case EnumSceneName.SceneDRuine:
                return dataSceneToSaveSceneD;
            case EnumSceneName.SceneFAntreMatriarche:
                return dataSceneToSaveSceneF;
            case EnumSceneName.SceneGOutro:
                return dataSceneToSaveSceneG;
        }

        return new DataSceneToSave();
    }
}
