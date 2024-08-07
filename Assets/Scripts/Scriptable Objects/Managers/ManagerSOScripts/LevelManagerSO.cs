using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="LevelManager")]
public class LevelManagerSO : ScriptableObject
{
    [SerializeField] GameEvent1ParamSO onBaseDeath;
    [SerializeField] List<Button> levels;

    public void ChangeLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

}
