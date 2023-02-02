using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.GUI.Utils;
using Unity.Services.Core;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Threading;

namespace ToolBox.GUI.Widgets
{
    public class ConversationView : Widget
    {
        public enum TextVelocity
        {
            Low,
            Medium,
            High
        }

        public enum SubmitMethod
        {
            Timed,
            Pushed
        }

        private enum State
        {
            Writing,
            Finish
        }

        public Conversation conversation;

        private State state;

        public TextVelocity textVelocity;

        public SubmitMethod submitMethod;

        public float interval;


        private InputController input;

        private Coroutine writeCoroutine;

        private Coroutine waitCoroutine;

        private void Start()
        {
            state = State.Writing;

            TextMeshProUGUI textBox = GetTextBox();
            TextMeshProUGUI speakerBox = GetSpeakerBox();
            textBox.text = speakerBox.text = "";
            input= GameObject.Find("Controller")
                .GetComponent<InputController>();
            input.SetState(InputController.State.DialogControl);
        }

        private void Update()
        {
            print(state);
            switch(state)
            {
                case State.Writing:
                    WritingState();
                    break;
                case State.Finish:
                    FinishState();
                    break;
            }
        }

        private float GetTime()
        {
            float time = 1f;
            switch(textVelocity)
            {
                case TextVelocity.Low:
                    time *= 0.2f;
                    break;
                case TextVelocity.Medium:
                    time *= 0.1f;
                    break;
                case TextVelocity.High:
                    time *= 0.02f;
                    break;
            }
            return time;
        }

        public TextMeshProUGUI GetTextBox()
        {
            GameObject game = this.transform.GetChild(0).GetChild(0).gameObject;
            return game.GetComponent<TextMeshProUGUI>();
        }

        public float GetTextHeight()
        {
            return GetTextBox().rectTransform.rect.height;
        }

        public TextMeshProUGUI GetSpeakerBox()
        {
            GameObject game = this.transform.GetChild(1).GetChild(0).gameObject;
            return game.GetComponent<TextMeshProUGUI>();
        }

        public void ClearText()
        {
            TextMeshProUGUI textview = GetTextBox();
            textview.text = "";
        }

        private void WritingState()
        {
            
            if (writeCoroutine==null)
                writeCoroutine = StartCoroutine(WriteConversation());

        }

        private IEnumerator WriteConversation()
        {
            TextMeshProUGUI speaker = GetSpeakerBox();
            TextMeshProUGUI textview = GetTextBox();
            foreach (Dialogue dialogue in conversation)
            {
                ClearText();
                speaker.text = dialogue.Speaker;
                foreach (char c in dialogue)
                {
                    yield return new WaitForSeconds(GetTime());
                    textview.text += c;
                }
                if (submitMethod == SubmitMethod.Timed)
                    yield return new WaitForSeconds(interval);
                else if (submitMethod == SubmitMethod.Pushed)
                    yield return new WaitUntil(() => Input.GetButtonDown("Confirm"));
            }
            state = State.Finish;
        }      

        private void FinishState()
        {
            input.SetState(InputController.State.CharacterControl);
            Destroy(this.gameObject);
        }
        



    }
}
