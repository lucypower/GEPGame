using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_animator.SetBool("WalkLeft", true);
        }
        else
        {
            m_animator.SetBool("WalkLeft", false);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            m_animator.SetBool("WalkRight", true);
        }
        else
        {
            m_animator.SetBool("WalkRight", false);
        }
    }
}
