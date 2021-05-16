using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DeskLampState
{
    On,
    Off



}


public class DeskLamp : SceneObjWithState<DeskLampState>
{
    DeskLampState tDeskLampState;
    [SerializeField] private SpriteRenderer theBlackGround;
    [SerializeField] private GameObject theLight;
    [SerializeField] Color offGroundColor;
    [SerializeField] Color onGroundColor;


    public override void ObjTrigger(string eventName)
    {
        if (eventName == NormalTriggers.mouseClick)
        {
            tDeskLampState = GetDicStateValue();

            switch (tDeskLampState)
            {

                case DeskLampState.Off:

                    if(DicDataReader.IsDicStateExist(DicDataReader.SceneObjDataDicName, "DeskPlug")
                        && DicDataReader.GetDicStateValue<DeskPlugState>(DicDataReader.SceneObjDataDicName, "DeskPlug") == DeskPlugState.On)
                    {

                        SetDicStateValue(DeskLampState.On);
                        SetSelfViewByState();

                    }

                    
                    break;


                case DeskLampState.On:
                    break;


                default:
                    break;
            }

        }




    }


    private void OnMouseUpAsButton()
    {
        if (GameManager.gameManagerInstance.GetIsSceneObjRespondMouse())
        {
            ObjTrigger(NormalTriggers.mouseClick);
            //SetSelfViewByState();

        }
    }


    protected override void SetSelfViewByState()
    {
        tDeskLampState = GetDicStateValue();
        if (tDeskLampState == DeskLampState.Off)
        {
            theBlackGround.color = offGroundColor;
            theLight.SetActive(false);


        }
        else if(tDeskLampState == DeskLampState.On)
        {
            theBlackGround.color = onGroundColor;
            theLight.SetActive(true);

        }
    }



    private void Awake()
    {
        InitAddDicKeyStateValue(DeskLampState.Off);

        SetSelfViewByState();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
