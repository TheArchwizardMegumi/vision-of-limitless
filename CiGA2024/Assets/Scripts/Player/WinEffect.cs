using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffect : MonoBehaviour
{
    private Animator animator;
    public AudioSource se;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayEffect());
    }
    private IEnumerator PlayEffect()
    {
        se.Play();
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
