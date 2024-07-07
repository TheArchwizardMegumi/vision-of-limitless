using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetSceneByName("SelectLevel").isLoaded)
        {
            SceneManager.UnloadSceneAsync("SelectLevel");
        }
    }
}
