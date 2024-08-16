using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Band.Otamatone.Mazer.Utils.Distance
{

    public static class DistanceMetrics
    {

        public static List<Func<string,string,int>> Metrics
        {
            get
            {
                return new List<Func<string, string, int>>()
                {
                    CountDistance,
                    CenterDistance,
                    SizeDistance,
                    DoorDistance
                };
            }
        }

        public static int CountDistance(string current, string previous)
        {
            int distance = 0;
            List<string> previousList=MazerUtils.ToAspList(previous);
            foreach (string currentFact in MazerUtils.ToAspList(current))
            {
                if(previousList.Contains(currentFact))
                {
                    distance++;
                }
            }
            return distance;
        }


        public static int CenterDistance(string current, string previous)
        {
            JObject currentJson = MazerUtils.SerializeLevel(current);
            JObject previousJson = MazerUtils.SerializeLevel(previous);
            int distance = 0;
            Func<int, int, int> operation = (a, b) => Math.Abs(a - b);
            foreach(JObject currentRoom in currentJson["rooms"])
            {
                JObject previousRoom = previousJson["rooms"]
                    .Where((room) => room["id"].Equals(currentRoom["id"]))
                    .ToList()[0] as JObject;
                if(previousRoom != null)
                {
                    int distX = ApplyOperation(currentRoom["center"]["x"], previousRoom["center"]["x"], operation);
                    int distY = ApplyOperation(currentRoom["center"]["y"], previousRoom["center"]["y"], operation);
                    distance += distX + distY;
                }
       
            }
            return distance;
        }

        public static int SizeDistance(string current, string previous)
        {
            JObject currentJson = MazerUtils.SerializeLevel(current);
            JObject previousJson = MazerUtils.SerializeLevel(previous);
            int distance = 0;
            Func<int, int, int> operation = (a, b) => Math.Abs(a - b);
            foreach (JObject currentRoom in currentJson["rooms"])
            {
                JObject previousRoom = previousJson["rooms"]
                    .Where((room) => room["id"].Equals(currentRoom["id"]))
                    .ToList()[0] as JObject;
                if (previousRoom != null)
                {
                    int distX = ApplyOperation(currentRoom["size"]["x"], previousRoom["size"]["x"], operation);
                    int distY = ApplyOperation(currentRoom["size"]["y"], previousRoom["size"]["y"], operation);
                    distance += distX + distY;
                }

            }
            return distance;
        }

        public static int DoorDistance(string current, string previous)
        {
            JObject currentJson = MazerUtils.SerializeLevel(current);
            JObject previousJson = MazerUtils.SerializeLevel(previous);
            int numDoorsCurrent = (currentJson["doors"] as JArray).Count();
            int numDoorsPrevious = (previousJson["doors"] as JArray).Count();
            return Math.Abs(numDoorsPrevious - numDoorsCurrent);
        }

        private static int ApplyOperation(JToken a, JToken b, Func<int,int,int> operation)
        {
            int aNum = a.Value<int>();
            int bNum = b.Value<int>();
            return operation(aNum, bNum);
        }
    }
}
