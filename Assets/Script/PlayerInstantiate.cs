using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanciate : MonoBehaviour
{
            // �I�u�W�F�N�g�𐶐����錳�ƂȂ�Prefab�ւ̎Q�Ƃ�ێ����܂��B
    public GameObject prefabObj;

    void Start()
    {
        CreateObject();
    }

    void CreateObject()
    {
        // �Q�[���I�u�W�F�N�g�𐶐����܂��B
        
        GameObject obj = Instantiate(prefabObj, new Vector3(0,250,0), Quaternion.identity);
        obj.tag = "Team1";
        //GameObject obj = Instantiate(prefabObj, new Vector3(0, 250, 0), Quaternion.identity);
        //obj.tag = Team2;
    }

}
