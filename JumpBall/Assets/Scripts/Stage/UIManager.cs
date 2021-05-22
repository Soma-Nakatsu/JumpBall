//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class UIManager : MonoBehaviour
//{
    //// enum
    //private enum MenuType
    //{
    //    MAP_SELECT,   // マップ選択
    //    EXIT                   //  終了
    //}

    //#region スクリプト取得
    //// GameManager変数
    //private GameManager gameManager;
    //#endregion

    //#region オブジェクト取得
    //// TutorialUiパネル取得
    //[SerializeField] public GameObject tutorialUi;
    //// GameUiパネル取得
    //[SerializeField] public GameObject gameUi;
    //// Clearパネル取得
    //[SerializeField] public GameObject clear;
    //#endregion

    //// clearの最初に選択するボタン
    //[SerializeField] public Button clearFirstSelect;
    //// 一度だけ実行
    //[SerializeField] public bool OnlyOnce;

    //#region Image取得
    //// チュートリアルのImage変数
    //[SerializeField] private Image turorialJumpGauge;
    //// ゲームのImage変数
    //[SerializeField] private Image gameJumpGauge;
    //#endregion

    //#region 変数宣言
    //// ステージ１のnowTime
    //private float stage1NowTime = 0;
    //// ステージ２のnowTime
    //private float stage2NowTime = 0;
    //// ステージ１のbestTIme
    //private static float stage1BestTime = 0;
    //// ステージ２のbestTIme
    //private static float stage2BestTime = 0;
    //// ステージ１のclearTIme
    //private float stage1ClearTime = 0;
    //// ステージ２のclearTIme
    //private float stage2ClearTime = 0;


    //// nowTime取得
    //[SerializeField] private TextMeshProUGUI nowTimeLabel;
    //// clearTime取得
    //[SerializeField] private TextMeshProUGUI clearTimeLabel;
    //// bestTime取得
    //[SerializeField] private List<TextMeshProUGUI> bestTimeLabels = new List<TextMeshProUGUI>();
    ////  Message取得
    //[SerializeField] private List<TextMeshProUGUI> messages = new List<TextMeshProUGUI>();
    //// メッセージ取得後のMaterial
    //[SerializeField] private Material changeColor;
    //// 取得メッセージリセット後のMaterial
    //[SerializeField] private Material resetColor;
    //#endregion

    //void Start()
    //{
    //    // ステージ１をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage1")
    //    {
    //        // Stage1にあるオブジェクトを取得する
    //        gameManager = SceneManager.GetSceneByName("Stage1").GetRootGameObjects()[0].GetComponent<GameManager>();
    //    }
    //}

    //// ボタンクリック処理
    //public void OnClick(int menuType)
    //{
    //    switch ((MenuType)menuType)
    //    {
    //        case MenuType.MAP_SELECT:
    //            // チュートリアルをプレイ中なら
    //            if (SceneManager.GetActiveScene().name == "Tutorial")
    //            {
    //                // Tutorialアンロード
    //                SceneManager.UnloadSceneAsync("Tutorial");
    //            }
    //            // ステージ１をプレイ中なら
    //            if (SceneManager.GetActiveScene().name == "Stage1")
    //            {
    //                // Stage1アンロード
    //                SceneManager.UnloadSceneAsync("Stage1");
    //            }
    //            // 一時停止解除
    //            Time.timeScale = 1f;

    //            // MapSelect読み込み
    //            SceneManager.LoadScene("MapSelect");
    //            break;

    //        case MenuType.EXIT:
    //            // チュートリアルをプレイ中なら
    //            if (SceneManager.GetActiveScene().name == "Tutorial")
    //            {
    //                // Tutorialアンロード
    //                SceneManager.UnloadSceneAsync("Tutorial");
    //            }
    //            // ステージ１をプレイ中なら
    //            if (SceneManager.GetActiveScene().name == "Stage1")
    //            {
    //                // Stage1アンロード
    //                SceneManager.UnloadSceneAsync("Stage1");
    //            }
    //            // 一時停止解除
    //            Time.timeScale = 1f;

    //            // MapSelect読み込み
    //            SceneManager.LoadScene("Title");
    //            break;
    //    }
    //}

    //#region Setter
    //// GameManagerからnowTimeを受け取る
    //public void SetNowTime(float nowTime)
    //{
    //    // ステージ１をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage1")
    //    {
    //        // nowTime格納
    //        stage1NowTime = nowTime;
    //        nowTimeLabel.text = " " + stage1NowTime.ToString("N2");
    //    }
    //    // ステージ２をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage2")
    //    {
    //        // nowTime格納
    //        stage2NowTime = nowTime;
    //        nowTimeLabel.text = " " + stage2NowTime.ToString("N2");
    //    }
    //}
    //// GameManagerからbestTimeを受け取る
    //public void SetBestTime(float bestTime)
    //{
    //    // ステージ１をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage1")
    //    {
    //        // bestTime格納
    //        stage1BestTime = bestTime;
    //        for (int i = 0; i < bestTimeLabels.Count; i++)
    //        {
    //            bestTimeLabels[i].text = " " + stage1BestTime.ToString("N2");
    //        }
    //    }
    //    // ステージ２をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage2")
    //    {
    //        // bestTime格納
    //        stage2BestTime = bestTime;
    //        for (int i = 0; i < bestTimeLabels.Count; i++)
    //        {
    //            bestTimeLabels[i].text = " " + stage2BestTime.ToString("N2");
    //        }
    //    }
    //}
    //// GameManagerからclearTImeを受け取る
    //public void SetClearTime(float clearTime)
    //{
    //    // ステージ１をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage1")
    //    {
    //        // nowTime格納
    //        stage1ClearTime = clearTime;
    //        clearTimeLabel.text = stage1ClearTime.ToString("N2");
    //    }
    //    // ステージ２をプレイ中なら
    //    if (SceneManager.GetActiveScene().name == "Stage2")
    //    {
    //        // nowTime格納
    //        stage2ClearTime = clearTime;
    //        clearTimeLabel.text = stage2ClearTime.ToString("N2");
    //    }
    //}

    //// 該当のメッセージのマテリアルを変更する
    //public void ChangeMaterial(int index)
    //{
    //    messages[index].color = changeColor.color;
    //}

    //// TurorialManagerからPlayerのジャンプ力と最大のジャンプ力を取得する
    //public void SetTutorialJumpPower(float firstJumpPower, float jumpPower, float maxJumpPower)
    //{
    //    // 最小値を_firstJumpPower, 最大値を_maxJumpPowerとしたときの_jumpPowerの割合を出し、fillAmountに渡す
    //    turorialJumpGauge.fillAmount = Mathf.InverseLerp(firstJumpPower, maxJumpPower, jumpPower);
    //}

    //// GameManagerからPlayerのジャンプ力と最大のジャンプ力を取得する
    //public void SetGameJumpPower(float firstJumpPower, float jumpPower, float maxJumpPower)
    //{
    //    // 最小値を_firstJumpPower, 最大値を_maxJumpPowerとしたときの_jumpPowerの割合を出し、fillAmountに渡す
    //    gameJumpGauge.fillAmount = Mathf.InverseLerp(firstJumpPower, maxJumpPower, jumpPower);
    //}
    //#endregion
//}
