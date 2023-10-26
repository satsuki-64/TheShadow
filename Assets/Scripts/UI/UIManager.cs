using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject Canvas_Obj;
    //Canvas
    public Stack<BasePanel> stack_ui;

    public Dictionary<string, GameObject> dict_ui;


    private static UIManager instance;
    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("no instance here (UIMannger)");
            return instance;
        }
        return instance;
    }

    public UIManager()
    {
        instance = this;
        stack_ui = new Stack<BasePanel>();
        dict_ui = new Dictionary<string, GameObject>();
    }


    public GameObject GetSingleObject(UIType in_for)
    {
        if (dict_ui.ContainsKey(in_for.Name))
        {
            return dict_ui[in_for.Name];
        }
        if (Canvas_Obj == null)
        {
            Debug.LogWarning("no canvas here");
            Canvas_Obj = UIMethod.GetInstance().FindCanvas();
        }
        if (!dict_ui.ContainsKey(in_for.Name))
        {
            if (Canvas_Obj == null)
            {
                return null;
            }
            else
            {
                GameObject in_ui = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(in_for.Path), Canvas_Obj.transform);
                return in_ui;
            }
        }
        return null;
    }

    public GameObject GetSingleObject(string pathGet)
    {
        GameObject in_ui = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(pathGet));
        return in_ui;
    }


    public void Push(BasePanel basePanel_push)
    {
        if (stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisable();
        }

        GameObject basePanel_ui = GetSingleObject(basePanel_push.uitype);
        dict_ui.Add(basePanel_push.uitype.Name, basePanel_ui);
        basePanel_push.Active_Obj = basePanel_ui;

        if (stack_ui.Count == 0)
        {
            stack_ui.Push(basePanel_push);
        }

        if (stack_ui.Count > 0)
        {
            if (stack_ui.Peek().uitype.Name != basePanel_push.uitype.Name)
            {
                stack_ui.Push(basePanel_push);
            }
        }
        basePanel_push.Onstart();
    }




    public void Pop(bool isload)
    {
        if (isload == true)
        {
            if (stack_ui.Count > 0)
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestroy();
                GameObject.Destroy(dict_ui[stack_ui.Peek().uitype.Name]);
                dict_ui.Remove(stack_ui.Peek().uitype.Name);
                stack_ui.Pop();
                Pop(true);
            }
        }
        else
        {
            if (stack_ui.Count > 0)
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestroy();
                GameObject.Destroy(dict_ui[stack_ui.Peek().uitype.Name]);
                dict_ui.Remove(stack_ui.Peek().uitype.Name);
                stack_ui.Pop();
                if (stack_ui.Count > 0)
                {
                    stack_ui.Peek().OnEnable();
                }
            }
        }
    }


}
