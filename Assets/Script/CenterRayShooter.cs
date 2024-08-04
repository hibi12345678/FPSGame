using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CenterRayShooter : MonoBehaviour
{
    public float maxDistance = 100f; // Ray�̍ő勗��
    public int damage = 10; // �^����_���[�W��

    public GameObject effectPrefab;  // �G�t�F�N�g�̃v���n�u
    public GameObject firePrefab;  // �G�t�F�N�g�̃v���n�u
    public GameObject hibanaEffect;  // �G�t�F�N�g�̃v���n�u
    public Transform camConTransform;
    public Transform rotationObjRotation;
    public float bulletSpeed = 20f;
    public GameObject firePoint; // �e�𔭎˂���ʒu
    Quaternion rotation;
    int bulletCount;
    private bool myBool,timeBool, continuBool;
    int errorCount = 0;
    float[] recoilX = { 0.0f, 0.1f, 0.1f, 0.3f, 1.0f, -0.3f, 0.2f, 0.4f, -0.4f, 0.2f, 1.0f, 1.0f, -0.3f, -0.2f, -0.1f, 0.4f, 0.2f, 0.0f, -0.3f, 0.0f, 1.0f, -1.0f,-0.1f, 0.0f, 0.3f, 0.0f};
    float[] recoilY = { 0.0f, 0.0f, 0.2f, 1.0f, 1.0f, 1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -0.3f, -0.4f, 0.0f, -0.0f, 0.5f, 0.1f, 0.2f, 0.0f, 0.0f, 0.1f, -0.2f, 0.1f, 0.1f, -0.2f, 0.0f, 0.3f};
    float[] errorX = { 0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.1f, -0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, -0.1f, 0.1f, 0.1f, 0.0f, 0.1f, 0.05f, 0.1f, 0.0f, 0.0f, 0.1f, 0.1f, -0.1f, 0.0f };
    float[] errorY = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.05f, 0.1f, -0.1f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f, 0.0f, 0.05f, -0.1f, 0.0f, 0.1f, 0.0f, 0.0f, 0.05f, -0.1f, 0.1f };
    private Coroutine currentCoroutine = null;  // ���ݎ��s���̃R���[�`����ǐ�    
    // UI Text�ւ̎Q��
    public Text uiText;
    public GameObject uiPrefab; // ��u�\��������UI��GameObject
    // �e�L�X�g�̐F��ύX���邽�߂̕ϐ�
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
        // �ϐ��̒l���e�L�X�g�ɕ\��
        uiText.text = bulletCount.ToString() + " / 25 ";
        // �����ɉ����ĐF��ύX�����
        if (bulletCount <= 10)
        {
            uiText.color = Color.red;
        }
        else
        {
            uiText.color = Color.black;
        }

        if (Input.GetMouseButton(0) && myBool == true && bulletCount != 0 && timeBool == true) // �}�E�X�̍��{�^�����������Ƃ�
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
        
        // ��ʂ̒��S����Ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2+errorX[errorCount], Screen.height / 2+errorY[errorCount]));
        RaycastHit hit;
        Vector3 targetPosition;
        transform.localRotation *= Quaternion.Euler(-recoilX[errorCount], -recoilY[errorCount], 0);
        StartCoroutine(DelayCoroutine());
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            targetPosition = hit.point;
            
            // �q�b�g�����I�u�W�F�N�g��Health�R���|�[�l���g������ꍇ�A�_���[�W��^����
            Health targetHealth = hit.collider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            // �v���n�u���w��̈ʒu�ɃC���X�^���X��
            GameObject hibanaInstance = Instantiate(hibanaEffect, targetPosition, rotation);

            // Particle System���Đ�
            ParticleSystem hibana = hibanaInstance.GetComponent<ParticleSystem>();
            if (hibana != null)
            {
                hibana.Play();

                // �G�t�F�N�g���I�������玩���ō폜�����悤�ɐݒ�
                Destroy(hibanaInstance, hibana.main.duration + hibana.main.startLifetime.constantMax);
            }


            // �q�b�g�����I�u�W�F�N�g��Transform���擾
            Transform hitTransform = hit.transform;

            // "Canvas"�Ƃ������O�̎q�I�u�W�F�N�g�ɃA�N�Z�X
            Transform canvas = hitTransform.Find("Canvas");
            if (canvas != null)
            {
                // �v���n�u�𐶐����A�L�����o�X�̎q�I�u�W�F�N�g�Ƃ��Đݒ�
                GameObject uiInstance = Instantiate(uiPrefab, canvas.transform);
                DamageDirection damageDirection = uiInstance.GetComponent<DamageDirection>();
                damageDirection.enemyPosition = transform.position;
            }
        }
        else
        {
            targetPosition = ray.origin + ray.direction * 100f; // �f�t�H���g�̃^�[�Q�b�g�ʒu��ݒ�
        }

        

        // �e���������������v�Z���A���x��ݒ肷��
        Vector3 direction = (targetPosition - firePoint.transform.position).normalized;

        // direction�x�N�g�������]�s����쐬
        rotation = Quaternion.LookRotation(direction);

        // �v���n�u���w��̈ʒu�ɃC���X�^���X��
        GameObject effectInstance = Instantiate(effectPrefab, firePoint.transform.position, rotation);
        
        GameObject fireInstance = Instantiate(firePrefab, firePoint.transform.position, rotation);
        // ���݂̃X�P�[�����擾
        Vector3 currentScale = effectInstance.transform.localScale;

        // x���̃X�P�[����ύX
        currentScale.z = (targetPosition - firePoint.transform.position).magnitude;

        // �ύX�����X�P�[�����I�u�W�F�N�g�ɓK�p
        effectInstance.transform.localScale = currentScale;
        // Particle System���Đ�
        ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
        
        ParticleSystem fire = fireInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            fire.Play();
            // �G�t�F�N�g���I�������玩���ō폜�����悤�ɐݒ�
            Destroy(effectInstance, ps.main.duration + ps.main.startLifetime.constantMax);
            // �G�t�F�N�g���I�������玩���ō폜�����悤�ɐݒ�
            Destroy(fireInstance, fire.main.duration + fire.main.startLifetime.constantMax);
        }
        else
        {
            // �G�t�F�N�g�̃I�u�W�F�N�g���̂��폜�����悤�ɐݒ�iParticle System���Ȃ��ꍇ�j
            Destroy(effectInstance, 0.1f);  // 5�b��ɍ폜
            Destroy(fireInstance, 0.1f);  // 5�b��ɍ폜
        }

    }

    IEnumerator Relord()
    {
        audioSources[1].Play();
        // 3.5�b�ԑ҂�
        timeBool = false;
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(2.5f);
        audioSources[2].Play();
        yield return new WaitForSeconds(1.0f);
        timeBool = true;
        bulletCount = 25;
        errorCount = 0;
    }

    // �R���[�`���{��
    IEnumerator DelayCoroutine()
    {
        myBool = false;
        // 0.2�b�ԑ҂�
        yield return new WaitForSeconds(0.2f);
        myBool = true;
    }

    // �R���[�`���{��
    IEnumerator recoilCoroutine()
    {
        continuBool = false;
        // 0.3�b�ԑ҂�
        yield return new WaitForSeconds(0.3f);
        errorCount = 0;
        transform.localEulerAngles = Vector3.zero;
        continuBool = true;
        currentCoroutine = null;  // �R���[�`���̏I��������
    }
}