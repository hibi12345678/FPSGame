using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        audioSource.Play();
        Debug.Log(gameObject.name + " died.");
        // �I�u�W�F�N�g��j�󂷂�Ȃǂ̏���
        Destroy(gameObject, 1.0f); ;
    }






}

