using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Band.GUI.Utils
{
    public class GuiStack
    {
        private Stack<Gui> stack;
        private Stack<string> names;

        public GuiStack()
        {
            stack = new Stack<Gui>();
            names = new Stack<string>() { };
        }

        public void Push(Gui p)
        {
            if (stack.Count >= 1)
            {
                Gui top = stack.Pop();
                top.Hide();
                stack.Push(top);
            }
            p.Show();
            stack.Push(p);
            names.Push(p.name);
        }

        public void Pop()
        {
            if (stack.Count >= 1)
            {
                Gui p = stack.Pop();
                names.Pop();
                if (stack.Count != 0)
                {
                    Gui top = stack.Pop();
                    top.Show();
                    stack.Push(top);
                }
                GameObject.Destroy(p.gameObject);
            }

        }

        public Gui Top()
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
