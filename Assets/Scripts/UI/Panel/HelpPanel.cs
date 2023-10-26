using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : BasePanel
{
    private static string HelpPanelName = "HelpUI";
    private static string HelpPanelPath = "UI/HelpUI";

    public static readonly UIType uItype = new UIType(HelpPanelName, HelpPanelPath);


    public HelpPanel() : base(uItype)
    {

    }


    //退出该界面
    public void ExitHelpPanel()
    {
        StartPanel startPanel = new StartPanel();
        
        GameRoot.GetInstance().uIMannger.Pop(true);
        GameRoot.GetInstance().uIMannger.Push(startPanel);
        
        
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
