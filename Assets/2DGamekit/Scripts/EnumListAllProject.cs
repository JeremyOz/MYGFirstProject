using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnumSceneName
{
    Undefined,
    SceneAIntroduction,
    SceneBGrotte,
    SceneCJungle,
    SceneDRuine,
    SceneEAscenseurRuine,
    SceneFAntreMatriarche,
    SceneGOutro,
}

[System.Serializable]
public enum EnumTypeOfInteractable
{
    PushableBox,
    Switch,
    Health,
    ItemInventory,
    PlayableDirector,
}
