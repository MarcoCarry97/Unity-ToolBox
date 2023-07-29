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
        yield return StartCoroutine(maker.Generate());
        foreach(LevelData level in maker.Dungeon.Levels)
        {
            World world=Instantiate(worldPrefab).GetComponent<World>();
            yield return new WaitForEndOfFrame();
            maker.World = world;
            if (lastWorld != null)
                world.transform.position = lastWorld.transform.position + Vector3.right * 300;
            lastWorld = world;
            yield return StartCoroutine(maker.Build(level));
        }

    }
}
