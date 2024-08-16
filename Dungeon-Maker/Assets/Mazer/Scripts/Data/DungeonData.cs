using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Mazer.Data
{
    [Serializable]
    public class DungeonData : ScriptableObject
    {
        [SerializeField]
        private string status;

        [SerializeField]
        private List<LevelData> levels;

        public string Status { get { return status; } set { status = value; } }

        public List<LevelData> Levels { get { return levels; } set { levels = value; } }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
