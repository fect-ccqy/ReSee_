using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
 
public enum DeskPotPlantState
{
    WaitSeed,
    HaveSeed,
    Growing,
    HaveFruit,
    FruitFall,
    GetHammer,
    Finish

}
 
 */



public enum DeskPotPlantState
{
    WaitSeed,
    HaveSeed,
    HaveFruit,
    Finish

}

public class DeskPotPlant : SceneObjWithState<DeskPotPlantState>
{
    [SerializeField] private GameObject popupPrefab;

    [SerializeField] private Sprite WaitSeedSprite;
    [SerializeField] private Sprite HaveSeedSprite;
    [SerializeField] private Sprite HaveFruitSprite;
    [SerializeField] private Sprite FinishSprite;


    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitAddDicKeyStateValue(DeskPotPlantState.WaitSeed);

        SetSelfViewByState();
    }

    private void OnMouseUpAsButton()
    {
        ObjTrigger("MouseClick");
    }


    public override void ObjTrigger(string eventName)
    {
        if (GameManager.gameManagerInstance.GetIsSceneObjRespondMouse())
        {

            if (eventName == "MouseClick")
            {
                GameManager.gameManagerInstance.ShowPopUp(popupPrefab, this);
            }
        }
    }

    public override void SetSelfSceneObjByState()
    {
        SetSelfViewByState();
    }

    protected override void SetSelfViewByState()
    {
        DeskPotPlantState tStateValue = GetDicStateValue();

        switch (tStateValue)
        {
            case DeskPotPlantState.WaitSeed:
                spriteRenderer.sprite = WaitSeedSprite;

                break;


            case DeskPotPlantState.HaveSeed:
                spriteRenderer.sprite = HaveSeedSprite;
                break;



            case DeskPotPlantState.HaveFruit:
                spriteRenderer.sprite = HaveFruitSprite;
                break;

            case DeskPotPlantState.Finish:
                spriteRenderer.sprite = FinishSprite;
                break;


            default:
                break;


        }



    }


    /*   
     
     



    [SerializeField] private Sprite emptyPot, potWithSeed, potWithFruit, finishPot;
    [SerializeField] private PropContent deskHammerContent;
    [SerializeField] private GameObject potAnimationObj;
    [SerializeField] private GameObject fruitObj;

    [SerializeField] private Vector3 theFruitFallPosisiton;
    [SerializeField]private Sprite fruitSprite, fruitWithHammer;

    [SerializeField] private GameObject thePopupObj;

    private SpriteRenderer fruitRenderer;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fruitRenderer= fruitObj.GetComponent<SpriteRenderer>();
        InitAddDicKeyStateValue(DeskPotPlantState.WaitSeed);

        SetSelfViewByState();
    }

    public override void ObjTrigger(string eventName)
    {
        DeskPotPlantState tStateValue = GetDicStateValue();


        if (eventName == "ClickFlowerpot")
        {
            if (tStateValue == DeskPotPlantState.WaitSeed
                && GameManager.gameManagerInstance.GetNowChosenProp() == PropName.DeskDrawerSeed)
            {

                GameManager.gameManagerInstance.DeleteProp(PropName.DeskDrawerSeed);
                SetDicStateValue(DeskPotPlantState.HaveSeed);
                SetSelfViewByState();


            }

            if (tStateValue == DeskPotPlantState.HaveSeed
                && GameManager.gameManagerInstance.GetNowChosenProp() == PropName.DeskShovel)
            {

                GameManager.gameManagerInstance.DeleteProp(PropName.DeskShovel);
                SetDicStateValue(DeskPotPlantState.Growing);
                SetSelfViewByState();


            }


        }
        else if (eventName == "GrowingFinish")
        {
            if (tStateValue == DeskPotPlantState.Growing)
            {

                SetDicStateValue(DeskPotPlantState.HaveFruit);
                SetSelfViewByState();


            }

        }
        else if (eventName == "ClickFruit")
        {
            if (tStateValue == DeskPotPlantState.HaveFruit)
            {

                SetDicStateValue(DeskPotPlantState.FruitFall);
                SetSelfViewByState();

            }

            if (tStateValue == DeskPotPlantState.FruitFall && GameManager.gameManagerInstance.AddPropWithCallBack(deskHammerContent, this, "GetHammerCallBack") == 0)
            {
                SetDicStateValue(DeskPotPlantState.GetHammer);

                SetSelfViewByState();

            }





        }
        else if (eventName == "GetHammerCallBack")
        {

            if (tStateValue == DeskPotPlantState.GetHammer)
            {
                SetDicStateValue(DeskPotPlantState.Finish);

                SetSelfViewByState();

            }

        }

    }




    protected override void SetSelfViewByState()
    {
        DeskPotPlantState tStateValue = GetDicStateValue();

        switch (tStateValue)
        {
            case DeskPotPlantState.WaitSeed:
                spriteRenderer.sprite = emptyPot;

                fruitObj.SetActive(false);
                potAnimationObj.SetActive(false);
                break;


            case DeskPotPlantState.HaveSeed:
                spriteRenderer.sprite = potWithSeed;
                fruitObj.SetActive(false);
                potAnimationObj.SetActive(false);
                break;

            case DeskPotPlantState.Growing:
                spriteRenderer.sprite = potWithSeed;
                fruitObj.SetActive(false);
                potAnimationObj.SetActive(true);
                break;

            case DeskPotPlantState.HaveFruit:
                spriteRenderer.sprite = potWithFruit;
                fruitObj.SetActive(true);
                fruitRenderer.sprite = null;
                potAnimationObj.SetActive(false);
                break;

            case DeskPotPlantState.FruitFall:

                spriteRenderer.sprite = finishPot;

                fruitObj.SetActive(true);
                theFruitFallPosisiton.z = fruitObj.transform.position.z;
                fruitRenderer.sprite = fruitSprite;
                fruitObj.transform.position = theFruitFallPosisiton;
                potAnimationObj.SetActive(false);

                break;


            case DeskPotPlantState.GetHammer:
                spriteRenderer.sprite = finishPot;
                fruitRenderer.sprite = fruitWithHammer;
                fruitObj.SetActive(true);
                potAnimationObj.SetActive(false);
                break;


            case DeskPotPlantState.Finish:
                spriteRenderer.sprite = finishPot;
                fruitObj.SetActive(false);
                potAnimationObj.SetActive(false);
                break;


            default:
                break;


        }





    }


    private void OnMouseUpAsButton()
    {
        
      


        print("click");
        GameManager.gameManagerInstance.ShowPopUp(thePopupObj, this);

    }

     
     */






    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
