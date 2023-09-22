using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility 
{

    // thằng này dùng để tránh bị lỗi null do component đó chưa được khởi tạo đã bị gọi tới
    public static T TryGetComponent<T>(this MonoBehaviour monoBehaviour, ref T component)
    {
        if(component == null)
        {
            component = monoBehaviour.gameObject.GetComponent<T>();
        }
        return component;
    }

    public static void DelayLamda(this MonoBehaviour monoBehaviour,Action lamda, float timeDelay)
    {
        monoBehaviour.StartCoroutine(IEDelay(lamda, timeDelay));
    }

    private static IEnumerator IEDelay(Action callBack,float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        callBack?.Invoke();
    }
}
