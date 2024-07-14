using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGlobal : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadSceneAsync("Global", LoadSceneMode.Additive);
    }
}
