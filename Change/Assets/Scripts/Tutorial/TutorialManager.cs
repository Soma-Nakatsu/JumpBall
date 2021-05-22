using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // プレイヤー変数
    [SerializeField] private PlayerController player;
    // カメラ変数
    [SerializeField] private CameraController camController;
    // GoToMapSelect変数
    [SerializeField] private GoToMapSelect goToMapSelect;

    private void Awake()
    {
        // チュートリアルUI呼び出し
        SceneManager.LoadSceneAsync("TutorialUI", LoadSceneMode.Additive);
    }

    private void Start()
    {
        // プレイヤー初期化
        player.Init();
        // カメラ初期化
        camController.Init();
        // GoToMapSelect初期化
        goToMapSelect.Init();
    }

    private void Update()
    {
        // プレイヤー更新
        player.Run();
        // カメラ更新
        camController.Run();
    }
}
