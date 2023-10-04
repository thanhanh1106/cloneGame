using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ButtonGun : MonoBehaviour
{
    public WeaponName weaponName;
    public Action<WeaponName> OnClickSwapGun;
    public void OnClick()
    {

        OnClickSwapGun?.Invoke(weaponName);
    }
}
