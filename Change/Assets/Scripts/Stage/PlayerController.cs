using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 変数宣言
    // StageManager変数
    [SerializeField] private StageManager stageManager;
    // CameraController変数
    [SerializeField] private CameraController camController;

    // プレイヤージャンプ音
    // [SerializeField] private AudioClip jumpSound;
    // プレイヤー着地音
    // [SerializeField] private AudioClip landingSound;
    // オーディオソース
    // AudioSource audioSource;

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

    // ジャンプ中か
    [SerializeField] private bool isJump;
    // 壁ジャンプが可能か
    [SerializeField] private bool canWallJump;
    // 死んだか
    [SerializeField] private bool isDead;
    // リスポーン地点が更新されたか
    [SerializeField] private bool isUpdateResPos;
    // プレイヤー移動無効フラグ
    [SerializeField] private bool moveInvalidFlag; 

    // プレイヤーに与える重力
    [SerializeField] private Vector3 gravity;
    // プレイヤー復活位置
    [SerializeField] private Vector3 resPos;
    // 移動用ベクトル
    private Vector3 moveVec;
    // 壁ジャンプ用ベクトル
    private Vector3 wallJumpVec;
    // カメラの向き
    private Vector3 cameraForward;

    // PlayerのRigidBody変数
    [SerializeField] private Rigidbody rb;
    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init()
    {
        // コンポーネント取得
        // audioSource = GetComponent<AudioSource>();

        // RigidBodyの重力を使わないようにする
        rb.useGravity = false;

        // 移動用ベクトルに代入
        moveVec = new Vector3(x, 0, z);
        // 壁ジャンプ用ベクトルに代入
        wallJumpVec = new Vector3(x, jumpPower, z);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Run()
    {
        // プレイヤーに重力を与える
        SetPlayerGravity();

        // プレイヤーの移動処理
        MovePlayer();

        //プレイヤーのジャンプ処理
        JumpPlayer();

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
            //audioSource.PlayOneShot(landingSound);
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
    /// プレイヤーに重力を与える
    /// </summary>
    private void SetPlayerGravity()
    {
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    /// <summary>
    ///  プレイヤーの移動処理
    /// </summary>
    private void MovePlayer()
    {
        #region プレイヤーの進行方向決定処理
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        cameraForward = Vector3.Scale(camController.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        moveVec = cameraForward * z + camController.transform.right * x;

        // キャラクターの向を進行方向に
        if (moveVec != Vector3.zero)
        {
            // プレイヤーを回転させる
            transform.rotation = Quaternion.LookRotation(moveVec);
        }
        #endregion

        // 移動無効フラグがfalseなら
        if (!moveInvalidFlag)
        {
            // 横移動入力
            x = Input.GetAxis("L_Stick_H") * speed;
            // 左移動入力
            z = Input.GetAxis("L_Stick_V") * speed;
        }

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
            }
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
            // ジャンプ力を加える
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            // ジャンプ音再生
            // audioSource.PlayOneShot(jumpSound);
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
            // audioSource.PlayOneShot(jumpSound);

            // ジャンプ力をデフォルトに戻す
            jumpPower = defaultJumpPower;
        }
    }
    #endregion

    /// <summary>
    /// プレイヤーの死亡処理
    /// </summary>
    private IEnumerator DeadPlayer()
    {
        // プレイヤー固定
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // ジャンプ力を初期ジャンプ力に
        jumpPower = defaultJumpPower;

        // 0.5秒待つ
        yield return new WaitForSeconds(0.5f);

        // プレイヤーをリスポーン位置へ
        transform.position = resPos;

        // プレイヤーの固定解除
        rb.constraints = RigidbodyConstraints.None;

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
    public bool SetMoveInvalidFlag { set { moveInvalidFlag = value; } }

    /// <summary>
    /// プレイヤーリスポーン地点のセッター
    /// </summary>
    public Vector3 SetPlayerRes { set { resPos = value; } }
}