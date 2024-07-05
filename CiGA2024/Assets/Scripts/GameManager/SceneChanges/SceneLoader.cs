using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneAdditivelyByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void UnloadSceneByIndex(int sceneIndex)
    {
        string sceneName = SceneManager.GetSceneAt(sceneIndex).name;
        SceneManager.UnloadSceneAsync(sceneName);
    }
    
    public void ReplaceCurrentScene(string newSceneName)
    {
        SceneManager.LoadScene(newSceneName, LoadSceneMode.Additive);
        
        Scene currentScene = SceneManager.GetActiveScene();
        
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
