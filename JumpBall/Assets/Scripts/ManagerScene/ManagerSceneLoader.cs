using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ManagerSceneをロードするクラス
/// </summary>
public class ManagerSceneLoader
{
    // これを入れるとシーンがロードされる前(Awakeより前)に実行される
    // [RuntimeInitializeOnLoadMethod]はオブジェクトに配置しなくても呼び出される
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadManagerScene()
    {
        // ManagerSceneが読み込まれていなければ
        if (!SceneManager.GetSceneByName("ManagerScene").IsValid())
        {
            // ManagerSceneをロード
            SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
        }
    }
}