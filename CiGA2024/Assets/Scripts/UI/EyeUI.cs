using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeUI : MonoBehaviour
{
    public Sprite[] openEye;
    public Sprite[] closeEye;
    public float eyeSpeed = 0.2f;

    private Image eyeImage;

    void Start()
    {
        eyeImage = GetComponent<Image>();
        StartCoroutine(OpenEyeAnimation());
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, ChangeEye);
    }

    void ChangeEye(PlayerState state)
    {
        if (state == PlayerState.Open)
        {
            StartCoroutine(OpenEyeAnimation());
        }
        else
        {
            StartCoroutine(CloseEyeAnimation());
        }
    }

    IEnumerator OpenEyeAnimation()
    {
        while (true)
        {
            eyeImage.sprite = openEye[0];
            yield return new WaitForSeconds(eyeSpeed);
            eyeImage.sprite = openEye[1];
            yield return new WaitForSeconds(eyeSpeed);
            eyeImage.sprite = openEye[2];
            yield return new WaitForSeconds(eyeSpeed);
        }
    }

    IEnumerator CloseEyeAnimation()
    {
        while (true)
        {
            eyeImage.sprite = closeEye[0];
            yield return new WaitForSeconds(eyeSpeed);
            eyeImage.sprite = closeEye[1];
            yield return new WaitForSeconds(eyeSpeed);
            eyeImage.sprite = closeEye[2];
            yield return new WaitForSeconds(eyeSpeed);
        }
    }
}
