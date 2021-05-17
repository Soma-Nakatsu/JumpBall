using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCube : MonoBehaviour
{
    // StageManager変数
    [SerializeField] private StageManager stageManager;

    /// <summary>
    /// もしオブジェクトをすり抜けたら
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Playerタグのオブジェクトかつゲームの状態がNONEなら
        if (other.gameObject.CompareTag("Player") && stageManager.gameState == StageManager.GameState.NONE)
        {
            // ゲームの状態を更新する
            UpdateGameState();
        }
    }

    /// <summary>
    /// ゲームの状態を更新する
    /// </summary>
    private void  UpdateGameState()
    {
        // ゲームの状態をゲーム中に変更
        stageManager.gameState = StageManager.GameState.GAME;
    }
}
