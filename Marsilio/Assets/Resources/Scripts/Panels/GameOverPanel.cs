using System.Collections;
using System.Collections.Generic;
using ToolBox.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    private Button continueButton;
    private Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        continueButton=this.transform.GetChild(1).GetComponent<Button>();
        menuButton = this.transform.GetChild(2).GetComponent<Button>();
        continueButton.onClick.AddListener(OnContinue);
        menuButton.onClick.AddListener(OnMenu);
    }

    private void OnContinue()
    {
        
    }

    private void OnMenu()
    {
        SceneManager.LoadScene("InitScene");
    }
}
