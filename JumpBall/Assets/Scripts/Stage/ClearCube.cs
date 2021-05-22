using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCube : MonoBehaviour
{
    // StageManager変数
    [SerializeField] private StageManager stageManager;

    /// <summary>
    /// もしオブジェクトをすり抜けたら
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Playerタグのオブジェクトかつゲームの状態がGAMEなら
        if (other.gameObject.CompareTag("Player") && stageManager.gameState == StageManager.GameState.GAME)
        {
            // ゲームの状態を更新する
            UpdateGameState();
        }
    }

    /// <summary>
    /// ゲームの状態を更新する
    /// </summary>
    private void UpdateGameState()
    {
        // ゲームの状態をクリアに変更
        stageManager.gameState = StageManager.GameState.CLEAR;
    }
}
