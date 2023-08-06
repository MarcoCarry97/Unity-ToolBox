using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.GUI.Widgets
{
    public class Widget : MonoBehaviour, ISelectable 
    {
        public void Build()
        {

        }

        public bool IsSelectable()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSelected()
        {
            throw new System.NotImplementedException();
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateWidget()
        {
            
        }
    }
}
