using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;

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
            AspProgram program=ScriptableObject.CreateInstance<AspProgram>();
            StreamReader reader = new StreamReader(pathname);
            while(!reader.EndOfStream)
                program.Content = reader.ReadToEnd();
            reader.Close();
            string[] parts=pathname.Split('/');
            program.name = parts[parts.Length-1];
            return program;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
