using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void LoadMenu()
    {
        GameManager.Instance.UnloadScene("Win");
        GameManager.Instance.currentLevelIndex = 0;
    }
}
