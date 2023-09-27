using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ui_CharaterStats : MonoBehaviour
{
    Canvas canvas;
    Camera cameraMain;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        cameraMain = Camera.main;
        canvas.worldCamera = cameraMain;
    }

    private void LateUpdate()
    {
        transform.forward = cameraMain.transform.forward;
    }
}
