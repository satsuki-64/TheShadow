using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePanel : BasePanel
{ 
    private static string SafePanelName = "SafeUI";
    private static string SafePanelPath = "UI/SafeUI";

    public static readonly UIType uItype = new UIType(SafePanelName, SafePanelPath);


    public SafePanel() : base(uItype)
    {

    }




    

    //ÍË³ö
    public void ExitGame()
    {
        StartScene startScene = new StartScene();

        GameRoot.GetInstance().uIMannger.Pop(true);
        SceneControl.GetInstance().ScenesLoad(startScene.StartScnenName, startScene);
    }



    public override void Onstart()
    {
        base.Onstart();
        

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
