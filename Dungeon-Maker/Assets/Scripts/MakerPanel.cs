using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakerPanel : MonoBehaviour
{
    private Button generateButton;
    private Button previousButton;
    private Button nextButton;

    private MapInstantiator map;

    private int current;

    // Start is called before the first frame update
    void Start()
    {
        generateButton=this.transform.GetChild(0).GetComponent<Button>();
        previousButton=this.transform.GetChild(1).GetComponent<Button>();
        nextButton=this.transform.GetChild(2).GetComponent<Button>();
        generateButton.onClick.AddListener(OnGenerate);
        previousButton.onClick.AddListener(OnPrevious);
        nextButton.onClick.AddListener(OnNext);

        map=GameObject.Find("MapInstantiator").GetComponent<MapInstantiator>();
        current = 0;
    }

    private void OnGenerate()
    {
        StartCoroutine(map.Compose());
        //maker.Generate();
        current = 0;
    }

    private void OnPrevious()
    {
        current--;
        if (current < 0)
            current = 0;
        //StartCoroutine(maker.Build(current));
    }

    private void OnNext()
    {
        //current=(current+1)%maker.Dungeon.Levels.Count;
        //StartCoroutine(maker.Build(current));
    }

    private IEnumerator BuildCoroutine()
    {
        //maker.Build(current);
        yield return null;
    }
}
