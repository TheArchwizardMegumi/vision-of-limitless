using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
using UnderCloud;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TestCor());
    }
    private IEnumerator TestCor()
    {
        yield break;
    }
}
