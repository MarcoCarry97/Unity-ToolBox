using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : ToolBox.GUI.Widgets.Widget
{
    public MobController Mob { get; set; }

    private TextMeshProUGUI textview;
    
    private void Start()
    {
        textview = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textview.text= string.Empty;
    }

    private void Update()
    {
        if (Mob!=null)
        {
            Image image = this.GetComponent<Image>();
            image.color = (Mob is AlliedController) ? Color.blue : Color.red;
            int health = Mob.ModificableStats.health;
            int maxHealth = Mob.ModificableStats.maxHealth;
            textview.text = health + "/" + maxHealth;
        }
    }
}
