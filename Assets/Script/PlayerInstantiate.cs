using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanciate : MonoBehaviour
{
            // オブジェクトを生成する元となるPrefabへの参照を保持します。
    public GameObject prefabObj;

    void Start()
    {
        CreateObject();
    }

    void CreateObject()
    {
        // ゲームオブジェクトを生成します。
        
        GameObject obj = Instantiate(prefabObj, new Vector3(0,250,0), Quaternion.identity);
        obj.tag = "Team1";
        //GameObject obj = Instantiate(prefabObj, new Vector3(0, 250, 0), Quaternion.identity);
        //obj.tag = Team2;
    }

}
