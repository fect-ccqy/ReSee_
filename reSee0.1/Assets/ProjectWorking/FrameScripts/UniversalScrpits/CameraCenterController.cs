using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenterController : MonoBehaviour
{
    [SerializeField] private CameraMoveContent theCamerMoveContent;
    private BoxCollider2D theBoxCollider2D;
    private void OnMouseUpAsButton()
    {
        if(GameManager.gameManagerInstance.CameraMove(theCamerMoveContent, this) == 0)
        {
            theBoxCollider2D.enabled = false;

        }
        else
        {

            print("CameraCenterController move camera error ");
        }
    }
    private void Awake()
    {
        theBoxCollider2D = GetComponent<BoxCollider2D>();
    }


    public void ActiveCollider()
    {

        theBoxCollider2D.enabled = true;
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
