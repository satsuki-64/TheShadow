using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : BasePanel
{    

    private static string StatusPanelName = "StatusUI";
    private static string StatusPanelPath = "UI/StatusUI";

    public static readonly UIType uItype = new UIType(StatusPanelName, StatusPanelPath);


    public StatusPanel() : base(uItype)
    {

    }

    
    public void GamePause()
    {
        Debug.Log("开始push");
        PausePanel pausePanel = new PausePanel();
        GameRoot.GetInstance().uIMannger.Push(pausePanel);
        
    }

    public void SetFillAmoutValue(float newNumber = 0f) 
    {
        if (newNumber <=1 &newNumber >=0) 
        {
            if (Active_Obj != null)
            {
                Debug.Log(Active_Obj.name);
                UIMethod.GetInstance().GetOrAddComponentInChild<Image>(Active_Obj, "Props").fillAmount = newNumber;
            }
            else 
            {
                Debug.LogWarning("StatusPanel`s Active_Obj is null!");
            }
            
            
        }
    }

    //能量减
    public void PowerValueDown(float degreeNum)
    {
        UIMethod.GetInstance().GetOrAddComponentInChild<Image>(Active_Obj, "Props").fillAmount -= degreeNum *Time.deltaTime;
    }


    //能量加
    public void PowerValueUp(float degreeNum)
    {
        UIMethod.GetInstance().GetOrAddComponentInChild<Image>(Active_Obj, "Props").fillAmount += degreeNum * Time.deltaTime;
    }


    //安全屋界面
    public void PushSafe()
    {
        SafePanel safePanel = new SafePanel();
        GameRoot.GetInstance().uIMannger.Pop(false);
        GameRoot.GetInstance().uIMannger.Push(safePanel);
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
