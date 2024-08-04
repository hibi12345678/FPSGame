using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CenterRayShooter : MonoBehaviour
{
    public float maxDistance = 100f; // Rayの最大距離
    public int damage = 10; // 与えるダメージ量

    public GameObject effectPrefab;  // エフェクトのプレハブ
    public GameObject firePrefab;  // エフェクトのプレハブ
    public GameObject hibanaEffect;  // エフェクトのプレハブ
    public Transform camConTransform;
    public Transform rotationObjRotation;
    public float bulletSpeed = 20f;
    public GameObject firePoint; // 弾を発射する位置
    Quaternion rotation;
    int bulletCount;
    private bool myBool,timeBool, continuBool;
    int errorCount = 0;
    float[] recoilX = { 0.0f, 0.1f, 0.1f, 0.3f, 1.0f, -0.3f, 0.2f, 0.4f, -0.4f, 0.2f, 1.0f, 1.0f, -0.3f, -0.2f, -0.1f, 0.4f, 0.2f, 0.0f, -0.3f, 0.0f, 1.0f, -1.0f,-0.1f, 0.0f, 0.3f, 0.0f};
    float[] recoilY = { 0.0f, 0.0f, 0.2f, 1.0f, 1.0f, 1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -0.3f, -0.4f, 0.0f, -0.0f, 0.5f, 0.1f, 0.2f, 0.0f, 0.0f, 0.1f, -0.2f, 0.1f, 0.1f, -0.2f, 0.0f, 0.3f};
    float[] errorX = { 0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.1f, -0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, -0.1f, 0.1f, 0.1f, 0.0f, 0.1f, 0.05f, 0.1f, 0.0f, 0.0f, 0.1f, 0.1f, -0.1f, 0.0f };
    float[] errorY = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.05f, 0.1f, -0.1f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f, 0.0f, 0.05f, -0.1f, 0.0f, 0.1f, 0.0f, 0.0f, 0.05f, -0.1f, 0.1f };
    private Coroutine currentCoroutine = null;  // 現在実行中のコルーチンを追跡    
    // UI Textへの参照
    public Text uiText;
    public GameObject uiPrefab; // 一瞬表示したいUIのGameObject
    // テキストの色を変更するための変数
    private Color textColor = Color.black;
    public Animator animator;
    private AudioSource[] audioSources;

    void Start()
    {
        myBool = true;
        timeBool = true;
        continuBool = true;
        bulletCount = 25;
        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        // 変数の値をテキストに表示
        uiText.text = bulletCount.ToString() + " / 25 ";
        // 条件に応じて色を変更する例
        if (bulletCount <= 10)
        {
            uiText.color = Color.red;
        }
        else
        {
            uiText.color = Color.black;
        }

        if (Input.GetMouseButton(0) && myBool == true && bulletCount != 0 && timeBool == true) // マウスの左ボタンを押したとき
        {                
            errorCount++;
            bulletCount--;
            ShootRayFromCenter();
            animator.SetTrigger("Fire");
            audioSources[0].Play();
            if (continuBool == false)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(recoilCoroutine());


        }

        if (Input.GetMouseButton(0) && bulletCount == 0 && timeBool == true)
        {
            StartCoroutine(Relord());
            
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletCount != 25 && timeBool == true)
        {
            
            StartCoroutine(Relord());
        }

    }

    void ShootRayFromCenter()
    {
        
        // 画面の中心からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2+errorX[errorCount], Screen.height / 2+errorY[errorCount]));
        RaycastHit hit;
        Vector3 targetPosition;
        transform.localRotation *= Quaternion.Euler(-recoilX[errorCount], -recoilY[errorCount], 0);
        StartCoroutine(DelayCoroutine());
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            targetPosition = hit.point;
            
            // ヒットしたオブジェクトにHealthコンポーネントがある場合、ダメージを与える
            Health targetHealth = hit.collider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            // プレハブを指定の位置にインスタンス化
            GameObject hibanaInstance = Instantiate(hibanaEffect, targetPosition, rotation);

            // Particle Systemを再生
            ParticleSystem hibana = hibanaInstance.GetComponent<ParticleSystem>();
            if (hibana != null)
            {
                hibana.Play();

                // エフェクトが終了したら自動で削除されるように設定
                Destroy(hibanaInstance, hibana.main.duration + hibana.main.startLifetime.constantMax);
            }


            // ヒットしたオブジェクトのTransformを取得
            Transform hitTransform = hit.transform;

            // "Canvas"という名前の子オブジェクトにアクセス
            Transform canvas = hitTransform.Find("Canvas");
            if (canvas != null)
            {
                // プレハブを生成し、キャンバスの子オブジェクトとして設定
                GameObject uiInstance = Instantiate(uiPrefab, canvas.transform);
                DamageDirection damageDirection = uiInstance.GetComponent<DamageDirection>();
                damageDirection.enemyPosition = transform.position;
            }
        }
        else
        {
            targetPosition = ray.origin + ray.direction * 100f; // デフォルトのターゲット位置を設定
        }

        

        // 弾が向かう方向を計算し、速度を設定する
        Vector3 direction = (targetPosition - firePoint.transform.position).normalized;

        // directionベクトルから回転行列を作成
        rotation = Quaternion.LookRotation(direction);

        // プレハブを指定の位置にインスタンス化
        GameObject effectInstance = Instantiate(effectPrefab, firePoint.transform.position, rotation);
        
        GameObject fireInstance = Instantiate(firePrefab, firePoint.transform.position, rotation);
        // 現在のスケールを取得
        Vector3 currentScale = effectInstance.transform.localScale;

        // x軸のスケールを変更
        currentScale.z = (targetPosition - firePoint.transform.position).magnitude;

        // 変更したスケールをオブジェクトに適用
        effectInstance.transform.localScale = currentScale;
        // Particle Systemを再生
        ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
        
        ParticleSystem fire = fireInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            fire.Play();
            // エフェクトが終了したら自動で削除されるように設定
            Destroy(effectInstance, ps.main.duration + ps.main.startLifetime.constantMax);
            // エフェクトが終了したら自動で削除されるように設定
            Destroy(fireInstance, fire.main.duration + fire.main.startLifetime.constantMax);
        }
        else
        {
            // エフェクトのオブジェクト自体が削除されるように設定（Particle Systemがない場合）
            Destroy(effectInstance, 0.1f);  // 5秒後に削除
            Destroy(fireInstance, 0.1f);  // 5秒後に削除
        }

    }

    IEnumerator Relord()
    {
        audioSources[1].Play();
        // 3.5秒間待つ
        timeBool = false;
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(2.5f);
        audioSources[2].Play();
        yield return new WaitForSeconds(1.0f);
        timeBool = true;
        bulletCount = 25;
        errorCount = 0;
    }

    // コルーチン本体
    IEnumerator DelayCoroutine()
    {
        myBool = false;
        // 0.2秒間待つ
        yield return new WaitForSeconds(0.2f);
        myBool = true;
    }

    // コルーチン本体
    IEnumerator recoilCoroutine()
    {
        continuBool = false;
        // 0.3秒間待つ
        yield return new WaitForSeconds(0.3f);
        errorCount = 0;
        transform.localEulerAngles = Vector3.zero;
        continuBool = true;
        currentCoroutine = null;  // コルーチンの終了を示す
    }
}