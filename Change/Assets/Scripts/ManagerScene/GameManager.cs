using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // SceneChanger変数
    [SerializeField] private SceneChanger sceneChanger;

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
}
