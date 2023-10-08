using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_SwapGun : MonoBehaviour
{
    [SerializeField] List<Ui_ButtonGun> buttons;
    public event Action<WeaponName> OnSwapGun;
    private void Awake()
    {
        foreach (Ui_ButtonGun button in buttons)
        {
            button.OnClickSwapGun += HandleClickSwapGun;
        }
    }

    private void HandleClickSwapGun(WeaponName name)
    {
        Debug.Log("click");
        OnSwapGun?.Invoke(name);
    }
}
