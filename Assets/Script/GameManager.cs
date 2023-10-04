using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-100)] // để cho script này sẽ chạy trước các script khác, chắc chắn giá trị của nó được khởi tạo trước, tránh bị null 
// thứ tự gọi từ nhỏ đến lớn với mặc định khi ko cài lại là 0
public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        MakeSingleton(true);
        Player = FindObjectOfType<PlayerBrain>();
    }

    public List<WayPoint> EnemiesWayPoints;
    public List<EnemyBrain> Enemies;
    public CharacterBrain Player;
    public UI_SwapGun UiSwapGunManager;


}
