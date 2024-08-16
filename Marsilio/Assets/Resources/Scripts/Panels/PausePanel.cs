using System.Collections;
using System.Collections.Generic;
using ToolBox.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ToolBox.Core;
using ToolBox.Control.Explorer2D;

public class PausePanel : Panel
{
    private Button continueButton;
    private Button menuButton;

    // Start is called before the first frame update
    void Start()
    {
        continueButton=this.transform.GetChild(1).GetComponent<Button>();
        menuButton=this.transform.GetChild(2).GetComponent<Button>();
        continueButton.onClick.AddListener(OnContinue);
        menuButton.onClick.AddListener(OnMenu);
        continueButton.Select();
    }

    private void OnContinue()
    {
        GameController.Instance.Commands.SetState(InputController.State.CharacterControl);
        GameController.Instance.Gui.DeactivePanel();

    }

    private void OnMenu()
    {
        SceneManager.LoadScene("InitScene");
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
