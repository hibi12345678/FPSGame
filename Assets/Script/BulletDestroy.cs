using UnityEngine;
using System.Collections.Generic;

public class BulletDestroy : MonoBehaviour
{

    public GameObject decalPrefab; // �f�J�[���̃v���n�u


    void OnCollisionEnter(Collision collision)
    {

        // �e���폜����
        Destroy(gameObject);
    }
}