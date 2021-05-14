using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // enum宣言
    public enum GameState
    {
        NONE,   // スタート地点から出ていない
        GAME,    // ゲーム中
        CLEAR,  // ゲームクリア
    }
    // enum変数宣言
    [SerializeField] public GameState gameState = GameState.NONE;

    // プレイヤー変数
    [SerializeField] private PlayerController player;
    // カメラ変数
    [SerializeField] private CameraController camController;

    void Start()
    {
        // プレイヤー初期化
        player.Init();
        // カメラ初期化
        camController.Init();
    }

    void Update()
    {
        // プレイヤー更新
        player.Run();
        // カメラ更新
        camController.Run();
        // 各ステートごとの更新処理
        UpdateGameState();

    }

    // 各ステートごとの更新処理
    private void UpdateGameState()
    {
        switch (gameState)
        {
            // スタート地点から出ていない
            case GameState.NONE:
                break;

            // ゲーム中
            case GameState.GAME:
                break;

            // ゲームクリア
            case GameState.CLEAR:
                // プレイヤーを移動無効にする
                player.SetMoveInvalidFlag = true;
                break;
        }
    }
}
