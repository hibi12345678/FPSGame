using UnityEngine;
using System.Collections.Generic;

public class BulletDestroy : MonoBehaviour
{

    public GameObject decalPrefab; // デカールのプレハブ


    void OnCollisionEnter(Collision collision)
    {

        // 弾を削除する
        Destroy(gameObject);
    }
}