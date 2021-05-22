using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRes : MonoBehaviour
{
    #region 変数宣言
    // TimeManager変数
    [SerializeField] private TimeManager timeManager;
    // PlayerController変数
    [SerializeField] private PlayerController player;

    // リスポーン位置
    [SerializeField] private Vector3 playerResPos;

    // プレイヤーがすり抜けたか
    [SerializeField] private bool isPassing;
    #endregion

    /// <summary>
    /// もしオブジェクトをすり抜けたら
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Playerタグのオブジェクトかつまだリスポーン位置をすり抜けていなければ
        if (other.gameObject.CompareTag("Player") && !isPassing)
        {
            //  // プレイヤーのリスポーン地点更新
            UpdatePlayerRes();
        }
    }

    /// <summary>
    /// プレイヤーのリスポーン地点更新
    /// </summary>
    private void UpdatePlayerRes()
    {
        // リスポーン位置更新
        player.SetPlayerRes = playerResPos;

        // nowタイムをリスポーン地点通過時の時間に変更
        timeManager.GetSetPassingTime = timeManager.GetSetNowTime;

        // プレイヤーのリスポーン地点が更新された
        if (!player.GetSetIsUpdateResPos) player.GetSetIsUpdateResPos = true;

         // すり抜けた
        isPassing = true;
    }
}
