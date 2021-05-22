using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // SceneChanger変数
    [SerializeField] private SceneChanger sceneChanger;

    // 各ステージの最速タイム
    [SerializeField] private List<float> fastestTime = new List<float>();

    private void Start()
    {
        // FPSを60に設定
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        // SceneChanger更新
        sceneChanger.Run();
    }

    /// <summary>
    /// 最速タイムのゲッター
    /// </summary>
    public float GetFastestTime( int stageNumber)
    {
        return fastestTime[stageNumber];
    }

    /// <summary>
    /// 最速タイムのセッター
    /// </summary>
    public void SetFastestTime(float fastestTime, int stageNumber)
    {
        this.fastestTime[stageNumber] = fastestTime;
    }
}
