using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameCondition 
{
    Run,
    Win,
    Fail
}

public class VictoryCondition: MonoBehaviour
{
    public GameCondition GameCondition = GameCondition.Run;

    public GameObject WinTrigger; // ÖÕµã¼ì²âÌå
    public GameObject FailTrigger; // Ê§°Ü¼ì²âÌå

	[SerializeField]
    private float gameTime = 0;

    private int musicBool1 = 0;
    private int musicBool2 = 0;
    private int musicBool3 = 0;

    private bool gameUIGet = false;

    public GameObject newUI;
    private RectTransform rectTransform;

    private float returnMenu = 0f;

   

    private void Start()
	{
        UnitAction.oldPlayerMoveDirect = PlayerMoveDirect.Forward;

        UnitAction.isBlock = new bool[] { false, false, false, false };
        UnitAction.isLock = false;
        UnitAction.blockTrigger = Trigger.other;
}

	private void Update()
	{
        gameTime += Time.deltaTime;
        GameStart();
        GameConditionUpdate(GameCondition);
    }

    private void GameConditionUpdate(GameCondition gameCondition) 
    {
        switch (gameCondition)
        {
            case GameCondition.Win:
                GameWin();
                break;

            case GameCondition.Fail:
                GameFail();
                break;
        }
    }

    private void GameStart() 
    {
        if (gameTime >= 0f && musicBool1 ==0) 
        {
            musicBool1 = 1;
        }

        if (gameTime >= 125f && musicBool2 == 0)
        {
            musicBool2 = 1;
        }

        if (gameTime >= 230f && musicBool3 == 0)
        {
            musicBool3 = 1;
        }

        if (musicBool1 == 1)
		{
            GameAudioManager.Instance().PlayMusic(0);
			musicBool1 = 2;
		}

        if (musicBool2 == 1)
        {
            GameAudioManager.Instance().PlayMusic(1);
            musicBool2 = 2;
        }

        if (musicBool3 == 1)
        {
            GameAudioManager.Instance().PlayMusic(2);
            musicBool3 = 2;
        }

        if (gameTime >= 330f) 
        {
            gameTime = 0;
            musicBool1 = 0;
            musicBool2 = 0;
            musicBool3 = 0;
        }
    }

    private void GameFail() 
    {
        returnMenu +=Time.deltaTime;

        if (gameUIGet == false) 
        {
            //gameUIGet = true;
            //GameObject UICanvas = GameObject.Find("Canvas");


            //newUI = UIManager.GetInstance().GetSingleObject("UI/FailUI");
            //newUI.transform.SetParent(UICanvas.transform);
            //rectTransform = newUI.GetComponent<RectTransform>();
            SceneManager.LoadScene(0);
            UIManager.GetInstance().Pop(true);
            UIManager.GetInstance().Push(new StartPanel());

        }

        //if (gameUIGet ==true) 
        //{
        //    rectTransform.anchoredPosition = new Vector2(0,0);
        //}

        //if (returnMenu > 3f) 
        //{
        //    SceneManager.LoadScene(0);
        //    UIManager.GetInstance().Pop(true);
        //    UIManager.GetInstance().Push(new StartPanel());
        //}
    }

    private void GameWin() 
    {
        returnMenu += Time.deltaTime;
        SceneManager.LoadScene(0);
        UIManager.GetInstance().Pop(true);
        Debug.Log("Game Win!");

        //if (returnMenu > 3f)
        //{
        //    SceneManager.LoadScene(0);
        //}
    }
}
