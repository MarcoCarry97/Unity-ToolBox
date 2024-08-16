using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Otamatone
{


    public abstract class GeneralBehaviour : IReasonable
    {
        public abstract IReasonable Reason();
    }
}
