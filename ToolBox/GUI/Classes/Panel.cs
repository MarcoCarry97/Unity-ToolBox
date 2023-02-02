using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.GUI
{

    public class Panel : MonoBehaviour
    {
        public bool dontDeactive;
        public void Active()
        {
            this.gameObject.SetActive(true);
        }

        public void Deactive()
        {
            if(dontDeactive)
                this.gameObject.SetActive(false);
        }
    }
}