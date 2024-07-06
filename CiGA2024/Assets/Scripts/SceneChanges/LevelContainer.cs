using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelContainer", menuName = "LevelContainer")]
public class LevelContainer : ScriptableObject
{
    public SceneAsset[] levels;
}
