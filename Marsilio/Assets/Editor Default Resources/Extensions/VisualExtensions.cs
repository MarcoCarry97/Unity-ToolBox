using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolBox.Extensions.Editor
{
    public static class VisualExtensions
    {
        public static void PutManipulator(this VisualElement element,IManipulator manipulator)
        {
            manipulator.target = element;
        }
    }
}
