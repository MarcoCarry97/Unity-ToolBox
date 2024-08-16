using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using ToolBox.Extensions;
using ToolBox.Core;
using ToolBox.Control.Explorer2D;

public class BattleController : MonoBehaviour
{
    private Party allieds;

    private Party enemies;

    public Party AlliedParty { get { return allieds; } }

    public Party EnemyParty { get { return enemies; } }

    public Move ChosenMove { get; set; }

    public MobController ChosenTarget { get; set; }

    private Scene previousScene;
    public void Begin(Party allieds, Party enemies)
    {
        this.allieds = allieds;
        this.enemies = enemies;
        GameController.Instance.Commands.SetState(InputController.State.DialogControl);
        previousScene =SceneManager.GetActiveScene();
        AsyncOperation operation=SceneManager.LoadSceneAsync("BattleScene",LoadSceneMode.Additive);
        StartCoroutine(BattleLoaded(operation));
    }

    public void Terminate()
    {
        allieds = enemies = null;
        Scene currentScene= SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(previousScene);
        AsyncOperation operation=SceneManager.UnloadSceneAsync(currentScene);
        StartCoroutine(WorldLoaded(operation));
    }

    private IEnumerator BattleLoaded(AsyncOperation operation)
    {
        yield return new WaitUntil(()=>operation.isDone);
        previousScene.SetActive(false);
        Scene scene = SceneManager.GetSceneByName("BattleScene");
        ChangeCamera(false);
        SceneManager.SetActiveScene(scene);
        BattleSystem battle=GameObject.FindObjectOfType<BattleSystem>();
        battle.SetState(BattleSystem.State.Preparing);
    }

    private IEnumerator WorldLoaded(AsyncOperation operation)
    {
        yield return new WaitUntil(() => operation.isDone);
        ChangeCamera(true);
        SceneManager.SetActiveScene(previousScene);
        previousScene.SetActive(true);
    }

    private void ChangeCamera(bool finished)
    {
        List<Camera> cameras = GameObject.FindObjectsOfType<Camera>().ToList<Camera>();
        foreach(Camera camera in cameras)
        {
            if (camera.gameObject.scene.name == "BattleScene")
            {
                camera.gameObject.SetActive(!finished);
            }
            else camera.gameObject.SetActive(finished);
        }
    }
}
