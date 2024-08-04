using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // �v���C���[�L�����N�^�[
    public float mouseSensitivity = 200f; // �}�E�X�̊��x
    //public Vector2 pitchMinMax = new Vector2(-40, 85); // �J�����̏㉺��]�̐���

    private float yaw; // ���������̉�]
    private float pitch; // ���������̉�]

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �J�[�\�������b�N
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       
        pitch -= mouseY;

        // x���̉�]�p�x�� -90 ���� 90 �x�ɐ���
        pitch = Mathf.Clamp(pitch, -45f, 75f);

        // �J�����̃��[�J����]��ݒ�
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}