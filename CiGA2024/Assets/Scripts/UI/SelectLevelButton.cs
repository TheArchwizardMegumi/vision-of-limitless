using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelButton : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        Debug.Log($"Loading {name}");
        GameManager.LoadLevel(index);
    }

    public void UnloadScene(string name)
    {
        Debug.Log($"Unloading {name}");
        GameManager.Instance.UnloadScene(name);
    }
}
