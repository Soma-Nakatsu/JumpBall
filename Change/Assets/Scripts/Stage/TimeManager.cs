using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // GameManager�ϐ�
    private GameManager gameManager;

    // ���̃X�e�[�W�ԍ�
    [SerializeField] int nowStageNumber;

    // ���̃^�C��
    [SerializeField] private float nowTime = 0;
    // �ő��^�C��
    [SerializeField] private float fastestTime;
    // �`�F�b�N�|�C���g(���X�|�[���n�_)�o�߃^�C��
    [SerializeField] private float checkPointTime = 0;

    /// <summary>
    /// ����������
    /// </summary>
    public IEnumerator Init()
    {
        // DontDestroyOnLoad�������I�u�W�F�N�g�ɂ���GameManager���擾
        yield return gameManager = DontDestroyObject.GetGameObject.GetComponent<GameManager>();

        // �ő��^�C��������
        fastestTime = gameManager.GetFastestTime(nowStageNumber);
    }

    /// <summary>
    /// ���̃^�C���̃Q�b�^�[�A�Z�b�^�[
    /// </summary>
    public float GetSetNowTime
    {
        get { return nowTime; }
        set { nowTime = value; }
    }

    /// <summary>
    /// �`�F�b�N�|�C���g(���X�|�[���n�_)�o�߃^�C���̃Q�b�^�[�A�Z�b�^�[
    /// </summary>
    public float GetSetCheckPointTime
    {
        get { return checkPointTime; }
        set { checkPointTime = value; }
    }
}
