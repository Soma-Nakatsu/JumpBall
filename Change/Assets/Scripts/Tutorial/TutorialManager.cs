using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // �v���C���[�ϐ�
    [SerializeField] private PlayerController player;
    // �J�����ϐ�
    [SerializeField] private CameraController camController;
    // GoToMapSelect�ϐ�
    [SerializeField] private GoToMapSelect goToMapSelect;

    private void Awake()
    {
        // �`���[�g���A��UI�Ăяo��
        SceneManager.LoadSceneAsync("TutorialUI", LoadSceneMode.Additive);
    }

    private void Start()
    {
        // �v���C���[������
        player.Init();
        // �J����������
        camController.Init();
        // GoToMapSelect������
        goToMapSelect.Init();
    }

    private void Update()
    {
        // �v���C���[�X�V
        player.Run();
        // �J�����X�V
        camController.Run();
    }
}
