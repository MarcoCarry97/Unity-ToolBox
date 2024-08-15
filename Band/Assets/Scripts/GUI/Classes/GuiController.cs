using System.Collections;
using System.Collections.Generic;
using Band.GUI.Utils;
using Band.GUI;
using UnityEngine;
using Band.Utils;

namespace Band.GUI.Utils
{
    public class GuiController : MonoBehaviour,IController
    {
        [SerializeField]
        private List<Gui> panels;

        private GuiStack stack;

        private void Awake()
        {
            stack = new GuiStack();
        }

        public void Show(string name)
        {
            int index = -1;
            for (int i = 0; i < panels.Count; i++)
                if (panels[i].name.Equals(name))
                    index = i;
            GameObject canvas = this.GetComponentInChildren<Canvas>().gameObject;
            Gui p = GameObject.Instantiate(panels[index], canvas.transform);
            p.name = panels[index].name;
            stack.Push(p);
        }

        public void Hide()
        {
            stack.Pop();
        }

        public void Clear()
        {
            stack.Clear();
        }

        public Gui GetCurrenteGui()
        {
            return stack.Top();
        }
    }

}