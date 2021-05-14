using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    // DontDestroyOnLoad化したオブジェクト
    private static GameObject dontDestroyObject;

    private void Awake()
    {
        // DontDestroyOnLoad化したオブジェクトがnulllなら
        if (dontDestroyObject == null)
        {
            // アタッチしたオブジェクトをDontDestroyOnLoad化
            DontDestroyOnLoad(gameObject);
            // アタッチしたオブジェクトをDontDestroyOnLoad化したオブジェクトへ
            dontDestroyObject = gameObject;
        }
        else
        {
            // DontDestroyOnLoad化したオブジェクトを削除
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// DontDestroyOnLoad化したdontDestroyObjectを返すゲッター
    /// </summary>
    public static GameObject GetGameObject
    {
        get { return dontDestroyObject; }
    }
}
