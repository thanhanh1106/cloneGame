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
        Vector3 posCam = cameraMain.transform.position;
        transform.LookAt(new Vector3(posCam.x,transform.rotation.y,posCam.z));
    }
}
