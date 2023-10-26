using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static string StartPanelName = "StartUI";
    private static string StartPanelPath = "UI/StartUI";

    public static readonly UIType uItype = new UIType(StartPanelName, StartPanelPath);


    public StartPanel() : base(uItype)
    {

    }


    //开始游戏
    public void StartGame()
    {
        StatusPanel statusPanel = new StatusPanel();
        PlayScene playScene = new PlayScene();

        SceneControl.GetInstance().ScenesLoad(playScene.PlayScnenName, playScene);

        
        Debug.Log("跳入状态界面");
    }

    public void HelpGame()
    {
        HelpPanel helpPanel = new HelpPanel();
        GameRoot.GetInstance().uIMannger.Pop(true);
        GameRoot.GetInstance().uIMannger.Push(helpPanel);
        Debug.Log("帮助界面");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public override void Onstart()
    {
        base.Onstart();
        UIMethod.GetInstance().GetOrAddComponentInChild<Button>(Active_Obj, "Help").onClick.AddListener(HelpGame);
        UIMethod.GetInstance().GetOrAddComponentInChild<Button>(Active_Obj, "Start").onClick.AddListener(StartGame);
        UIMethod.GetInstance().GetOrAddComponentInChild<Button>(Active_Obj, "Exit").onClick.AddListener(ExitGame);
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
