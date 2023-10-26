using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FailPanel : BasePanel
{
    private static string FailPanelName = "FailUI";
    private static string FailPanelPath = "UI/FailUI";

    public static readonly UIType uItype = new UIType(FailPanelName, FailPanelPath);


    public FailPanel() : base(uItype)
    {

    }




    public void ReStartGame()
    {
        StatusPanel statusPanel = new StatusPanel();
        GameRoot.GetInstance().uIMannger.Pop(true);
        GameRoot.GetInstance().uIMannger.Push(statusPanel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //ÍË³ö
    public void ExitGame()
    {
        StartScene startScene = new StartScene();

        GameRoot.GetInstance().uIMannger.Pop(true);

        SceneManager.LoadScene("StartScene");
        SceneControl.GetInstance().ScenesLoad(startScene.StartScnenName, startScene);
    }



    public override void Onstart()
    {
        base.Onstart();
        UIMethod.GetInstance().GetOrAddComponentInChild<Button>(Active_Obj, "Restart").onClick.AddListener(ReStartGame);
        UIMethod.GetInstance().GetOrAddComponentInChild<Button>(Active_Obj, "Back").onClick.AddListener(ExitGame);
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
