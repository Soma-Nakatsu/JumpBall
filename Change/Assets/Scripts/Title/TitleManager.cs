using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    private void Awake()
    {
        // TitleUI呼び出し
        SceneManager.LoadSceneAsync("TitleUI", LoadSceneMode.Additive);
    }
}
