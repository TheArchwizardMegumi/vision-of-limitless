using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIAnimation : MonoBehaviour
{
    public Sprite[] tutorialImages;
    private Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        while (true)
        {
            image.sprite = tutorialImages[0];
            yield return new WaitForSeconds(0.5f);
            image.sprite = tutorialImages[1];
            yield return new WaitForSeconds(0.5f);
            image.sprite = tutorialImages[2];
            yield return new WaitForSeconds(0.5f);
            image.sprite = tutorialImages[3];
            yield return new WaitForSeconds(0.5f);
        }
    }
}