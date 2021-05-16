using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // �v���C���[�ϐ�
    [SerializeField] private PlayerController player;
    // �J�����ϐ�
    [SerializeField] private CameraController camController;
    // GoToMapSelect�ϐ�
    [SerializeField] private GoToMapSelect goToMapSelect;

    void Start()
    {
        // �v���C���[������
        player.Init();
        // �J����������
        camController.Init();
        // GoToMapSelect������
        goToMapSelect.Init();
    }

    void Update()
    {
        // �v���C���[�X�V
        player.Run();
        // �J�����X�V
        camController.Run();
    }
}
