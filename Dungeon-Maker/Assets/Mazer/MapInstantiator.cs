using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstantiator : MonoBehaviour
{
    [SerializeField]
    private DungeonMaker maker;

    [SerializeField]
    private World worldPrefab;

    public IEnumerator Compose()
    {
        World lastWorld= null;
        for (int i = 0; i < maker.NumberOfLevels; i++)
        {
            World world=Instantiate(worldPrefab).GetComponent<World>();
            yield return new WaitForEndOfFrame();
            print("World: "+world);
            maker.World = world;
            yield return StartCoroutine(maker.Generate());
            yield return StartCoroutine(maker.Build(i));
            if(lastWorld!=null)
                world.transform.position = lastWorld.transform.position+new Vector3(5,0,0);
            lastWorld = world;
        }

    }
}
