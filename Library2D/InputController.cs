using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox.Control.Explorer2D;
using ToolBox.GUI;
using ToolBox.Interaction;
using UnityEngine;
using ToolBox.Core;
using ToolBox.GUI.Utils;
using ToolBox.Animations;

namespace ToolBox.Control.Explorer2D
{
    public class InputController : MonoBehaviour
    {
        public enum State
        {
            CharacterControl,
            PauseControl,
            DialogControl,
            VictoryControl
        }

        private State state;

        private Vector3 direction;
        public Vector3 Direction { get; private set; }

        private bool isButtonControlled;

        private void Awake()
        {
            Start();
        }

        // Start is called before the first frame update
        void Start()
        {
            state = State.DialogControl;
            isButtonControlled = false;
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControlState();
                    break;
                case State.DialogControl:
                    DialogControlState();
                    break;
                case State.PauseControl:
                    PauseControlState();
                    break;
                case State.VictoryControl:
                    VictoryControlState();
                    break;
            }
        }

        private void CharacterControlState()
        {
            if (Input.GetButtonDown("Pause"))
            {
                GameController.Instance.Commands.SetState(State.PauseControl);
                GameController.Instance.Gui.ActivePanel("PausePanel");
            }
            direction = Vector3.zero;
            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            direction.x = horiz;
            direction.y = vert;
            direction = direction.normalized;
            GameObject game = GameObject.FindGameObjectWithTag("MainCharacter");
            if (game != null)
            {
                CharacterController2D control = game.GetComponent<CharacterController2D>();
                control.Move(direction);
                game.GetComponent<AnimatorController>().SetDirection(direction);
                Direction = direction;
            }
        }

        public void DialogControlState()
        {
            isButtonControlled = Input.anyKey && !Input.GetMouseButtonDown(0);
            if (Input.anyKey && !isButtonControlled)
            {
                GameController.Instance.Gui.UseButtons();
            }
            BattleSystem battle = GameObject.FindObjectOfType<BattleSystem>();
            if (battle != null)
                BattleControlState(battle);
        }

        public void PauseControlState()
        {
            if (Input.GetButtonDown("Pause"))
            {
                GameController.Instance.Gui.DeactivePanel();
                state = State.CharacterControl;
            }

        }

        public void BattleControlState(BattleSystem battle)
        {
            if (battle.currentMob is AlliedController && Input.GetButtonDown("Cancel"))
            {
                PanelController panelC = GameController.Instance.Gui;
                BattleController battleC = GameController.Instance.Battle;
                Panel activePanel = panelC.GetActivePanel();
                if (activePanel is ChooseMovePanel)
                {
                    battleC.ChosenMove = null;
                    panelC.DeactivePanel();
                    panelC.ActivePanel("AlliedTurnPanel");
                }
                else if (activePanel is ChooseTargetPanel)
                {
                    battleC.ChosenTarget = null;
                    panelC.DeactivePanel();
                    panelC.ActivePanel("ChooseMovePanel");
                }
            }
        }

        private void VictoryControlState()
        {
            if (Input.GetButtonDown("Confirm"))
            {
                GameController.Instance.Battle.Terminate();
                SetState(State.CharacterControl);
                GameController.Instance.Gui.DeactivePanel();

            }
        }

        public void SetState(State state)
        {
            this.state = state;
        }

    }
}