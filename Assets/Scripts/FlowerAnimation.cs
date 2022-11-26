using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    Animator m_animator;
    int m_flowerHealth = 3;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_animator.SetInteger("Health", m_flowerHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            m_flowerHealth--;
            m_animator.SetInteger("Health", m_flowerHealth);

            if (m_flowerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
