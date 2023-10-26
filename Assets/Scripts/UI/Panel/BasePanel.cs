using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel 
{
    public GameObject Active_Obj;

    public UIType uitype;

    public BasePanel(UIType uItype)
    {
        uitype = uItype;
    }


    public virtual void Onstart()
    {
        Debug.Log("Panel1执行OnStart方法！");

        if (Active_Obj.GetComponent<CanvasGroup>() == null)
        {
            Active_Obj.AddComponent<CanvasGroup>();
            Debug.Log("Panel1执行添加Canvas方法！");
        }
        Debug.Log("Panel1为其自身添加CanvasGroup！");
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(Active_Obj).interactable = true;

    }


    public virtual void OnEnable()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(Active_Obj).interactable = true;
    }


    public virtual void OnDisable()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(Active_Obj).interactable = false;
    }


    public virtual void OnDestroy()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(Active_Obj).interactable = false;
    }

}
