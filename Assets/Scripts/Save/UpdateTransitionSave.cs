using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTransitionSave : MonoBehaviour
{
    public TransitionPoint transitionPoint;

    // Start is called before the first frame update
    void Start()
    {
        transitionPoint.newSceneName = SaveManager.instance.dataSave.enumScene.ToString();
        transitionPoint.transitionDestinationTag = (SceneTransitionDestination.DestinationTag)SaveManager.instance.dataSave.indexCheckPoint;
    }
}
