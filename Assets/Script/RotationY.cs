using UnityEngine;

public class RotationY : MonoBehaviour
{

    private float yaw; // ���������̉�]
    void Start()
    {
        // �v���C���[�̉�]��ݒ�i������]�j
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        // Y�����̉�]���������o���Č��݂̃��[�J����]�ɓK�p����
        float mouseX = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;

        yaw += mouseX;

        // �v���C���[�̉�]��ݒ�i������]�j
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
    }
}
