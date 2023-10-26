using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl 
{
    private static SceneControl instance;
    public static SceneControl GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("no instance (scenescontrl)");
            return instance;
        }
        return instance;
    }

    public Dictionary<string, SceneBase> dict_scene;

    public SceneControl()
    {
        instance = this;
        dict_scene = new Dictionary<string, SceneBase>();
    }

    public void ScenesLoad(string scene_name, SceneBase scene)
    {
        if (!dict_scene.ContainsKey(scene_name))
        {
            dict_scene.Add(scene_name, scene);
        }
        if (dict_scene.ContainsKey(SceneManager.GetActiveScene().name))
        {
            dict_scene[SceneManager.GetActiveScene().name].ExitScene();
        }
        if (scene_name == "Scene1")
        {
            GameRoot.GetInstance().SceneNumber = 1;
        }
        if (scene_name == "Scene3")
        {
            GameRoot.GetInstance().SceneNumber = 3;
        }
        SceneManager.LoadScene(scene_name);
        GameRoot.GetInstance().uIMannger.Pop(true);
        scene.EnterScene();
    }
}
