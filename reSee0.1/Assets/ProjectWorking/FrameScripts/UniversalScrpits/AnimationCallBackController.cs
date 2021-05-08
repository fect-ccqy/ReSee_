using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallBackController: MonoBehaviour
{
    [SerializeField] private InteractiveObj theCallBackObj;
    [SerializeField] private string CallBackEventName;

    public void CallBack()
    {
        theCallBackObj.ObjTrigger(CallBackEventName);
    }
}
