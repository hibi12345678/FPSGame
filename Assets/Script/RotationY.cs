using UnityEngine;

public class RotationY : MonoBehaviour
{

    private float yaw; // 水平方向の回転
    void Start()
    {
        // プレイヤーの回転を設定（水平回転）
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        // Y軸回りの回転だけを取り出して現在のローカル回転に適用する
        float mouseX = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;

        yaw += mouseX;

        // プレイヤーの回転を設定（水平回転）
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
    }
}
