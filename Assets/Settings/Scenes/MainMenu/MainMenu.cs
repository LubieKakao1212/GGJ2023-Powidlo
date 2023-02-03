using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Pararmeters")]
    [SerializeField] private string firstLevel;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject characterSelect;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void CharacterSelectOpen()
    {
        mainMenu.SetActive(false);
        characterSelect.SetActive(true);
    }
    public void CharacterOptionsClose()
    {
        characterSelect.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OptionsOpen()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void OptionsClose()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
