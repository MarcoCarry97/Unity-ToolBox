using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Band.Otamatone.Mazer.Data
{
    [Serializable]
    public class DoorData : ScriptableObject
    {
        [SerializeField]
        private int start;

        [SerializeField]
        private int end;

        [SerializeField]
        private string orientation;

        public int Start { get { return start; } set { start = value; } }
        public int End { get { return end; } set { end = value; } }
        public string Orientation { get { return orientation; } set { orientation = value; } }

        public (int, int) GetOrientationValues(int distance)
        {
            int x = 0, y = 0;
            if (orientation.Equals("east"))
                x = distance;
            else if (orientation.Equals("west"))
                x = -distance;
            else if (orientation.Equals("south"))
                y = -distance;
            else if (orientation.Equals("north"))
                y = distance;
            return (x, y);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
