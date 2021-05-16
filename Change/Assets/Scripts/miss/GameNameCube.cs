using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class GameNameCube : MonoBehaviour
{
    // LetterCubeHit()のポインタ
    private System.Action<GameNameCube> letterCubeHit = null;

    // どのLetterCubeと当たったか
    public int index { get; private set; }

    // 他のオブジェクトが通り抜けたら
    private void OnTriggerEnter(Collider other)
    {
        // ObjectのtagがPlayerなら
        if (other.gameObject.tag == "Player")
        {
            // letterCubeHit呼び出し
            letterCubeHit?.Invoke(this);
        }
    }

    // 添え字と関数のセッター
    public void SetAction(int index, System.Action<GameNameCube> letterCubeHit)
    {
        // セッターで呼んできた添え字を渡す
        this.index = index;
        // セッターで呼んできたLetterCubeHit()をletterCubeHitに格納する
        this.letterCubeHit = letterCubeHit;
    }
}
