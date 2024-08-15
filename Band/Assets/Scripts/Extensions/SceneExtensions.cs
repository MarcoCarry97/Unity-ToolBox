using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Band.Extensions
{
    public static class SceneExtensions
    {
        public static void SetActive(this Scene scene, bool active)
        {
            foreach (GameObject game in scene.GetRootGameObjects())
                game.SetActive(active);
        }

        public static List<string> DisableActiveObjects(this Scene scene)
        {
            List<string> list = new List<string>();
            foreach(GameObject game in scene.GetRootGameObjects())
            {
                if(game.activeSelf)
                {
                    list.Add(game.name);
                    game.SetActive(false);
                }
            }
            return list;
        }

        public static void EnableDeactiveObjects(this Scene scene,List<string> list)
        {
            foreach(GameObject game in scene.GetRootGameObjects())
            {
                if(list.Contains(game.name))
                {
                    game.SetActive(true);
                }
            }
        }

    }
}
