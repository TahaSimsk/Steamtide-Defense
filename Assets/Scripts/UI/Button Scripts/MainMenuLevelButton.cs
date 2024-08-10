using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int levelIndex;
    [SerializeField] GameObject[] gears;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ChangeLevel);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(ChangeLevel);
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in gears)
        {
            item.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in gears)
        {
            item.SetActive(false);
        }
    }
}
