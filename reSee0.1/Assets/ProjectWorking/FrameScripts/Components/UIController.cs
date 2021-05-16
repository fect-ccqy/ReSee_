using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject cameraReturnButton;

    private void Awake()
    {
        cameraReturnButton.SetActive(false);
    }

    public void ActiveCameraReturnButton()
    {
        cameraReturnButton.SetActive(true);
    }

    public void  CameraReturnToDefault()
    {
        GameManager.gameManagerInstance.CameraMoveBackToDefault();
        cameraReturnButton.SetActive(false);
    }
}
