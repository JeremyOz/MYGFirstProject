using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CustomEditor(typeof(AutomaticDefineInfoPostOnButton))]
public class AutomaticDefineInfoPostOnButton_Inspector : Editor
{ 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutomaticDefineInfoPostOnButton myScript = (AutomaticDefineInfoPostOnButton)target;
        if (GUILayout.Button("Define All InfoPost Events OnClick"))
        {
            myScript.OnButtonClick();
        }
    }
}

[ExecuteInEditMode]
public class AutomaticDefineInfoPostOnButton : MonoBehaviour
{
    public InteractOnButton2D[] array;

    // Start is called before the first frame update
    public void OnButtonClick()
    {
        for (int i = 0; i < array.Length-1; i++)
        {
            System.Reflection.MethodInfo targetInfo;

            targetInfo = UnityEventBase.GetValidMethodInfo(array[i + 1].gameObject, nameof(GameObject.SetActive), new Type[] { typeof(bool) });
            UnityAction<bool> methodDelegate = Delegate.CreateDelegate(typeof(UnityAction<bool>), array[i + 1].gameObject, targetInfo, true) as UnityAction<bool>;
            UnityEditor.Events.UnityEventTools.AddBoolPersistentListener(array[i].OnButtonPress, methodDelegate, true);
            
            targetInfo = UnityEventBase.GetValidMethodInfo(array[i].gameObject, nameof(GameObject.SetActive), new Type[] { typeof(bool) });
            methodDelegate = Delegate.CreateDelegate(typeof(UnityAction<bool>), array[i].gameObject, targetInfo, false) as UnityAction<bool>;
            UnityEditor.Events.UnityEventTools.AddBoolPersistentListener(array[i].OnButtonPress, methodDelegate, false);
        }
    }
}
