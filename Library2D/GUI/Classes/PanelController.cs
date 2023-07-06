using System.Collections;
using System.Collections.Generic;
using ToolBox.GUI.Utils;
using ToolBox.GUI;
using UnityEngine;

namespace ToolBox.GUI.Utils
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField]
        private List<Panel> panels;

        private PanelStack stack;

        private void Awake()
        {
            stack = new PanelStack();
        }

        public void ActivePanel(string name)
        {
            int index = -1;
            for (int i = 0; i < panels.Count; i++)
                if (panels[i].name.Equals(name))
                    index = i;
            GameObject canvas = this.transform.GetChild(0).gameObject;
            Panel p = GameObject.Instantiate(panels[index], canvas.transform);
            p.name = panels[index].name;
            stack.Push(p);
        }

        public void DeactivePanel()
        {
            stack.Pop();
        }

        public void Clear()
        {
            stack.Clear();
        }

        public void UseButtons()
        {
            stack.Top()?.UseButtons();
        }

        public Panel GetActivePanel()
        {
            return stack.Top();
        }
    }

}