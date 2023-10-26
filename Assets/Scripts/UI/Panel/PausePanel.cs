using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    private static string PausePanelName = "PauseUI";
    private static string PausePanelPath = "UI/PauseUI";

    public static readonly UIType uItype = new UIType(PausePanelName, PausePanelPath);


    public PausePanel() : base(uItype)
    {

    }

    //继续
    public void ContinueGame()
    {
        
        GameRoot.GetInstance().uIMannger.Pop(false);


    }


    //重开
    public void ReStartGame()
    {
        StatusPanel statusPanel = new StatusPanel();
        GameRoot.GetInstance().uIMannger.Pop(true);
        GameRoot.GetInstance().uIMannger.Push(statusPanel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //退出
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
        UIMethod.GetInstance().GetOrAddComponentInChild<Button>(Active_Obj, "Continue").onClick.AddListener(ContinueGame);
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
