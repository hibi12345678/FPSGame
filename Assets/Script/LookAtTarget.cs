using UnityEngine;

public class LookAtTarget: MonoBehaviour
{
    public Camera mainCamera; // メインカメラの参照
    public float rotationSpeed = 5f; // 回転速度を調整するための変数

    void Update()
    {
        // カメラの画面中心を向く
        LookAtCameraCenter();

    }

    void LookAtCameraCenter()
    {
        if (mainCamera != null)
        {
            // カメラの中心をワールド座標で取得する
            Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 50f));
            
            // カメラの中心の方向を向く
            Vector3 lookDirection = cameraCenter;
            
            // キャラクターの向きを調整する
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}