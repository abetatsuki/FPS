using UnityEngine;

[CreateAssetMenu(fileName = "SceneSO", menuName = "SceneSO")]
public class SceneSO : ScriptableObject
{
    public string[] SceneNames => _sceneName;
    [SerializeField] private string[] _sceneName;
    
    
}