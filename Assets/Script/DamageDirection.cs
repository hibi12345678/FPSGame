using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageDirection : MonoBehaviour
{
    public Vector3 enemyPosition;
    // Start is called before the first frame update

    void Start()
    {
        // ���g��RectTransform���擾
        RectTransform rectTransform = GetComponent<RectTransform>();
        // �X�P�[����傫������
        rectTransform.localScale = new Vector3(3, 3, 0); // �Ⴆ�΁A2�{�̃T�C�Y�ɂ���ꍇ
                                                         



    }

    void Update()
    {
        Transform currentTransform = transform.parent.parent.parent.parent;
        Vector3 targetTransform = (enemyPosition - currentTransform.position).normalized;
        Destroy(gameObject, 4.0f);
        // targetDirection�������悤�ɉ�]��ݒ�
        float angle = currentTransform.rotation.eulerAngles.y;



        float azimuthAngle = Mathf.Atan2(targetTransform.z, targetTransform.x) * Mathf.Rad2Deg;
        

        Debug.Log(angle +" " + azimuthAngle );
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, azimuthAngle + angle -90));
        
    }
}
