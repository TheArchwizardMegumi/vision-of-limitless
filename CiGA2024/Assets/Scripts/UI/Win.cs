using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        GameManager.Instance.currentLevelIndex = 0;
    }
}
