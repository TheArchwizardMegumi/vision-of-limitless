using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeUI : MonoBehaviour
{
    public Sprite[] openEye;
    public Sprite[] closeEye;
    public float eyeSpeed = 0.1f;

    private Image eyeImage;
    bool isClose = false;

    void Start()
    {
        eyeImage = GetComponent<Image>();
        StartCoroutine(EyeAnimation());
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, ChangeEye);

    }

    void ChangeEye(PlayerState state)
    {
        if (state == PlayerState.Close)
        {
            isClose = true;
        }
        else
        {
            isClose = false;
        }
    }

    IEnumerator EyeAnimation()
    {
        
        while (true)
        {
            if(isClose)
            {
                eyeImage.sprite = closeEye[0];
                yield return new WaitForSeconds(eyeSpeed);
            }
            else
            {
                eyeImage.sprite = openEye[0];
                yield return new WaitForSeconds(eyeSpeed);
            }
            if(isClose)
            {
                eyeImage.sprite = closeEye[1];
                yield return new WaitForSeconds(eyeSpeed);
            }
            else
            {
                eyeImage.sprite = openEye[1];
                yield return new WaitForSeconds(eyeSpeed);
            }
            if(isClose)
            {
                eyeImage.sprite = closeEye[2];
                yield return new WaitForSeconds(eyeSpeed);
            }
            else
            {
                eyeImage.sprite = openEye[2];
                yield return new WaitForSeconds(eyeSpeed);
            }
        }
    }
}
