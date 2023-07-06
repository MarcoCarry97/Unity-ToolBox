using System.Collections;
using System.Collections.Generic;
using ToolBox.Interaction.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static ToolBox.Interaction.InteractionFlag;
using ToolBox.Extensions;

namespace ToolBox.Interaction
{
    [RequireComponent (typeof(Collider2D))]
    public class Interactable : MonoBehaviour, IInteractable
    {

        private bool isCollidedWithInteractor;
        public UnityEvent OnEvent;

        private Coroutine mutexCoroutine;

        private void Start()
        {
            isCollidedWithInteractor = false;
        }

        private void Update()
        {
            
        }

        public void Activate()
        {
            if (isCollidedWithInteractor)
            {
                if (mutexCoroutine == null)
                    mutexCoroutine = StartCoroutine(DoInteraction());
            }
        }

        private IEnumerator DoInteraction()
        {
            OnEvent.Invoke();
            yield return null;
            /*yield return new WaitUntil(EndCondition);
            mutexCoroutine = null;
            yield break;*/
        }

        private bool EndCondition()
        {
            return GameObject.FindGameObjectWithTag("GUI")
                .transform.childCount == 0;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.HasComponent<Interactor>())
            {
                isCollidedWithInteractor = true;
                Interactor actor = collider.GetComponent<Interactor>();
                actor.NearestObject = this;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.HasComponent<Interactor>())
            {
                isCollidedWithInteractor = false;
                Interactor actor = collider.GetComponent<Interactor>();
                actor.NearestObject = null;
            }
        }

    }
}
