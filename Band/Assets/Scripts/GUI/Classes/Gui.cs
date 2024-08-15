using System.Collections;
using System.Collections.Generic;
using Band.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Band.GUI
{

    public class Gui : MonoBehaviour
    {
        [SerializeField]
        private bool dontHide;
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if(!dontHide)
                this.gameObject.SetActive(false);
        }

    }
}