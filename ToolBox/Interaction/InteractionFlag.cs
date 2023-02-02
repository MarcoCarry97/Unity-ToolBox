using System.Collections;
using System.Collections.Generic;
using ToolBox.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Interaction
{
    public class InteractionFlag : MonoBehaviour
    {
        public class Flag
        {
            public bool Value { get; set; }

            public Flag()
            {
                Value = false;
            }
        }

        public Flag flag;

        public UnityEvent<Flag> OnEvent;

        private void Start()
        {
            flag=new Flag();
            OnEvent.Invoke(flag);
        }


    }
}