using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNormalVector : MonoBehaviour
{
    //// プレイヤー変数
    //[SerializeField] private TutorialPlayerController player;

    //private void OnCollisionEnter(Collision other)
    //{
    //    // プレイヤーと当たったら 
    //    if (other.gameObject.tag == "Player")
    //    {
    //        // 最初に当たった場所の法線ベクトルを取得
    //        Vector3 normalVector = other.contacts[0].normal;
    //        // プレイヤーの速度を単位ベクトルにする
    //        Vector3 playerVelocity = other.rigidbody.velocity.normalized;
    //        // 反射ベクトル計算
    //        playerVelocity += new Vector3(-normalVector.x * 2.0f, -normalVector.y * 2.0f, -normalVector.z * 2.0f);

    //        player.SetReflectionVector(playerVelocity);
    //    }
    //}
}
