using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private UIManager uimannger;
    private SceneControl scenesControl;


    public UIManager uIMannger { get => uimannger; }
    public SceneControl scenescontrol { get => scenesControl; }


    private static GameRoot instance;

    [Header("开始面板")]
    public StartPanel startPanel;
    [Header("开始场景")]
    public StartScene startScene;
    [Header("场景画布")]
    public Canvas canvas;

    /// <summary>
    /// SceneNumber=1 Scene1;SceneNumber =2,Scene;
    /// </summary>
    public int SceneNumber;
    public static GameRoot GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("no instance gameroot");
            return instance;
        }
        return instance;
    }


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        uimannger = new UIManager();
        scenesControl = new SceneControl();
    }




    private void Start()
    {
        startPanel = new StartPanel();
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        startScene = new StartScene();
        scenescontrol.dict_scene.Add(startScene.StartScnenName, startScene);
        uIMannger.Canvas_Obj = UIMethod.GetInstance().FindCanvas();
        uIMannger.Push(startPanel);
        
    }

    private void Update()
    {

    }
}
