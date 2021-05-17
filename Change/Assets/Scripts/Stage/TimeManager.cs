using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // GameManager�ϐ�
    private GameManager gameManager;

    // ���̃X�e�[�W�ԍ�
    [SerializeField] private int nowStageNumber;

    // ���̃^�C��
    [SerializeField] private float nowTime = 0;
    // �ő��^�C��
    [SerializeField] private float fastestTime;
    // ���X�|�[���n�_�o�߃^�C��
    [SerializeField] private float passingTime = 0;

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
    /// ���X�|�[���n�_�ʉ߃^�C���̃Q�b�^�[�A�Z�b�^�[
    /// </summary>
    public float GetSetPassingTime
    {
        get { return passingTime; }
        set { passingTime = value; }
    }
}
