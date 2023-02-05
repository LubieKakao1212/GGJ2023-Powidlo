using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Pararmeters")]
    [SerializeField] private string firstLevel;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject mainMenuShade;
    [SerializeField] private GameObject characterSelect;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        mainMenuShade.SetActive(false);
        optionsMenu.SetActive(false);
        characterSelect.SetActive(false);
    }

    public void LoadFirstLevel()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 1);

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
        mainMenuShade.SetActive(true);
        optionsMenu.SetActive(true);
    }
    public void OptionsClose()
    {
        optionsMenu.SetActive(false);
        mainMenuShade.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
