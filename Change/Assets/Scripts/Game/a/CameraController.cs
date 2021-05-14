using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region 変数宣言
    // Camera変数
    [SerializeField] private Camera cam;

    // PlayerController変数
    [SerializeField] private PlayerController player;

    // カメラの回転スピード
    [SerializeField] private float rotateSpeed;
    // カメラの視野角
    [SerializeField] private float view;
    // 視野角の増減量
    [SerializeField] private float changeView;
    // 視野角の変更スピード
    [SerializeField] private float changeVel;
    // カメラの最小視野角
    [SerializeField] private float minView;
    // カメラの最大視野角
    [SerializeField] private float maxView;
    // プレイヤーとカメラの距離
    [SerializeField] private float distance;

    // ステージのレイヤーマスク(Unity側で8がStageCubeと設定してある)8のフラグを立てる
    private int stageLayer = 1 << 8;

    // 現在のアングル
    private Vector3 angle;
    // １フレーム前のプレイヤーの位置
    private Vector3 playerPos;

    // rayが壁に当たった時に入る変数
    private RaycastHit wallHit;
    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init()
    {
        // プレイヤーとカメラの距離
        distance = Vector3.Distance(player.transform.position, transform.position);
        // スタート時のプレイヤーのポジション
        playerPos = player.transform.position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Run()
    {
        // スティックの位置を現在のアングルにする
        NowAngle();
        // カメラの視野角変更
        ChangeView();
        // プレイヤーに合わせたカメラの移動
        MoveCamera();
        // カメラの回転処理
        RotateCamera();
        //カメラのめり込み防止処理
        WallBack();
    }

    /// <summary>
    /// スティックの位置を現在のアングルにする
    /// </summary>
    private void NowAngle()
    {
        // スティックの位置を現在のアングルに
        angle = new Vector3(Input.GetAxis("R_Stick_H") * rotateSpeed, Input.GetAxis("R_Stick_V") * rotateSpeed, 0);
    }

    /// <summary>
    /// カメラの視野角変更
    /// </summary>
    private void ChangeView()
    {
        // プレイヤーの速度が一定を超えたら
        if (player.GetPlayerRb.velocity.x > changeVel || player.GetPlayerRb.velocity.y > changeVel || player.GetPlayerRb.velocity.z > changeVel)
        {
            // 視野角を増やす
            view = cam.fieldOfView += changeView;
        }
        else
        {
            // 視野角を減らす
            view = cam.fieldOfView -= changeView;
        }

        // 視野角変更(視野角, 最小視野角, 最大視野角)
        cam.fieldOfView = Mathf.Clamp(value: view, min: minView, max: maxView);
    }

    /// <summary>
    /// プレイヤーに合わせたカメラの移動
    /// </summary>
    private void MoveCamera()
    {
        // プレイヤーの移動量分、カメラも移動する
        transform.position += player.transform.position - playerPos;
        // １フレーム前のプレイヤーのポジション
        playerPos = player.transform.position;
    }

    /// <summary>
    /// カメラの回転処理
    /// </summary>
    private void RotateCamera()
    {
        // 回転処理(中心にしたい軸, 回転させたい座標軸, 回転角度)
        transform.RotateAround(player.transform.position, Vector3.up, angle.x);
        transform.RotateAround(player.transform.position, transform.right, angle.y);
    }

    /// <summary>
    /// カメラのめり込み防止処理
    /// </summary>
    private void WallBack()
    {
        // カメラを元の距離まで戻す
        transform.position = player.transform.position - transform.rotation * Vector3.forward * distance;

        // Line処理(Lineの開始地点, Lineの終了地点, 当たった時のLinecastの情報, レイヤーマスク)
        if (Physics.Linecast(player.transform.position, transform.position, out wallHit, stageLayer))
        {
            // Rayが当たった場所にカメラを持ってくる
            transform.position = wallHit.point;
        }

        // Lineの可視化(Lineの開始地点, Lineの終了地点, Lineの色, Lineが写る時間)
        Debug.DrawLine(player.transform.position, transform.position, Color.red, 0f, false);
    }
}
