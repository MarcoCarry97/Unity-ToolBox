using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.GUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ToolBox.Core;
using ToolBox.Control;
using ToolBox.Control.Explorer2D;

public class StartPanel : Panel
{
    private Button startButton;
    private Button quitButton;

    private void Start()
    {
        startButton=this.transform.GetChild(1).GetComponent<Button>();
        quitButton=this.transform.GetChild(2).GetComponent<Button>();
        startButton.onClick.AddListener(OnStart);
        quitButton.onClick.AddListener(OnQuit);
        startButton.Select();
    }

    public void OnStart()
    {
        SceneManager.LoadScene("ExampleScene2D");
        GameController.Instance.Gui.DeactivePanel();
        GameController.Instance.Commands.SetState(InputController.State.CharacterControl);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
