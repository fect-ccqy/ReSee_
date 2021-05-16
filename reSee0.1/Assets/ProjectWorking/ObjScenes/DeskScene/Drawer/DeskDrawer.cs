using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public struct DeskDrawerState
{
    public enum DeskDrawerSeedState
    {
        exist,notExist
    }

    public enum DeskDrawerLockState
    {
        locked,unlocked
    }

    public enum DeskOpenState
    {
        open,close
    }

    public DeskDrawerSeedState deskDrawerSeedState;
    public DeskDrawerLockState deskDrawerLockState;
    public DeskOpenState deskOpenState;



}


public class DeskDrawer : SceneObjWithState<DeskDrawerState>
{

    [SerializeField] private GameObject openDrawer;
    [SerializeField] private GameObject theSeed;
    [SerializeField] private PropContent theSeedPropContent;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void Awake()
    {
        DeskDrawerState defaultDeskDrawerState;
        defaultDeskDrawerState.deskDrawerLockState = DeskDrawerState.DeskDrawerLockState.locked;
        defaultDeskDrawerState.deskDrawerSeedState = DeskDrawerState.DeskDrawerSeedState.exist;
        defaultDeskDrawerState.deskOpenState = DeskDrawerState.DeskOpenState.close;

        InitAddDicKeyStateValue(defaultDeskDrawerState);
        SetSelfViewByState();

    }



    public override void ObjTrigger(string eventName)
    {
        DeskDrawerState tStateValue = GetDicStateValue();

        switch (eventName)
        {
            case "SeedClick":

                if(tStateValue.deskDrawerLockState==DeskDrawerState.DeskDrawerLockState.unlocked
                    &&tStateValue.deskDrawerSeedState==DeskDrawerState.DeskDrawerSeedState.exist
                    &&tStateValue.deskOpenState==DeskDrawerState.DeskOpenState.open
                    )
                {
                    tStateValue.deskDrawerSeedState = DeskDrawerState.DeskDrawerSeedState.notExist;

                    SetDicStateValue(tStateValue);
                    SetSelfViewByState();

                    GameManager.gameManagerInstance.AddProp(theSeedPropContent);

                }
                else
                {
                    print("Seed error");
                }




                break;




            case "OpenDrawerClick":
                if (tStateValue.deskDrawerLockState == DeskDrawerState.DeskDrawerLockState.unlocked
                   && tStateValue.deskOpenState == DeskDrawerState.DeskOpenState.open
                   )
                {
                    tStateValue.deskOpenState = DeskDrawerState.DeskOpenState.close;

                    SetDicStateValue(tStateValue);
                    SetSelfViewByState();

                }
                else
                {
                    print("OpenDrawer error");
                }





                break;




            case "CloseDrawerClick":
                if (tStateValue.deskDrawerLockState == DeskDrawerState.DeskDrawerLockState.unlocked
                  && tStateValue.deskOpenState == DeskDrawerState.DeskOpenState.close
                  )
                {
                    tStateValue.deskOpenState = DeskDrawerState.DeskOpenState.open;

                    SetDicStateValue(tStateValue);
                    SetSelfViewByState();

                }
                else if (tStateValue.deskDrawerLockState == DeskDrawerState.DeskDrawerLockState.locked
                  && GameManager.gameManagerInstance.GetNowChosenProp()==PropName.DeskCuKey)
                {
                    tStateValue.deskDrawerLockState = DeskDrawerState.DeskDrawerLockState.unlocked;
                    tStateValue.deskDrawerSeedState = DeskDrawerState.DeskDrawerSeedState.exist;
                    tStateValue.deskOpenState = DeskDrawerState.DeskOpenState.open;
                    GameManager.gameManagerInstance.DeleteProp(PropName.DeskCuKey);
                    print("open drawer");

                    SetDicStateValue(tStateValue);
                    SetSelfViewByState();


                }
                else
                {
                    print("CloseDrawer error");
                }

                break;


            default:

                break;
        }

    }





    protected override void SetSelfViewByState()
    {

        DeskDrawerState tStateValue=GetDicStateValue();

        if (tStateValue.deskOpenState == DeskDrawerState.DeskOpenState.close)
        {
            openDrawer.SetActive(false);
        }
        else
        {
            openDrawer.SetActive(true);

            if (tStateValue.deskDrawerSeedState == DeskDrawerState.DeskDrawerSeedState.exist)
            {
                theSeed.SetActive(true);
            }
            else
            {
                theSeed.SetActive(false);

            }



        }

    }



    private void OnMouseUpAsButton()
    {
        if (GameManager.gameManagerInstance.GetIsSceneObjRespondMouse())
        {
            ObjTrigger("CloseDrawerClick");
            //SetSelfViewByState();

        }

        
    }

}
