using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // GameManager変数
    private GameManager gameManager;

    // 今のステージ番号
    [SerializeField] private int nowStageNumber;

    // 今のタイム
    [SerializeField] private float nowTime = 0;
    // 最速タイム
    [SerializeField] private float fastestTime;
    // リスポーン地点経過タイム
    [SerializeField] private float passingTime = 0;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public IEnumerator Init()
    {
        // DontDestroyOnLoad化したオブジェクトにあるGameManagerを取得
        yield return gameManager = DontDestroyObject.GetGameObject.GetComponent<GameManager>();

        // 最速タイム初期化
        fastestTime = gameManager.GetFastestTime(nowStageNumber);
    }

    /// <summary>
    /// 今のタイムのゲッター、セッター
    /// </summary>
    public float GetSetNowTime
    {
        get { return nowTime; }
        set { nowTime = value; }
    }

    /// <summary>
    /// リスポーン地点通過タイムのゲッター、セッター
    /// </summary>
    public float GetSetPassingTime
    {
        get { return passingTime; }
        set { passingTime = value; }
    }
}
