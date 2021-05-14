using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRes1 : MonoBehaviour
{
    #region 変数宣言
    // StageManager変数
    [SerializeField] private StageManager stageManager;
    // PlayerController変数
    [SerializeField] private PlayerController player;

    // ステージ１の一つ目のリスポーン位置
    [SerializeField] private Vector3 playerResPos;

    // プレイヤーがすり抜けたか
    [SerializeField] private bool isPassing;
    #endregion

    /// <summary>
    /// もしオブジェクトをすり抜けたら
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Playerタグのオブジェクトかつまだ一つ目のリスポーン位置をすり抜けていなければ
        if (other.gameObject.tag == "Player" && !isPassing)
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
        // ステージ１の一つ目のリスポーン位置を渡す
        player.SetPlayerRes = playerResPos;

        // リスポーン位置通過時のnowTimeを保存する
        //gameManager.TransTime = gameManager.NowTime;

        // すり抜けた
        isPassing = true;
    }
}
