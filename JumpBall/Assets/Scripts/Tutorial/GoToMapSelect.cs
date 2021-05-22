using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMapSelect : MonoBehaviour
{
    // SceneChanger�ϐ�
    private SceneChanger sceneChanger;

    /// <summary>
    /// ����������
    /// </summary>
    public void Init()
    {
        // DontDestroyOnLoad�������I�u�W�F�N�g�ɂ���SceneCnager���擾
        sceneChanger = DontDestroyObject.GetGameObject.GetComponent<SceneChanger>();
    }

    /// <summary>
    /// �I�u�W�F�N�g�ɓ���������
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // ���̃V�[�����}�b�v�Z���N�g�ɕύX
            sceneChanger.nextScene = SceneChanger.SceneName.MapSelect;
        }
    }
}
