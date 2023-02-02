using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Interaction
{
    public class Interactor : MonoBehaviour
    {

        public Interactable NearestObject { get; set; }

        public void Interact(Interactable obj)
        {
            obj.Activate();
        }

        private void Start()
        {
        }

        private void Update()
        {
            if(Input.GetButtonDown("Confirm") && NearestObject!=null)
            {
                this.Interact(NearestObject);
            }

        }


    }

    
}
