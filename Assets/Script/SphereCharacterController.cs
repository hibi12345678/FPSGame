using Photon.Pun;
using UnityEngine;
using System.Collections;

public class SphereCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // ジャンプ力
    

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    public Transform groundCheck; // キャラクターの足元の位置を指定するTransform
    public Transform amatureleg;
    
    private bool isGrounded;
    Vector3 localMove;
    private Rigidbody rb; // Rigidbodyコンポーネント

    // 子オブジェクトを設定
    public Transform childTransform;

    public Animator animator;
    public Animator animatorleg;
    public Animator animatormid;
    float currentLegYPosition;

    private AudioSource[] audioSources;
    Transform planet;
    GameObject sphere;
    void Start()
    {        
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        audioSources = GetComponents<AudioSource>();
        audioSources[0].Play();
        audioSources[1].Play();
        sphere = GameObject.Find("Planet");
        planet = sphere.transform;
    }

    void Update()
    {
        // 子オブジェクトのローカル座標系での方向を取得
        Vector3 childForward = childTransform.forward;
        Vector3 childRight = childTransform.right;
        // 移動ベクトルを初期化
        Vector3 moveDirection = Vector3.zero;
        // Wキーで前進
        if (Input.GetKey(KeyCode.W))
        {
            audioSources[0].UnPause();
            audioSources[1].Pause();
            moveDirection += childForward;
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
            if (!animatorleg.GetBool("Walk"))
            {
                animatorleg.SetBool("Walk", true);
            }
            if (!animatormid.GetBool("Walk"))
            {
                animatormid.SetBool("Walk", true);
            }


            animator.SetBool("Run", false);
            animatorleg.SetBool("WalkRight", false);
            animatorleg.SetBool("WalkLeft", false);
            animatorleg.SetBool("WalkBack", false);
            animatorleg.SetBool("Run", false);
            animatormid.SetBool("WalkRight", false);
            animatormid.SetBool("WalkLeft", false);
            animatormid.SetBool("WalkBack", false);
            animatormid.SetBool("Run", false);
            moveSpeed = 2.5f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                audioSources[1].UnPause();
                audioSources[0].Pause();
                moveSpeed = 4f;

                if (!animator.GetBool("Run"))
                {
                    animator.SetBool("Run", true);
                }

                if (!animatorleg.GetBool("Run"))
                {
                    animatorleg.SetBool("Run", true);
                }
                if (!animatormid.GetBool("Run"))
                {
                    animatormid.SetBool("Run", true);
                }



            }
        }
        // Sキーで後退
        else if (Input.GetKey(KeyCode.S))
        {
            audioSources[0].UnPause();
            audioSources[1].Pause();
            moveDirection -= childForward;
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
            if (!animatorleg.GetBool("WalkBack"))
            {
                animatorleg.SetBool("WalkBack", true);
            }
            if (!animatormid.GetBool("WalkBack"))
            {
                animatormid.SetBool("WalkBack", true);
            }


            animator.SetBool("Run", false);
            animatorleg.SetBool("Walk", false);
            animatorleg.SetBool("WalkRight", false);
            animatorleg.SetBool("WalkLeft", false);

            animatorleg.SetBool("Run", false);
            animatormid.SetBool("Walk", false);
            animatormid.SetBool("WalkRight", false);
            animatormid.SetBool("WalkLeft", false);

            animatormid.SetBool("Run", false);
            moveSpeed = 2.5f;

        }
        // Aキーで左移動
        else if (Input.GetKey(KeyCode.A))
        {
            audioSources[0].UnPause();
            audioSources[1].Pause();
            moveDirection -= childRight;
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
            if (!animatorleg.GetBool("WalkLeft"))
            {
                animatorleg.SetBool("WalkLeft", true);
            }
            if (!animatormid.GetBool("WalkLeft"))
            {
                animatormid.SetBool("WalkLeft", true);
            }


            animator.SetBool("Run", false);
            animatorleg.SetBool("Walk", false);
            animatorleg.SetBool("WalkRight", false);

            animatorleg.SetBool("WalkBack", false);
            animatorleg.SetBool("Run", false);
            animatormid.SetBool("Walk", false);
            animatormid.SetBool("WalkRight", false);

            animatormid.SetBool("WalkBack", false);
            animatormid.SetBool("Run", false);
            moveSpeed = 2.5f;

        }
        // Dキーで右移動
        else if (Input.GetKey(KeyCode.D))
        {
            audioSources[0].UnPause();
            audioSources[1].Pause();
            moveDirection += childRight;
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
            }
            if (!animatorleg.GetBool("WalkRight"))
            {
                animatorleg.SetBool("WalkRight", true);
            }
            if (!animatormid.GetBool("WalkRight"))
            {
                animatormid.SetBool("WalkRight", true);
            }


            animator.SetBool("Run", false);
            animatorleg.SetBool("Walk", false);

            animatorleg.SetBool("WalkLeft", false);
            animatorleg.SetBool("WalkBack", false);
            animatorleg.SetBool("Run", false);
            animatormid.SetBool("Walk", false);

            animatormid.SetBool("WalkLeft", false);
            animatormid.SetBool("WalkBack", false);
            animatormid.SetBool("Run", false);
            moveSpeed = 2.5f;

        }



        else if (moveDirection == Vector3.zero)
        {
            audioSources[0].Pause();
            audioSources[1].Pause();
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animatorleg.SetBool("Walk", false);
            animatorleg.SetBool("WalkRight", false);
            animatorleg.SetBool("WalkLeft", false);
            animatorleg.SetBool("WalkBack", false);
            animatorleg.SetBool("Run", false);
            animatormid.SetBool("Walk", false);
            animatormid.SetBool("WalkRight", false);
            animatormid.SetBool("WalkLeft", false);
            animatormid.SetBool("WalkBack", false);
            animatormid.SetBool("Run", false);

        }
        // 移動ベクトルを正規化して、移動速度をかける
        moveDirection = moveDirection.normalized * moveSpeed * Time.deltaTime;

        // 親オブジェクトを移動させる
        transform.position += moveDirection;

        // グローバルな移動方向に変換
        moveDirection = localMove;

        // 惑星の中心に向かってキャラクターを向ける
        Vector3 gravityDirection = (transform.position - planet.position).normalized;

        // 重力方向を考慮して回転を調整
        transform.rotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;

        // ジャンプの入力をチェック
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(gravityDirection  * jumpForce, ForceMode.Impulse);
            //animator.SetTrigger("Jump");
            animatorleg.SetTrigger("Jump");
            animatormid.SetTrigger("Jump");
            StartCoroutine(ChangePos());
        }

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(isGrounded == false)
            {
                audioSources[2].Play();
            }
            isGrounded = true;
            
            
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            
            
        }
    }

    IEnumerator ChangePos()
    {
        currentLegYPosition = 0.6f;
        amatureleg.localPosition = new Vector3(0f, currentLegYPosition, 0f);
        yield return new WaitForSeconds(0.1579f);
        currentLegYPosition = 0.25f;
        amatureleg.localPosition = new Vector3(0f, currentLegYPosition, 0f);
        yield return new WaitForSeconds(0.24839f);
        currentLegYPosition = 0.6f;      
        amatureleg.localPosition = new Vector3(0f, currentLegYPosition, 0f);
    }
}
