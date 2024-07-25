using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffect : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayEffect()
    {
        StartCoroutine(PE());
    }

    private IEnumerator PE()
    {
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
