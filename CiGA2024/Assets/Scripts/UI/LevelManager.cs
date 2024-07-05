using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    void Awake()
    {
        int discoveredLevels = PlayerPrefs.GetInt("DiscoveredLevels", 1);
        ShowExistingLevels(discoveredLevels);
    }

    void ShowExistingLevels(int discoveredLevels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i < PlayerPrefs.GetInt("DiscoveredLevels", 1))
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
    }

}
