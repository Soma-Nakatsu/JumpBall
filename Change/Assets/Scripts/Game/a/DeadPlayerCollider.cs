using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerCollider : MonoBehaviour
{
    // PlayerController変数
    [SerializeField] private PlayerController player;

    /// <summary>
    /// もしオブジェクトをすり抜けたら
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Playerタグのオブジェクトなら
        if (other.gameObject.tag == "Player")
        {
            // プレイヤーは死んだ
            player.SetIsDead = true;
        }
    }
}
