using UnityEngine;

public class StageManager : MonoBehaviour
{
    // enum宣言
    public enum GameState
    {
        NONE,   // スタート地点から出ていない
        GAME,   // ゲーム中
        CLEAR,  // ゲームクリア
    }
    // enum変数宣言
    [SerializeField] public GameState gameState = GameState.NONE;

    // TimeManager変数
    [SerializeField] private TimeManager timeManager;
    // プレイヤー変数
    [SerializeField] private PlayerController player;
    // カメラ変数
    [SerializeField] private CameraController camController;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        // プレイヤー初期化
        player.Init();
        // カメラ初期化
        camController.Init();
        // TimeManager初期化
        StartCoroutine(timeManager.Init());
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // プレイヤー更新
        player.Run();
        // プレイヤーが死んだらStageManager側のプレイヤー死亡処理を行う
        if (player.GetSetIsDead) IsDeadPlayer();
        // カメラ更新
        camController.Run();
        // 各ステートごとの更新処理
        UpdateGameState();
    }

    /// <summary>
    /// 各ステートごとの更新処理
    /// </summary>
    private void UpdateGameState()
    {
        switch (gameState)
        {
            // スタート地点から出ていない
            case GameState.NONE:
                break;

            // ゲーム中
            case GameState.GAME:
                // nowTimeカウント開始
                timeManager.GetSetNowTime += Time.deltaTime;
                break;

            // ゲームクリア
            case GameState.CLEAR:
                // プレイヤーを移動無効にする
                player.SetMoveInvalidFlag = true;
                break;
        }
    }

    /// <summary>
    /// StageManager側のプレイヤー死亡処理
    /// </summary>
    private void IsDeadPlayer()
    {
        // リスポーン地点が更新されていたらゲーム状態をNONEへ
        if (player.GetSetIsUpdateResPos) gameState = GameState.NONE;

        // nowタイムをチェックポイント(リスポーン地点)通過時の時間に戻す
        timeManager.GetSetNowTime = timeManager.GetSetPassingTime;
    }
}
