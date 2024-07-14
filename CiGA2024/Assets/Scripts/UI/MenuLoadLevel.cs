using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadLevel : MonoBehaviour
{
    public void LoadLevel()
    {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        AsyncOperation level = SceneManager.LoadSceneAsync("Level0", LoadSceneMode.Additive);
        AsyncOperation player = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);

        while (!level.isDone || !player.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level0"));

        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
