using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // プレイヤーキャラクター
    public float mouseSensitivity = 200f; // マウスの感度
    //public Vector2 pitchMinMax = new Vector2(-40, 85); // カメラの上下回転の制限

    private float yaw; // 水平方向の回転
    private float pitch; // 垂直方向の回転

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       
        pitch -= mouseY;

        // x軸の回転角度を -90 から 90 度に制限
        pitch = Mathf.Clamp(pitch, -45f, 75f);

        // カメラのローカル回転を設定
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}