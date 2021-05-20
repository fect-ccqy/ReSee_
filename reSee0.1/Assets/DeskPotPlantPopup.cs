using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskPotPlantPopup : SceneObjWithState<DeskPotPlantState>
{
    private bool isGrowing=false;

    [SerializeField] private PropContent deskHammerContent;
    [SerializeField] private Image potPlantImage;


    [SerializeField] private Sprite WaitSeedSprite;
    [SerializeField] private Sprite HaveSeedSprite;
    [SerializeField] private Sprite HaveFruitSprite;
    [SerializeField] private Sprite FinishSprite;


    [SerializeField] private Sprite[] plantAnimSprites;


    [SerializeField] private GameObject fruitClick;
    [SerializeField] private GameObject fruit;
    [SerializeField] private Sprite[] fruitSprites;

    [SerializeField] private GameObject hammerImage;

    private bool isHammerOut=false;


    private void Awake()
    {

        SetSelfViewByState();
    }


    public override void ObjTrigger(string eventName)
    {
        print(eventName);



        DeskPotPlantState tStateValue = GetDicStateValue();
        if (GameManager.gameManagerInstance.GetIsGlobalRespondMouse())
        {

            if (eventName == "PlantClick")
            {
                if (tStateValue == DeskPotPlantState.WaitSeed
                    && GameManager.gameManagerInstance.GetNowChosenProp() == PropName.DeskDrawerSeed)
                {

                    GameManager.gameManagerInstance.DeleteProp(PropName.DeskDrawerSeed);
                    SetDicStateValue(DeskPotPlantState.HaveSeed);
                    SetSelfViewByState();
                    GameManager.gameManagerInstance.SetPopupSceneObjView();

                }

                if (tStateValue == DeskPotPlantState.HaveSeed
                    && GameManager.gameManagerInstance.GetNowChosenProp() == PropName.DeskShovel)
                {

                    GameManager.gameManagerInstance.DeleteProp(PropName.DeskShovel);

                    isGrowing = true;
                    SetDicStateValue(DeskPotPlantState.HaveFruit);

                    GameManager.gameManagerInstance.SetPopupSceneObjView();

                    //开启动画播放协程
                    StartCoroutine(PlayPlantGrowing());

                }


            }
            else if (eventName == "FruitClick")
            {

                if(tStateValue == DeskPotPlantState.HaveFruit)
                {

                    StartCoroutine(PlayGettingHammer());
                }

            }


            else if (eventName == "ClickHammerFruit")
            {
                if (isHammerOut)
                {
                    Vector3 tPosition = hammerImage.GetComponent<RectTransform>().position;
                    tPosition.z = 0;
                    deskHammerContent.startPosition = tPosition;
                    //
                    //print(fruit.GetComponent<RectTransform>().position);
                    if (GameManager.gameManagerInstance.AddProp(deskHammerContent) == 0)
                    {

                        fruit.SetActive(false);
                        hammerImage.SetActive(false);

                        SetDicStateValue(DeskPotPlantState.Finish);

                        SetSelfViewByState();

                        GameManager.gameManagerInstance.SetPopupSceneObjView();


                    }



                }



            }



        }

        //throw new System.NotImplementedException();
    }


    IEnumerator PlayPlantGrowing()
    {

        GameManager.gameManagerInstance.SetIsGlobalObjRespondMouse(false);

        float speed=4;

        for(int i = 0; i < plantAnimSprites.Length; i++)
        {
            for (float timer = 0; timer < 1; timer += Time.deltaTime * speed)
            {
                yield return 0;
            }
            potPlantImage.sprite = plantAnimSprites[i];


        }




        SetSelfViewByState();

        GameManager.gameManagerInstance.SetIsGlobalObjRespondMouse(true);

    }


    IEnumerator PlayGettingHammer()
    {
        GameManager.gameManagerInstance.SetIsGlobalObjRespondMouse(false);

        potPlantImage.sprite = FinishSprite;

        float speed1=1.5f;

        fruit.SetActive(true);

        RectTransform fruitTransfrom = fruit.GetComponent<RectTransform>();
        Image fruitImage = fruit.GetComponent<Image>();

        Vector2 startPosition = fruitTransfrom.anchoredPosition;
        Vector2 targetPosition = new Vector2(-23f, -32f);
        Vector2 deltaPosition = targetPosition - startPosition;

        for (float timer = 0; timer < 1; timer += Time.deltaTime* speed1)
        {
            fruitTransfrom.anchoredPosition = startPosition + deltaPosition * timer;
            yield return 0;
        }

        fruitTransfrom.anchoredPosition = targetPosition;
        yield return 0;


        float speed2 = 2.5f;

        for (int i = 0; i < fruitSprites.Length; i++)
        {

            fruitImage.sprite = fruitSprites[i];
            for (float timer = 0; timer < 1; timer += Time.deltaTime * speed2)
            {
                yield return 0;
            }


        }
        hammerImage.SetActive(true);
        isHammerOut = true;
        yield return 0;


        GameManager.gameManagerInstance.SetPopupSceneObjView();


        GameManager.gameManagerInstance.SetIsGlobalObjRespondMouse(true);



    }


    protected override void SetSelfViewByState()
    {


        DeskPotPlantState tStateValue = GetDicStateValue();

        switch (tStateValue)
        {
            case DeskPotPlantState.WaitSeed:
                potPlantImage.sprite = WaitSeedSprite;

                break;


            case DeskPotPlantState.HaveSeed:
                potPlantImage.sprite = HaveSeedSprite;
                break;



            case DeskPotPlantState.HaveFruit:
                potPlantImage.sprite = HaveFruitSprite;
                fruitClick.SetActive(true);
                break;

            case DeskPotPlantState.Finish:
                potPlantImage.sprite = FinishSprite;
                break;


            default:
                break;


        }


    }


    public void PlantClick()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }



}
