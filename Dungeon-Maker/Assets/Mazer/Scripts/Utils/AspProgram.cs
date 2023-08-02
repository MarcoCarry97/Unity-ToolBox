using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft;
using Newtonsoft.Json;

namespace Mazer.Utils
{
    [Serializable]
    [CreateAssetMenu(menuName = "Mazer/Script/AspProgram", fileName = "AspProgram")]
    public class AspProgram : ScriptableObject
    {

        [SerializeField]
        [TextArea]
        private string content;

        public string Content { get { return content; } set { content = value; } }

        public static AspProgram FromFile(string pathname)
        {
            throw new NotImplementedException();
        }

        public AspProgram ToFile()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
