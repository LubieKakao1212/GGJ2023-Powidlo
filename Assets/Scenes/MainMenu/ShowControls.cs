using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowControls : MonoBehaviour
{
    [SerializeField] GameObject controlsPanel;
    [SerializeField] KeyCode showControlsKey = KeyCode.T;
    [SerializeField] KeyCode backToMenuKey = KeyCode.Escape;
    [SerializeField] private string mainMenuScene;
    private bool areControlsVisible = false;

    private void Start()
    {
        controlsPanel.SetActive(areControlsVisible);
    }

    private void Update()
    {
        if(UnityEngine.Input.GetKeyDown(showControlsKey))
        {
            controlsPanel.SetActive(areControlsVisible);
            areControlsVisible =! areControlsVisible;
        }
        if(UnityEngine.Input.GetKeyDown(backToMenuKey))
        {
            SceneManager.LoadScene(mainMenuScene);
            Debug.Log("Back to Main");
        }
    }
}
