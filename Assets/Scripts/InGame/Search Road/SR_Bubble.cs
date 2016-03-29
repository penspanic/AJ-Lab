using UnityEngine;
using System.Collections;

public class SR_Bubble : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.5f, 1.5f);
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }
}