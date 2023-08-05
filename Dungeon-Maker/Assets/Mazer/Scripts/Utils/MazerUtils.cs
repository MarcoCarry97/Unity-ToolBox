using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mazer.Utils
{


    public static class MazerUtils
    {
        

        public static JArray SerializeLevels(List<string> levels)
        {
            JArray json=new JArray();
            foreach (string level in levels)
                json.Add(SerializeLevel(level));
            return json;
        }

        public static JObject SerializeLevel(string level)
        {
            Dictionary<string, List<string>> levelDict = GetDictionaryFromLevel(level);
            JObject json=new JObject();
            
            List<JObject> jsonSizeList = SerializeSizeList(levelDict);
            List<JObject> jsonExpansionList = SerializeExpansionList(levelDict,jsonSizeList);
            int initRoom = -1;
            if (levelDict["initial_room"].Count != 0)
                initRoom = SerializeInitRoom(levelDict);
            json["initial_room"] = initRoom;
            json["decorations"] = SerializeDecorationList(levelDict);
            if (levelDict["stairs_pos"].Count != 0)
                json["stairs"] = SerializeDecoration(levelDict["stairs_pos"][0],"stairs");
            if (levelDict["start_point_pos"].Count != 0)
                json["start_point_pos"] = SerializeDecoration(levelDict["start_point_pos"][0],"start_point");
            json["rooms"] = SerializeRoomList(levelDict, jsonSizeList, jsonExpansionList);
            json["doors"] = SerializeDoorList(levelDict);
            return json;
        }

        public static JArray SerializeDoorList(Dictionary<string,List<string>> levelDict)
        {
            JArray json = new JArray();
            foreach (string fact in levelDict["door"])
                json.Add(SerializeDoor(fact));
            return json;
        }

        public static JObject SerializeDoor(string fact)
        {
            JObject json = new JObject();
            fact = fact.Replace("door(", "").Replace(")", "");
            List<string> parts = fact.Split(",").ToList<string>();
            json["start"] = int.Parse(parts[0]);
            json["end"] = int.Parse(parts[1]);
            json["orientation"] = parts[2];
            return json;
        }


        public static JArray SerializeRoomList(Dictionary<string, List<string>> levelDict,List<JObject> sizeList,List<JObject> expList)
        {
            JArray json = new JArray();
            foreach(string fact in levelDict["place_center"])
            {
                json.Add(SerializeRoom(fact, sizeList, expList));
            }
            return json;
        }

        public static JObject SerializeRoom(string fact,List<JObject> sizeList, List<JObject> expList)
        {
            JObject json = new JObject();
            fact = fact.Replace("place_center(", "").Replace(")", "");
            List<string> parts=fact.Split(",").ToList<string>();
            JObject center=new JObject();
            json["id"] = int.Parse(parts[0]);
            center["x"] = int.Parse(parts[1]);
            center["y"] = int.Parse(parts[2]);
            JObject trueCenter = new JObject();
            trueCenter["x"] = int.Parse(parts[3]);
            trueCenter["y"] = int.Parse(parts[4]);
            json["center"] = center;
            json["truecenter"] = trueCenter;
            foreach(JObject size in sizeList)
            {
                if (size["room"].Equals(json["id"]))
                {
                    JObject sizeJson = new JObject();
                    sizeJson["x"] = size["x"];
                    sizeJson["y"] = size["y"];
                    json["size"] = sizeJson;
                }
            }
            JArray expansions = new JArray();
            foreach (JObject exp in expList)
            {
                if (exp["room"].Equals(json["id"]))
                {
                    JObject expJson = new JObject();
                    expJson["center"] = exp["center"];
                    expJson["truecenter"] = exp["truecenter"];
                    expJson["size"] = exp["size"];
                    expansions.Add(expJson);
                }
            }
            json["expansions"] = expansions;
            return json;
        }

        public static int SerializeInitRoom(Dictionary<string, List<string>> levelDict)
        {
            string initRoom = levelDict["initial_room"][0];
            initRoom = initRoom.Replace("initial_room(", "").Replace(")", "");
            return int.Parse(initRoom);
        }

        public static JArray SerializeDecorationList(Dictionary<string, List<string>> levelDict)
        {
            JArray jsonArray = new JArray();
            List<string> names = new List<string>() {
                "trap", "treasure",
                "enemy", "key", "item"
            };
            foreach(string name in names)
            {
                foreach(string fact in levelDict[name+"_pos"])
                {
                    jsonArray.Add(SerializeDecoration(fact, name));
                }
            }
            return jsonArray;
        }

        public static JObject SerializeDecoration(string fact,string name)
        {
            JObject json = new JObject();
            fact = fact.Replace(name + "_pos(", "").Replace(")", "");
            List<string> parts = fact.Split(",").ToList<string>();
            json["type"] = name;
            json["index"] = int.Parse(parts[0]);
            json["room"] = int.Parse(parts[1]);
            JObject pos=new JObject();
            pos["x"] = int.Parse(parts[2]);
            pos["y"] = int.Parse(parts[3]);
            json["position"] = pos;
            return json;
        }

        public static List<JObject> SerializeSizeList(Dictionary<string, List<string>> levelDict)
        {
            List<JObject> list=new List<JObject> ();
            foreach (string fact in levelDict["place_size"])
                list.Add(SerializeSize(fact));
            return list;
        }

        public static JObject SerializeSize(string fact)
        {
            JObject json = new JObject();
            fact = fact.Replace("place_size(", "").Replace(")", "");
            List<string> parts = fact.Split(',').ToList<string>();
            json["room"] = int.Parse(parts[0]);
            json["x"] = int.Parse(parts[3]);
            json["y"] = int.Parse(parts[4]);
            return json;
        }

        public static List<JObject> SerializeExpansionList(Dictionary<string, List<string>> levelDict,List<JObject> sizeList)
        {
            List<JObject> list = new List<JObject>();
            foreach (string fact in levelDict["expansion"])
            {
                JObject expJson = SerializeExpansion(levelDict, fact, sizeList);
                if(expJson!=null)
                    list.Add(expJson);
            }
            return list;
        }

        public static JObject SerializeExpansion(Dictionary<string,List<string>> levelDict, string fact, List<JObject> sizeList)
        {
            fact = fact.Replace("expansion(","").Replace(")", "");
            List<string> parts = fact.Split(',').ToList<string>();
            foreach(string roomFact in levelDict["place_center"])
            {
                int id = int.Parse(parts[0]);
                int expId = int.Parse(parts[1]);
                int roomId = int.Parse(parts[2]);
                JObject roomJson = SerializeRoom(roomFact, sizeList, new List<JObject>());
                int roomJsonId = (roomJson["id"] as JToken).Value<int>();
                if (roomJsonId.Equals(id))
                {
                    JObject json = new JObject();
                    json["id"] = id;
                    json["room"] = roomId;
                    json["center"] = roomJson["center"];
                    json["truecenter"] = roomJson["truecenter"];
                    json["size"] = roomJson["size"];
                    return json;
                }
            }
            return null;
        }

        public static Dictionary<string,List<string>> GetDictionaryFromLevel(string level)
        {
            List<string> levelList = ToAspList(level);
            Dictionary<string,List<string>> levelDict=new Dictionary<string,List<string>>(); ;
            List<string> labels = new List<string>
            {
                "place_size","place_center","door",
                "initial_room","trap_pos","treasure_pos",
                "key_pos","item_pos","stairs_pos","start_point_pos",
                "enemy_pos","expansion"
            };
            foreach(string  label in labels)
                FillDictionary(levelDict,levelList, label);
            return levelDict;
        }

        private static void FillDictionary(Dictionary<string,List<string>> dict,List<string> list, string label)
        {
            dict[label] = ExtractFromList(list, label);
        }

        public static List<string> ToAspList(string level)
        {
            return level.Split(".")
                .Where((s)=>!s.Trim().Equals(""))
                .ToList<string>();
        }

        public static string ToAspString(List<string> modelList)
        {
            string model = modelList.Aggregate<string>((string a, string b) => a + ".\n" + b);
            if (!model.Trim().Equals(""))
                model += ".\n";
            return model;
        }

        public static List<string> ConvertToListOfStrings(Dictionary<int,List<string>> dict)
        {
            List<string> models = new List<string>();
            foreach (List<string> modelList in dict.Values)
                models.Add(MazerUtils.ToAspString(modelList));
            return models;
        }

        private static List<string> ExtractFromList(List<string> list, string factName)
        {
            List<string> res=new List<string>();
            foreach (string fact in list)
            {
                string noCrFact = fact.Replace("\n", "").Trim();
                List<string> parts = noCrFact.Split("(").ToList<string>();
                if (parts[0].Equals(factName))
                    res.Add(noCrFact);
            }
            return res;
        }
    }

    
}
