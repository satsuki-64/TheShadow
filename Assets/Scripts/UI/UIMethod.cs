using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIMethod
{
    private static UIMethod instance;
    public static UIMethod GetInstance()
    {
        if (instance == null)
        {
            instance = new UIMethod();
        }
        return instance;
    }


    public GameObject FindObjectInChild(GameObject Obj_Child, string Child_name)
    {
        Transform[] transforms_find = Obj_Child.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms_find)
        {
            if (tra.gameObject.name == Child_name)
            {
                return tra.gameObject;
                break;
            }
        }
        return null;
    }



    public GameObject FindCanvas()
    {
        GameObject find_Canvas = GameObject.FindObjectOfType<Canvas>().gameObject;

        if (find_Canvas == null)
        {
            Debug.LogError("cant find canvas");
            return null;
        }
        return find_Canvas;
    }



    public T AddOrGetComponent<T>(GameObject get_Con) where T : Component
    {
        if (get_Con.GetComponent<T>() == null)
        {
            return null;
        }
        Debug.LogWarning("can find Component");
        return get_Con.GetComponent<T>();
    }


    public T GetOrAddComponentInChild<T>(GameObject Get_child, string child_name) where T : Component
    {
        Transform[] transforms_child = Get_child.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms_child)
        {
            if (tra.gameObject.name == child_name)
            {
                return tra.gameObject.GetComponent<T>();
                break;
            }
        }
        Debug.LogWarning("cant find child component");
        return null;
    }



    
}
