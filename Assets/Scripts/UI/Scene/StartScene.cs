using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : SceneBase
{
    public readonly string StartScnenName = "UITestScene";

    public override void EnterScene()
    {
        StartPanel startPanel = new StartPanel();
        GameRoot.GetInstance().uIMannger.Push(startPanel);
    }

    public override void ExitScene()
    {
        
    }
}
