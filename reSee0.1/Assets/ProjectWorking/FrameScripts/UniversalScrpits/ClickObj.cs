using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    [SerializeField] InteractiveObj theListener;
    [SerializeField] string eventName;
    private void OnMouseUpAsButton()
    {
        if (GameManager.gameManagerInstance.GetIsGlobalObjRespondMouse())
        {
            theListener.ObjTrigger(eventName);

        }

        
    }

}
