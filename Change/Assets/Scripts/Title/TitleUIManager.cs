using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    // SceneChanger変数
    private SceneChanger sceneChanger;

    // 最初に選択状態にしたいボタン
    [SerializeField] private Button firstSelectButton;

    void Start()
    {
        // 最初に選択状態にしたいボタンの設定
        firstSelectButton?.Select();

        // DontDestroyOnLoad化したオブジェクトにあるSceneCnagerを取得
        sceneChanger = DontDestroyObject.GetGameObject.GetComponent<SceneChanger>();
    }

    /// <summary>
    /// ボタンクリック処理
    /// </summary>
    public void OnClick(string menuType)
    {
        switch (menuType)
        {
            case "Tutorial":
                // 次のシーンをチュートリアルに変更
                sceneChanger.nextScene = SceneChanger.SceneName.Tutorial;
                break;

            case "MapSelect":
                // 次のシーンをマップセレクトに変更
                sceneChanger.nextScene = SceneChanger.SceneName.MapSelect;
                break;

            case "Quit":
                // ゲーム終了
                Quit();
                break;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
