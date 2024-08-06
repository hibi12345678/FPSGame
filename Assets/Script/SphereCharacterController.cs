using Photon.Pun;
using UnityEngine;
using System.Collections;

public class SphereCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // �W�����v��
    

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    public Transform groundCheck; // �L�����N�^�[�̑����̈ʒu���w�肷��Transform
    public Transform amatureleg;
    
    private bool isGrounded;
    Vector3 localMove;
    private Rigidbody rb; // Rigidbody�R���|�[�l���g

    // �q�I�u�W�F�N�g��ݒ�
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
        // Rigidbody�R���|�[�l���g���擾
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
        // �q�I�u�W�F�N�g�̃��[�J�����W�n�ł̕������擾
        Vector3 childForward = childTransform.forward;
        Vector3 childRight = childTransform.right;
        // �ړ��x�N�g����������
        Vector3 moveDirection = Vector3.zero;
        // W�L�[�őO�i
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
        // S�L�[�Ō��
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
        // A�L�[�ō��ړ�
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
        // D�L�[�ŉE�ړ�
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
        // �ړ��x�N�g���𐳋K�����āA�ړ����x��������
        moveDirection = moveDirection.normalized * moveSpeed * Time.deltaTime;

        // �e�I�u�W�F�N�g���ړ�������
        transform.position += moveDirection;

        // �O���[�o���Ȉړ������ɕϊ�
        moveDirection = localMove;

        // �f���̒��S�Ɍ������ăL�����N�^�[��������
        Vector3 gravityDirection = (transform.position - planet.position).normalized;

        // �d�͕������l�����ĉ�]�𒲐�
        transform.rotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;

        // �W�����v�̓��͂��`�F�b�N
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
