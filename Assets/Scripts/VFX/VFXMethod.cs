using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class VFXMethod
{
    private static VFXMethod instance;
    private static readonly object locker = new object();
    private VFXMethod() { }



    float timer = 0;
    public static VFXMethod GetInstance()
    {
        if (instance == null)
        {
            lock (locker)
            {
                if(instance == null)
                {
                    instance = new VFXMethod();
                }
            }
        }
        return instance;
    }



    //特效寻找
    public VisualEffect VFXFind(string name)
    {
        VisualEffect[] vfx = GameObject.FindObjectsOfType<VisualEffect>();

        foreach (VisualEffect effect in vfx)
        {
            if (effect.name == name)
            {
                return effect;
            }
        }
        return null;
    }


    //特效播放
    public void EffectOn(VisualEffect effect)
    {
        if (effect == null)
        {
            Debug.Log("未找到改特效，无法播放");
            return;
        }
        effect.Play();
        
    }


    //特效实体在特定地点播放
    public void AddEffect(GameObject effect, GameObject parent)
    {
        GameObject BoomObj = GameObject.Instantiate(Resources.Load("Prefabs/"+effect.name)) as GameObject;
        BoomObj.transform.position = parent.transform.position;
    }


    //特效停止
    public void PauseEffectOn(VisualEffect effect)
    {
        if (!effect)
        {
            Debug.Log("未找到特效");
            return;
        }
        effect.Stop();
    }



    //特效预制体删除
    public void DeleteEffect(GameObject effectObj)
    {
        
        GameObject.Destroy(effectObj);
    }

    //延迟执行协程
    IEnumerable DelayFuc()
    {
        while (timer < 30)
        {
            yield return new WaitForSeconds(1);
            timer++;
            Debug.Log("执行测试函数" + timer);
        }

    }
}
