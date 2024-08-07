using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName ="LevelManager")]
public class LevelManagerSO : ScriptableObject
{
    public SceneAsset MainMenuScene;
    [SerializeField] GameEvent1ParamSO onBaseDeath;


    public void ChangeLevel(SceneAsset scene)
    {
        string scenePath = AssetDatabase.GetAssetPath(scene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SceneManager.LoadScene(sceneName);
    }

}
