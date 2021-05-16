using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] GameObject backGround;
    private bool isShowPopup=false;
    private GameObject nowPropup;
    //private bool isBackgorundRespondMouse=true;
    private InteractiveObj popupObj;


    public bool GetIsShowPopup()
    {
        return isShowPopup;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ShowPopUp(GameObject thePopup, InteractiveObj tpopupObj)
    {
        if (!isShowPopup)
        {
            popupObj = tpopupObj;
            isShowPopup = true;
            nowPropup = Instantiate(thePopup, transform);
            backGround.SetActive(true);
            return 0;
        }
        else
        {
            return -1;
        }
    }


    public int ClosePopup()
    {
        if (isShowPopup)
        {
            SetPopupSceneObjView();
            isShowPopup = false;
            DestroyImmediate(nowPropup);

            backGround.SetActive(false);

            return 0;
        }
        else
        {
            return -1;
        }
    }


    public int SetPopupSceneObjView()
    {
        if (isShowPopup)
        {
            popupObj.SetSelfSceneObjByState();

            return 0;
        }
        else
        {
            return -1;
        }
    }


    public void popupReturn()
    {
        if (GameManager.gameManagerInstance.GetIsGlobalRespondMouse())
        {
            ClosePopup();
        }
    }


}
