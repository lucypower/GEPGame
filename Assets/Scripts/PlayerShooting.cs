using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerShooting : MonoBehaviour
{
    public GameObject m_Prefab;
    public Transform m_spawnPosition;
    public float m_maxShots = 20f;
    [SerializeField] private float m_currentShots;
    public TMP_Text m_textCounter;

    public AudioSource m_shootingAudio;
    public AudioSource m_refillingAudio;

    public ParticleSystem m_dropletPS;

    Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_currentShots = m_maxShots;
    }

    private void Update()
    {
        if (m_currentShots > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(m_Prefab, m_spawnPosition.position, m_spawnPosition.rotation);
                m_shootingAudio.Play();
                m_currentShots--;
            }
        }

        m_textCounter.text = " Bullets Left : " + m_currentShots.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Return) && collision.CompareTag("Bucket"))
        {
            if (m_currentShots != m_maxShots)
            {
                m_refillingAudio.Play();
                m_dropletPS.Play();
                m_animator.SetTrigger("Reload");
            }

            m_currentShots = m_maxShots;            
        }
    }
}
