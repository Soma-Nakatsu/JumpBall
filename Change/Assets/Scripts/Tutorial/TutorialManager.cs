using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // プレイヤー変数
    [SerializeField] private PlayerController player;
    // カメラ変数
    [SerializeField] private CameraController camController;
    // GoToMapSelect変数
    [SerializeField] private GoToMapSelect goToMapSelect;

    void Start()
    {
        // プレイヤー初期化
        player.Init();
        // カメラ初期化
        camController.Init();
        // GoToMapSelect初期化
        goToMapSelect.Init();
    }

    void Update()
    {
        // プレイヤー更新
        player.Run();
        // カメラ更新
        camController.Run();
    }
}
