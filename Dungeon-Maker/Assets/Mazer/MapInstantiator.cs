using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mazer.Generators;
using Mazer.Data;

public class MapInstantiator : MonoBehaviour
{
    /// <summary>
    /// /private DungeonMaker maker;
    /// </summary>
    /// 

    [SerializeField]
    private ClingoMazer maker;

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
            Vector3 vecToCenter = new Vector3(level.GetRoom(level.Initial_Room).TrueCenter.X, level.GetRoom(level.Initial_Room).TrueCenter.Y, 0);
            if (lastWorld != null)
                world.transform.position = lastWorld.transform.position - vecToCenter + Vector3.right*100;
            lastWorld = world;
            yield return StartCoroutine(maker.Build(level));
        }

    }
}
