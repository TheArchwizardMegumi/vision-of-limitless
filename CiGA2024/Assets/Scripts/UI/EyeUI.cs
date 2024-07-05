using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeUI : MonoBehaviour
{
    public Sprite openEye;
    public Sprite closeEye;

    private Image eyeImage;

    // Start is called before the first frame update
    void Start()
    {
        eyeImage = GetComponent<Image>();
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, ChangeEye);
    }

    void ChangeEye(PlayerState state)
    {
        if (state == PlayerState.Open)
        {
            // set the source image of the Image component to the openEye sprite
            
            eyeImage.sprite = openEye;
        }
        else
        {
            eyeImage.sprite = closeEye;
        }
    }
}
