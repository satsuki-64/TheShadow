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



    //��ЧѰ��
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


    //��Ч����
    public void EffectOn(VisualEffect effect)
    {
        if (effect == null)
        {
            Debug.Log("δ�ҵ�����Ч���޷�����");
            return;
        }
        effect.Play();
        
    }


    //��Чʵ�����ض��ص㲥��
    public void AddEffect(GameObject effect, GameObject parent)
    {
        GameObject BoomObj = GameObject.Instantiate(Resources.Load("Prefabs/"+effect.name)) as GameObject;
        BoomObj.transform.position = parent.transform.position;
    }


    //��Чֹͣ
    public void PauseEffectOn(VisualEffect effect)
    {
        if (!effect)
        {
            Debug.Log("δ�ҵ���Ч");
            return;
        }
        effect.Stop();
    }



    //��ЧԤ����ɾ��
    public void DeleteEffect(GameObject effectObj)
    {
        
        GameObject.Destroy(effectObj);
    }

    //�ӳ�ִ��Э��
    IEnumerable DelayFuc()
    {
        while (timer < 30)
        {
            yield return new WaitForSeconds(1);
            timer++;
            Debug.Log("ִ�в��Ժ���" + timer);
        }

    }
}
