using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChooseView : MonoBehaviour
{

    [SerializeField]
    private string folderName;

    [SerializeField]
    private Color color;

    private Sprite chosen;

    private List<Sprite> sprites;

    private int currentIndex;

    private Button leftButton;

    private Button rightButton;

    private RawImage imageView;

    public Sprite CurrentChoice
    {
        get { return chosen; }
    }

    public Color CurrentColor
    {
        get { return color; }
    }

    // Start is called before the first frame update
    void Start()
    {
        color = Color.white;
        imageView=transform.GetChild(0).GetComponent<RawImage>();
        sprites = Resources.LoadAll<Sprite>("CharacterEditor/Sprites/" + folderName).ToList<Sprite>();
        currentIndex= 0;
        leftButton = transform.GetChild(2).GetComponent<Button>();
        rightButton = transform.GetChild(1).GetComponent<Button>();
        leftButton.onClick.AddListener(OnLeft);
        rightButton.onClick.AddListener(OnRight);
        chosen = sprites[currentIndex];
    }

    private void OnLeft()
    {
        currentIndex=(currentIndex+1)%sprites.Count;
    }

    private void OnRight()
    {
        currentIndex--;
        if(currentIndex<0)
            currentIndex=sprites.Count-1;

    }

    // Update is called once per frame
    void Update()
    {
        chosen = sprites[currentIndex];
        imageView.texture = chosen.texture;
        imageView.color = color;
    }
}
