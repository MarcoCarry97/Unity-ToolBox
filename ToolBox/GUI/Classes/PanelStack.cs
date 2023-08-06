using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ToolBox.GUI.Utils
{
    public class PanelStack
    {
        private Stack<Panel> stack;
        private Stack<string> names;

        public PanelStack()
        {
            stack = new Stack<Panel>();
            names = new Stack<string>() { };
        }

        public void Push(Panel p)
        {
            if (stack.Count >= 1)
            {
                Panel top = stack.Pop();
                top.Deactive();
                stack.Push(top);
            }
            p.Active();
            stack.Push(p);
            names.Push(p.name);
        }

        public void Pop()
        {
            if (stack.Count >= 1)
            {
                Panel p = stack.Pop();
                names.Pop();
                if (stack.Count != 0)
                {
                    Panel top = stack.Pop();
                    top.Active();
                    stack.Push(top);
                }
                GameObject.Destroy(p.gameObject);
            }

        }

        public Panel Top()
        {
            return stack.First();   
        }

        public void Clear()
        {
            while (stack.Count != 0)
            {
                Pop();
            }
        }
    }
}
