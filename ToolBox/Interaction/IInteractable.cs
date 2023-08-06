using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Interaction.Utils
{
    public interface IInteractable
    {
        private extern void InteractWhenPushed();

        private extern void InteractWhenCollided();


    }
}
