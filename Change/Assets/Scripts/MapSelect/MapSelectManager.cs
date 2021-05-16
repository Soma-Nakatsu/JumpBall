using UnityEngine;
using UnityEngine.UI;

public class MapSelectManager : MonoBehaviour
{
    // SceneChanger変数
    private SceneChanger sceneChanger;

    // 最初に選択状態にしたいボタン
    [SerializeField] private Button firstSelectButton;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        // 最初に選択状態にしたいボタンの設定
        firstSelectButton?.Select();

        // DontDestroyOnLoad化したオブジェクトにあるSceneCnagerを取得
        sceneChanger = DontDestroyObject.GetGameObject.GetComponent<SceneChanger>();
    }

    /// <summary>
    /// ボタンクリック処理
    /// </summary>
    public void OnClick(string sceneName)
    {
        switch (sceneName)
        {
            case "Stage1":
                // 次のシーンをステージ1に変更
                sceneChanger.nextScene = SceneChanger.SceneName.Stage1;
                break;

            case "Stage2":
                // 次のシーンをステージ2に変更
                sceneChanger.nextScene = SceneChanger.SceneName.Stage2;
                break;
        }
    }
}
