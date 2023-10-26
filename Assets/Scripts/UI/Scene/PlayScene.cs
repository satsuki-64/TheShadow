using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : SceneBase
{
    public readonly string PlayScnenName = "GamePlayScene1";

    public override void EnterScene()
    {
        StatusPanel statusPanel = new StatusPanel();
        GameRoot.GetInstance().uIMannger.Push(statusPanel);
    }

    public override void ExitScene()
    {

    }
}
