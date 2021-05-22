using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 変数宣言
    // 足元の影
    [SerializeField] private GameObject footShadow;

    // CameraController変数
    [SerializeField] private CameraController camController;

    // プレイヤージャンプ音
    [SerializeField] private AudioClip jumpSound;
    // プレイヤー着地音
    [SerializeField] private AudioClip landingSound;
    // オーディオソース
    AudioSource audioSource;

    // プレイヤーのメッシュレンダラー
    [SerializeField] private MeshRenderer meshRenderer;
    // 初期のHDRカラーパネル
    [SerializeField] [ColorUsage(false, true)] private Color defaultColor;
    // 変更後のHDRカラーパネル
    [SerializeField] [ColorUsage(false, true)] private Color changeColor;

    // ポイントライト
    [SerializeField] private Light pointLight;

    // プレイヤーのスピード
    [SerializeField] private float speed;
    // プレイヤーの最大スピード
    [SerializeField] private float maxSpeed;
    // プレイヤーのデフォルトのジャンプ力
    [SerializeField] private float defaultJumpPower;
    // プレイヤーのジャンプ力
    [SerializeField] private float jumpPower;
    // プレイヤーの最大ジャンプ力
    [SerializeField] private float maxJumpPower;
    // 長押し時にプラスするジャンプ力
    [SerializeField] private float upJumpPower;
    // プレイヤー移動用
    private float x, z;

    // ステージのレイヤーマスク(Unity側で8がStageCubeと設定してある)8のフラグを立てる
    private int stageLayer = 1 << 8;

    // ジャンプ中か
    [SerializeField] private bool isJump;
    // 壁ジャンプが可能か
    [SerializeField] private bool canWallJump;
    // 死んだか
    [SerializeField] private bool isDead;
    // リスポーン地点が更新されたか
    [SerializeField] private bool isUpdateResPos;
    // プレイヤー入力無効フラグ
    [SerializeField] private bool moveInvalidFlag; 

    // プレイヤー復活位置
    [SerializeField] private Vector3 resPos;
    // 移動用ベクトル
    private Vector3 moveVec;
    // ジャンプ用ベクトル
    private Vector3 JumpVec;
    // 壁ジャンプ用ベクトル
    private Vector3 wallJumpVec;
    // カメラの向き
    private Vector3 cameraForward;

    // rayがマップ当たった時に入る変数
    private RaycastHit stageHit;

    // PlayerのRigidBody変数
    [SerializeField] private Rigidbody rb;
    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init()
    {
        // コンポーネント取得
        audioSource = GetComponent<AudioSource>();

        // 変更するマテリアルのパラメータを事前に知らせる。
        meshRenderer.material.EnableKeyword("_EMISSION");

        // 移動用ベクトルに代入
        moveVec = new Vector3(x, 0, z);
        // ジャンプ力初期化
        jumpPower = defaultJumpPower;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Run()
    {
        // プレイヤーの進行方向決定処理
        DirectionDecision();

        // 入力無効フラグがfalseなら
        if (!moveInvalidFlag)
        {
            // プレイヤーの移動処理
            MovePlayer();

            //プレイヤーのジャンプ処理
            JumpPlayer();
        }

        // プレイヤー影を計算
        DrawPlayerShadow();

        // プレイヤーが死んだら死亡処理を行う
        if (isDead) StartCoroutine(DeadPlayer());
    }

    #region OnCollision
    /// <summary>
    /// オブジェクトに当たった時
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        // Gorundタグのオブジェクトなら
        if (other.gameObject.CompareTag("Ground"))
        {
            // 着地した
            isJump = false;

            // 着地音再生
            audioSource.PlayOneShot(landingSound);
        }

        // Wallタグのオブジェクトかつジャンプ中なら
        if (other.gameObject.CompareTag("Wall") && isJump)
        {
            // 情報を保存
            wallJumpVec = new Vector3(x, jumpPower, z);

            // 壁ジャンプが可能
            canWallJump = true;
        }
    }

    /// <summary>
    /// オブジェクトから離れたら
    /// </summary>
    private void OnCollisionExit(Collision other)
    {
        // 壁ジャンプは出来ない
        canWallJump = false;
    }
    #endregion

    /// <summary>
    /// プレイヤーの進行方向決定処理
    /// </summary>
    private void DirectionDecision()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        cameraForward = Vector3.Scale(camController.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        moveVec = cameraForward * z + camController.transform.right * x;
    }

    /// <summary>
    ///  プレイヤーの移動処理
    /// </summary>
    private void MovePlayer()
    {
        // 横移動入力
        x = Input.GetAxis("L_Stick_H") * speed;
        // 左移動入力
        z = Input.GetAxis("L_Stick_V") * speed;

        // 速度制限
        if (rb.velocity.magnitude < maxSpeed)
        {
            // 力を加える
            rb.AddForce(moveVec, ForceMode.Force);
        }
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    private void JumpPlayer()
    {
        // ジャンプボタンが押されたら
        if (Input.GetKey("joystick button 0"))
        {
            // jumpPowerが最大でないときは
            if (jumpPower < maxJumpPower)
            {
                // ジャンプ力を増やす
                jumpPower += upJumpPower;

                // メッシュの色を変える
                meshRenderer.material.SetColor("_EmissionColor", changeColor);

                // ポイントライトの強度が9以下なら
                if (pointLight.intensity <= 9)
                {
                    // ポイントライトの強度を1増やす
                    pointLight.intensity += 1;
                }
            }
        }
        else
        {
            // メッシュの色を元に戻す
            meshRenderer.material.SetColor("_EmissionColor", defaultColor);
            // ポイントライトの強度を0にする
            pointLight.intensity = 0;
        }

        // 壁ジャンプが可能でないなら
        if (!canWallJump)
        {
            // 通常ジャンプ
            NormalJump();
        }
        else
        {
            // 壁ジャンプ
            WallJump();
        }
    }

    #region 各ジャンプ処理
    /// <summary>
    /// プレイヤーの通常ジャンプ処理
    /// </summary>
    private void NormalJump()
    {
        // ジャンプボタンが離されるかつジャンプ中じゃなければ
        if (Input.GetKeyUp("joystick button 0") && !isJump)
        {
            // ジャンプ用ベクトルに代入
            JumpVec = new Vector3(0, jumpPower, 0);
            // ジャンプ力を加える
            rb.AddForce(JumpVec, ForceMode.Impulse);
            // ジャンプ音再生
            audioSource.PlayOneShot(jumpSound);
            // ジャンプ中
            isJump = true;

            // ジャンプ力をデフォルトに戻す
            jumpPower = defaultJumpPower;
        }
    }

    /// <summary>
    /// プレイヤーの壁ジャンプ処理
    /// </summary>
    private void WallJump()
    {
        // ジャンプボタンが離されたら
        if (Input.GetKeyUp("joystick button 0"))
        {
            // 壁ジャンプ力を加える
            rb.AddForce(new Vector3(-wallJumpVec.x, wallJumpVec.y, -wallJumpVec.z), ForceMode.Impulse);
            // ジャンプ音再生
            audioSource.PlayOneShot(jumpSound);

            // ジャンプ力をデフォルトに戻す
            jumpPower = defaultJumpPower;
        }
    }
    #endregion

    /// <summary>
    /// // プレイヤー影を計算
    /// </summary>
    private void DrawPlayerShadow()
    {
        // 影を表示する場所を計算
        //Raycast処理(Rayの開始地点, Rayの向き, 当たった時のRaycastの情報, レイヤーマスク)
        if (Physics.Raycast(transform.position, Vector3.down, out stageHit, 10.0f, stageLayer))
        {
            // Rayが当たった場所に影を持ってくる(影のちらつきを防止するために0.0001f分座標を上げる)
            footShadow.transform.position = stageHit.point + new Vector3(0, 0.0001f, 0);
        }

        // Rayの可視化(rayの開始地点, Rayの向き, Rayの色, Rayが写る時間)
        Debug.DrawRay(transform.position, Vector3.down * 10, Color.red, 0f);
    }

    /// <summary>
    /// プレイヤーの死亡処理
    /// </summary>
    private IEnumerator DeadPlayer()
    {
        // プレイヤー固定
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // 操作無効
        moveInvalidFlag = true;

        // ジャンプ力を初期ジャンプ力に
        jumpPower = defaultJumpPower;

        // 0.5秒待つ
        yield return new WaitForSeconds(0.5f);

        // プレイヤーをリスポーン位置へ
        transform.position = resPos;

        // プレイヤーの固定解除
        rb.constraints = RigidbodyConstraints.None;

        // 操作有効
        moveInvalidFlag = false;

        // 生き返った
        isDead = false;
    }

    /// <summary>
    /// プレイヤーのRigidBodyのゲッター
    /// </summary>
    public Rigidbody GetPlayerRb { get { return rb; } }

    /// <summary>
    /// プレイヤーは死んだか？のセッター
    /// </summary>
    public bool GetSetIsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    /// <summary>
    /// プレイヤーのリスポーン地点が更新されたか？のゲッター、セッター
    /// </summary>
    public bool GetSetIsUpdateResPos
    {
        get { return isUpdateResPos; }
        set { isUpdateResPos = value; }
    }

    /// <summary>
    /// 移動入力フラグのセッター
    /// </summary>
    public bool GetSetMoveInvalidFlag
    {
        get { return moveInvalidFlag; }
        set { moveInvalidFlag = value; }
    }

    /// <summary>
    /// プレイヤーリスポーン地点のセッター
    /// </summary>
    public Vector3 SetPlayerRes { set { resPos = value; } }
}