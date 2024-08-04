using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMid : MonoBehaviour
{

    public GameObject bottom;  // ��]���擾����I�u�W�F�N�g
    public GameObject target;  // ��]��K�p����I�u�W�F�N�g

    void Update()
    {
        // bottom �̃��[�J���̃��[�e�[�V�������擾
        Quaternion localRotation = bottom.transform.localRotation;

        // ���[�J���̃��[�e�[�V�������I�C���[�p�Ŏ擾
        Vector3 localEulerAngles = localRotation.eulerAngles;

        // x���̉�]�p�x�� 0-360 �x���� -180-180 �x�ɕϊ�
        if (localEulerAngles.x > 180f)
        {
            localEulerAngles.x -= 360f;
        }
        // x���̉�]�p�x��1/2�ɂ���
        localEulerAngles.x = 0.4f * localEulerAngles.x;

        // �ύX�����I�C���[�p���N�H�[�^�j�I���ɖ߂�
        Quaternion adjustedRotation = Quaternion.Euler(localEulerAngles);

        // target �ɓK�p
        target.transform.localRotation = adjustedRotation;
    }
}
