using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DeskShovelState
{
    Exist,
    notExist

}



public class DeskShovel : SceneObjWithState<DeskShovelState>
{
    DeskShovelState tDeskShovelState;
    [SerializeField] private PropContent theDeskShovelPropContent;


    private void Awake()
    {

        InitAddDicKeyStateValue(DeskShovelState.Exist);

        SetSelfViewByState();
    }


    public override void ObjTrigger(string eventName)
    {
        if (eventName == NormalTriggers.mouseClick)
        {
            tDeskShovelState = GetDicStateValue();

            switch (tDeskShovelState)
            {

                case DeskShovelState.Exist:
                    if (GameManager.gameManagerInstance.AddProp(theDeskShovelPropContent) == 0)
                    {

                        SetDicStateValue(DeskShovelState.notExist);

                        SetSelfViewByState();

                    }

                    break;


                case DeskShovelState.notExist:

                    break;


                default:
                    break;
            }

        }
    }


    private void OnMouseUpAsButton()
    {
        if (GameManager.gameManagerInstance.GetIsGlobalObjRespondMouse())
        {
            ObjTrigger(NormalTriggers.mouseClick);
            //SetSelfViewByState();

        }
    }

    protected override void SetSelfViewByState()
    {
        tDeskShovelState = GetDicStateValue();
        if (tDeskShovelState == DeskShovelState.Exist)
        {
            gameObject.SetActive(true);
        }
        else if (tDeskShovelState == DeskShovelState.notExist)
        {
            gameObject.SetActive(false);
        }



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
