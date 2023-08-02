using Mazer.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazer.Utils
{
    public interface IMazer
    {
        public abstract IEnumerator Generate();

        public abstract IEnumerator Build(int index);

        public abstract IEnumerator Build(LevelData level);
    }
}
