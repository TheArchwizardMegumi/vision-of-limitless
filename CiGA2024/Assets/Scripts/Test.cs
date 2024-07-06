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
        yield return new WaitForSeconds(1f);
        Messenger.Broadcast(MsgType.changeOpenCloseEye, PlayerState.Open);
        yield return null;
    }
}
