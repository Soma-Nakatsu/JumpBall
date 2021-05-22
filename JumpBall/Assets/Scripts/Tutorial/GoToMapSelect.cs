using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMapSelect : MonoBehaviour
{
    // SceneChanger変数
    private SceneChanger sceneChanger;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init()
    {
        // DontDestroyOnLoad化したオブジェクトにあるSceneCnagerを取得
        sceneChanger = DontDestroyObject.GetGameObject.GetComponent<SceneChanger>();
    }

    /// <summary>
    /// オブジェクトに当たった時
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // 次のシーンをマップセレクトに変更
            sceneChanger.nextScene = SceneChanger.SceneName.MapSelect;
        }
    }
}
