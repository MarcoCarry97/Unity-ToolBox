using System.Collections;
using System.Collections.Generic;
using ToolBox.Extensions;
using UnityEngine;
using UnityEngine.UI;

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
            if(!dontDeactive)
                this.gameObject.SetActive(false);
        }

        public void UseButtons()
        {
            this.GetComponentInChildren<Button>().Select();
        }
    }
}