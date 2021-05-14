using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // enum宣言
    public enum SceneName
    {
        NONE,           // なし
        Title,             // タイトル
        MapSelect,    // マップセレクト
        Tutorial,       // チュートリアル
        Stage1,        // ステージ1
        Stage2,        // ステージ2
    }
    // 今のシーン
    [SerializeField] private SceneName nowScene = SceneName.NONE;
    // 次のフレームのシーン
    [SerializeField] public SceneName nextScene = SceneName.NONE;

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Run()
    {
        // シーン変更が必要なら
        if (IsChangeScene())
        {
            // シーン変更処理
            ChangeScene();

            // 今のシーンを次のフレームのシーンへ
            nowScene = nextScene;
        }
    }

    /// <summary>
    /// シーンが切り替えが必要か
    /// </summary>
    private bool IsChangeScene()
    {
        // 前のシーンと今のシーンが違えば
        if (nowScene != nextScene)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// シーン変更処理
    /// </summary>
    private void ChangeScene()
    {
        switch (nextScene)
        {
            case SceneName.Title:
                // シーン変更
                SceneManager.LoadScene("Title");
                break;

            case SceneName.MapSelect:
                // シーン変更
                SceneManager.LoadScene("MapSelect");
                break;

            case SceneName.Tutorial:
                // シーン変更
                SceneManager.LoadScene("Tutorial");
                break;

            case SceneName.Stage1:
                // シーン変更
                SceneManager.LoadScene("Stage1");
                break;

            case SceneName.Stage2:
                // シーン変更
                SceneManager.LoadScene("Stage2");
                break;
        }
    }
}
