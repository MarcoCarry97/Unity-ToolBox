using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ToolBox.Control.Explorer2D;
using ToolBox.GUI.Utils;

namespace ToolBox.Core
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;
        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<GameController>();
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }

        public PanelController Gui { get; private set; }
        public InventoryController Inventory { get; private set; }
        public InputController Commands { get; private set; }
        public PartyController Party { get; private set; }
        public AudioController Audio { get; private set; }
        public BattleController Battle { get; private set; }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            Gui = this.GetComponent<PanelController>();
            Inventory = this.GetComponent<InventoryController>();
            Commands = this.GetComponent<InputController>();
            Party = this.GetComponent<PartyController>();
            Audio = this.GetComponent<AudioController>();
            Battle = this.GetComponent<BattleController>();
        }

        private void Awake()
        {
            Start();
        }


    }
}
