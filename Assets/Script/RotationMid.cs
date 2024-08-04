using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMid : MonoBehaviour
{

    public GameObject bottom;  // 回転を取得するオブジェクト
    public GameObject target;  // 回転を適用するオブジェクト

    void Update()
    {
        // bottom のローカルのローテーションを取得
        Quaternion localRotation = bottom.transform.localRotation;

        // ローカルのローテーションをオイラー角で取得
        Vector3 localEulerAngles = localRotation.eulerAngles;

        // x軸の回転角度を 0-360 度から -180-180 度に変換
        if (localEulerAngles.x > 180f)
        {
            localEulerAngles.x -= 360f;
        }
        // x軸の回転角度を1/2にする
        localEulerAngles.x = 0.4f * localEulerAngles.x;

        // 変更したオイラー角をクォータニオンに戻す
        Quaternion adjustedRotation = Quaternion.Euler(localEulerAngles);

        // target に適用
        target.transform.localRotation = adjustedRotation;
    }
}
