using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class G : MonoBehaviour
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

    #region 変数宣言
    // プレイヤー変数
    [SerializeField] private PlayerController player;
    // カメラ変数
    [SerializeField] private CameraController camera;
    // UIManagerの変数
    //private UIManager uiManager;

    // 取得音
    [SerializeField] private AudioClip sound;
    // オーディオソース変数
    AudioSource audioSource;

    //  ステージ１最速タイム
    [SerializeField] private static float stage1BestTime;
    //  ステージ２最速タイム
    [SerializeField] private static float stage2BestTime;

    // 今のタイム
    [SerializeField] private float nowTime;
    // リスポーン位置通過時のタイム
    [SerializeField] private float transitTime;
    // クリアタイム
    [SerializeField] private float clearTime;
    #endregion

    IEnumerator Start()
    {
        // コンポーネント取得
        audioSource = GetComponent<AudioSource>();

        // UIの読み込みが終わるまで下にはいかない
        yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        // UIにあるオブジェクトを取得する
       // uiManager = SceneManager.GetSceneByName("UI").GetRootGameObjects()[0].GetComponent<UIManager>();

        //// ステージ１をプレイ中なら
        //if (SceneManager.GetActiveScene().name == "Stage1")
        //{
        //    // UIManegerにbestTimeを渡す
        //    uiManager.SetBestTime(stage1BestTime);
        //}
        //// ステージ２をプレイ中なら
        //if (SceneManager.GetActiveScene().name == "Stage2")
        //{
        //    // UIManegerにbestTimeを渡す
        //    uiManager.SetBestTime(stage2BestTime);
        //}

        //// TurorialUIパネル非表示
        //uiManager.tutorialUi.SetActive(false);
        //// GameUIパネル表示
        //uiManager.gameUi.SetActive(true);
        //// Menuパネル非表示
        ////uiManager.menu.SetActive(false);
        //// Clearパネル非表示
        //uiManager.clear.SetActive(false);

        // FPSを60に設定
        Application.targetFrameRate = 60;
    }

    //void Update()
    //{
    //    #region Player関連関数
    //    // プレイヤーに重力を与える
    //    player.SetPlayerGravity();
    //    // プレイヤーの移動処理
    //    player.MovePlayer();

    //    // 壁ジャンプは可能か
    //    if (!player.GetCanWallJump)
    //    {
    //        // プレイヤーのジャンプ処理
    //        player.JumpPlayer();
    //    }
    //    else
    //    {
    //        // プレイヤーの壁ジャンプ処理
    //        player.WallJumpPlayer();
    //    }
       
    //    // プレイヤーが死んだら
    //    if (player.GetSetIsPlayerDead)
    //    {
    //        // プレイヤー死亡処理
    //        StartCoroutine(player.DeadPlayer());

    //        // リスポーン位置にたどり着いていなければ
    //        if (transitTime == 0.0f)
    //        {
    //            // ゲーム状態をNONEへ
    //            gameState = GameState.NONE;
    //        }

    //        // nowTimeリスポーン位置通過時の時間に戻す
    //        nowTime = transitTime;
    //    }
    //    #endregion

    //    #region カメラ関連関数
    //    // スティックの位置を現在のアングルにする
    //    camera.NowAngle();
    //    // カメラの視野角変更
    //    camera.ChangeView();
    //    // プレイヤーに合わせたカメラの移動
    //    camera.MoveCamera();
    //    // カメラの回転処理
    //    camera.RotateCamera();
    //    // カメラのめり込み防止処理
    //    camera.WallBack();
    //    #endregion

    //    // ゲーム中の状態ごとの処理
    //    NOWGameState();
    //}

    // ゲーム中の状態ごとの処理
    private void NOWGameState()
    {
        switch (gameState)
        {
            case GameState.NONE:
                break;

            case GameState.GAME:
                // nowTimeのカウント開始
                nowTime += Time.deltaTime;
                // UIManegerにnowTimeを渡す
                //uiManager.SetNowTime(nowTime);
                break;

            case GameState.CLEAR:
                // クリア時のnowTimeをclearTimeにする
                clearTime = nowTime;
                // UIManagerにclearTimeを渡す
                //uiManager.SetClearTime(clearTime);

                // ステージ１をプレイ中なら
                if (SceneManager.GetActiveScene().name == "Stage1")
                {
                    // 初プレイまたはclearTimeがbestTimeより速ければ
                    if (stage1BestTime == 0.0f || clearTime < stage1BestTime)
                    {
                        // clearTimeをbestTimeにする
                        stage1BestTime = clearTime;
                    }
                    // UIManegerにbestTimeを渡す
                    //uiManager.SetBestTime(stage1BestTime);
                }
                // ステージ２をプレイ中なら
                if (SceneManager.GetActiveScene().name == "Stage2")
                {
                    // 初プレイまたはclearTimeがbestTimeより速ければ
                    if (stage2BestTime == 0.0f || clearTime < stage2BestTime)
                    {
                        // clearTimeをbestTimeにする
                        stage2BestTime = clearTime;
                    }
                    // UIManegerにbestTimeを渡す
                    //uiManager.SetBestTime(stage2BestTime);
                }

                // GameUIパネル非表示
                //uiManager.gameUi.SetActive(false);
                // Clearパネル表示
                //uiManager.clear.SetActive(true);

                // Clearボタン処理
                //if (!uiManager.OnlyOnce)
                //{
                //    // 最初に選択状態にしたいボタンの設定
                //    uiManager.clearFirstSelect.Select();

                //    // 処理した
                //    uiManager.OnlyOnce = true;
                //}
                break;
        }
    }

    /// <summary>
    /// 今のタイムのゲッター
    /// </summary>
    public float NowTime
    {
        get { return nowTime; }

        set { nowTime = value;  }
    }

    /// <summary>
    /// リスポーン位置通過時のタイムのセッター
    /// </summary>
    public float TransTime
    {
        get { return transitTime;  }

        set { transitTime = value; }
    }
}