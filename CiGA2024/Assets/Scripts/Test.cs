using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
using UnderCloud;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TestCor());
    }
    private IEnumerator TestCor()
    {
        MapManager.LoadMapOfCurrentLevel();
        yield return null;
        Debug.Log($"Access at 0,0 , Open: {MapManager.IsAccessible(new Vector2Int(0, 0), PlayerState.Open)}");
        Debug.Log($"Access at -1,0 , Open: {MapManager.IsAccessible(new Vector2Int(-1, 0), PlayerState.Open)}");
    }
}
