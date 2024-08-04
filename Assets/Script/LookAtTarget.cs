using UnityEngine;

public class LookAtTarget: MonoBehaviour
{
    public Camera mainCamera; // ���C���J�����̎Q��
    public float rotationSpeed = 5f; // ��]���x�𒲐����邽�߂̕ϐ�

    void Update()
    {
        // �J�����̉�ʒ��S������
        LookAtCameraCenter();

    }

    void LookAtCameraCenter()
    {
        if (mainCamera != null)
        {
            // �J�����̒��S�����[���h���W�Ŏ擾����
            Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 50f));
            
            // �J�����̒��S�̕���������
            Vector3 lookDirection = cameraCenter;
            
            // �L�����N�^�[�̌����𒲐�����
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}