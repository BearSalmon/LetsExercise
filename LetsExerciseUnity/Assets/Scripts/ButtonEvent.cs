using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using System.Runtime.InteropServices;

public class ButtonEvent : MonoBehaviour
{
    public enum SceneName
    {
        GameStart = 0,
        Trainer = 1,
        Intro = 2,
        CheckIfNew = 3,
        SelectPlayer = 4,
        SelectSex = 5,
        CharacterDecorate = 6,
        InquireData = 7,
        SelectPart = 8,
        Investigation = 9,
        Trainer2 = 10,
        MainPage = 11,
        SelectLevel = 12,
        SampleScene = 13,
        ExitGame = 14,
        Calendar = 15
    }
    public GameObject mouse;
    public Button[] buttons;

    Scene m_Scene;
    Scene f_Scene;

    int numOfButton;

    public AudioManager audioManager;

    // imported components
    public CharacterDeorate characterDeorate;
    public SelectPlayer selectPlayer;
    public CircleDrawer circleDrawer;
    public DBUtils dBUtils;
    public TrainPageUI trainPageUI;
    public MainPageSetUp mainPageSetUp;
    public SelectLevel selectLevel;
    public InquireDataPageUI inquireDataPageUI;
    public SelectPartSet selectPartSet;
    public InvestigateSet investigateSet;
    public ExitGameUI exitGameUI;
    public CalendarUI calendarUI;

    public bool isAddingWeight;
    public bool isChangingColor;

    User user;

    public string nowSelectChoice; // for button

    // for select train pose set
    public int poseSetID;
    public string poseSetLevel; 

    // 
    public Button currentClickingButton;

    // plan = 0 ; train = 1; 
    public int planOrTrain = 0;

    // for test (click button count)
    int count = 0;


    void Start()
    {
        isAddingWeight = false;
        isChangingColor = false;
        numOfButton = 1;
        m_Scene = SceneManager.GetActiveScene();
        f_Scene = SceneManager.GetActiveScene();
        SetButtonList();
        poseSetID = 1;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Scene = SceneManager.GetActiveScene();

        if (m_Scene.buildIndex != f_Scene.buildIndex)
        {
            
            SetButtonList();
        }
        f_Scene = SceneManager.GetActiveScene();

    }

    void RemoveListener()
    {
        for (var i = 1; i <= numOfButton; i++)
        {
            buttons[i - 1].onClick.RemoveAllListeners();
        }
    }


    // 每次 change scene 後都會呼叫 ， 重新設定 button list
    void SetButtonList()
    {
        RemoveListener();

        mouse = GameObject.Find("Mouse");
        dBUtils = GetComponent<DBUtils>();
        circleDrawer = GetComponent<CircleDrawer>();
        nowSelectChoice = "";

        if (m_Scene.name == "GameStart")
        {
            numOfButton = 1;
        }
        else if (m_Scene.name == "CheckIfNew")
        {
            numOfButton = 2;
        }
        else if (m_Scene.name == "SelectPlayer")
        {
            numOfButton = 3;
            selectPlayer = GameObject.Find("Manager").GetComponent<SelectPlayer>();
        }
        else if (m_Scene.name == "Trainer")
        {
            numOfButton = 1;
        }
        else if (m_Scene.name == "Intro")
        {
            numOfButton = 0;
        }
        else if (m_Scene.name == "SelectSex")
        {
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
            numOfButton = 2;
        }
        else if (m_Scene.name == "CharacterDecorate")
        {
            numOfButton = 8;
            characterDeorate = GameObject.Find("Manager").GetComponent<CharacterDeorate>();
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        }
        else if (m_Scene.name == "InquireData")
        {
            inquireDataPageUI = GameObject.Find("Manager").GetComponent<InquireDataPageUI>();
            if (inquireDataPageUI.state == 0)
            {
                numOfButton = 2;
            }
            else
            {
                numOfButton = 4;
            }
     
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        }
        else if (m_Scene.name == "SelectPart")
        {
            numOfButton = 6;
            selectPartSet = GameObject.Find("Manager").GetComponent<SelectPartSet>(); 
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        }
        else if (m_Scene.name == "Investigation")
        {
            investigateSet = GameObject.Find("Manager").GetComponent<InvestigateSet>();
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
            numOfButton = 4;
        }
        else if (m_Scene.name == "Trainer2")
        {
            numOfButton = 0;
        }
        else if (m_Scene.name == "MainPage")
        {
            trainPageUI = GameObject.Find("Manager").GetComponent<TrainPageUI>();
            mainPageSetUp = GameObject.Find("Manager").GetComponent<MainPageSetUp>();
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
            if (mainPageSetUp.isOpening == false) // menu is not opening
            {
                if (mainPageSetUp.nowState == 0) // user page 
                {
                    numOfButton = 6;
                }
                else if (mainPageSetUp.nowState == 1) // plan page
                {
                    numOfButton = 6;
                }
                else if (mainPageSetUp.nowState == 2) // train page
                {
                    numOfButton = 7;
                }
            }
            else
            {
                if (mainPageSetUp.nowState == 0) // user page 
                {
                    numOfButton = 10;
                }
                else if (mainPageSetUp.nowState == 1) // plan page
                {
                    numOfButton = 10;
                }
                else if (mainPageSetUp.nowState == 2) // train page
                {
                    numOfButton = 11;
                }
            }
        }
        else if (m_Scene.name == "SelectLevel")
        {
            selectLevel = GameObject.Find("Manager").GetComponent<SelectLevel>();
            numOfButton = 4;
        }
        else if (m_Scene.name == "SampleScene")
        {
            numOfButton = 0;
        }

        else if (m_Scene.name == "ExitGame")
        {
            exitGameUI = GameObject.Find("Manager").GetComponent<ExitGameUI>();
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
            if (exitGameUI.nowState == 0)
            {
                numOfButton = 1;
            }
            else if (exitGameUI.nowState == 1)
            {
                numOfButton = 3;
            }
            else if (exitGameUI.nowState == 2)
            {
                numOfButton = 5;
            }
            else
            {
                numOfButton = 1;
            }
        }
        else if (m_Scene.name == "Calendar")
        {
            calendarUI = GameObject.Find("Manager").GetComponent<CalendarUI>();

            if (calendarUI.detailIsOpen == true)
            {
                numOfButton = 46;
            }
            else
            {
                numOfButton = 45;
            }
        }


        
        buttons = new Button[numOfButton];
        for (var i = 1; i <= numOfButton; i++)
        {
            Button btn = GameObject.Find("Btn" + i).GetComponent<Button>();

            // 每個 button 新增鼠標點擊功能
            btn.onClick.AddListener(() => ButtonClick(btn));

            buttons[i-1] = btn; 
        }

    }




    // 所有關於點擊 button 後的動作統一寫在這邊
    // button 命名格式 : Btn + 編號
    public void ButtonClick(Button btn)
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        count++;
        //Debug.Log(count);
        // Game Start
        if (m_Scene.name == "GameStart")
        {
            if (btn.name == "Btn1")
            {
                SceneManager.LoadScene((int)SceneName.Trainer);
                audioManager.TrainerSpeak(audioManager.trainerIntroduce);
            }

        }
        // Check If New User 
        else if (m_Scene.name == "CheckIfNew")
        {
            if (btn.name == "Btn1")
            {
                dBUtils.AddUser();
                SceneManager.LoadScene((int)SceneName.SelectSex);
            }
            else if (btn.name == "Btn2")
            {
                isAddingWeight = true;
                isChangingColor = true;
                SceneManager.LoadScene((int)SceneName.SelectPlayer);
                audioManager.TrainerSpeak(audioManager.selectOldUser);
            }

        }
        // Select User 
        else if (m_Scene.name == "SelectPlayer")
        {
            if (btn.name == "Btn1")
            {
                selectPlayer.Save();
                SceneManager.LoadScene((int)SceneName.MainPage);
            }
            else if (btn.name == "Btn2")
            {
                SceneManager.LoadScene((int)SceneName.CheckIfNew);
            }
            else if (btn.name == "Btn3")
            {
                selectPlayer.nextOption();
            }

        }
        // Trainer 1 
        else if (m_Scene.name == "Trainer")
        {
            if (btn.name == "Btn1")
            {
                SceneManager.LoadScene((int)SceneName.CheckIfNew);
                audioManager.TrainerSpeak(audioManager.askIfNewUser);
            }

        }
        // Select Sex 
        else if (m_Scene.name == "SelectSex")
        {
            if (btn.name == "Btn1")
            {
                user.Gender = "Girl";
            }
            else if (btn.name == "Btn2")
            {
                user.Gender = "Boy";
            }
            dBUtils.UpdateUser(user);
            SceneManager.LoadScene((int)SceneName.CharacterDecorate);

        }
        // Character Decorate 
        else if (m_Scene.name == "CharacterDecorate")
        {
            if (btn.name == "Btn1" || btn.name == "Btn2" || btn.name == "Btn3" || btn.name == "Btn4" || btn.name == "Btn5" || btn.name == "Btn6")
            {
                Color color = btn.GetComponent<Image>().color;
                characterDeorate.changeColor(color);
                if (characterDeorate.state == 0)
                {
                    user.Hair = ColorUtility.ToHtmlStringRGBA(color);
                }
                else if (characterDeorate.state == 1)
                {
                    user.Body = ColorUtility.ToHtmlStringRGBA(color);
                }
                else
                {
                    user.Cloth = ColorUtility.ToHtmlStringRGBA(color);
                }

            }
            else if (btn.name == "Btn7")
            {
                dBUtils.UpdateUser(user);
                if (characterDeorate.state == 0 || characterDeorate.state == 1)
                {
                    characterDeorate.state += 1;
                    characterDeorate.changeState();
                    SetButtonList();
                }
                else
                {
                    if (isChangingColor == false)
                    {
                        isChangingColor = true;
                        SceneManager.LoadScene((int)SceneName.InquireData);
                    }
                    else
                    {
                        SceneManager.LoadScene((int)SceneName.MainPage);

                    }

                }

            }
            else if (btn.name == "Btn8")
            {
                SceneManager.LoadScene((int)SceneName.SelectSex);
            }

        }
        // Inquire Data (Age , Weight , Height)
        else if (m_Scene.name == "InquireData")
        {
            if (isAddingWeight == false)
            {
                if (btn.name == "Btn3" && inquireDataPageUI.state != 3)
                {
                    inquireDataPageUI.state += 1;
                    inquireDataPageUI.ChangeSetUp();
                    SetButtonList();

                }
                else if (btn.name == "Btn3" && inquireDataPageUI.state == 3)
                {
                    user.Age = inquireDataPageUI.age_num;
                    user.Height = inquireDataPageUI.height_num;
                    user.Weight = inquireDataPageUI.weight.text + ",";

                    dBUtils.UpdateUser(user);
                    inquireDataPageUI.state = 0;

                    SceneManager.LoadScene((int)SceneName.SelectPart);
                }
                if (btn.name == "Btn4")
                {
                    inquireDataPageUI.state -= 1;
                    inquireDataPageUI.ChangeSetUp();
                    SetButtonList();
                }

            }
            else
            {
                if (btn.name == "Btn3")
                {
                    user.Weight += inquireDataPageUI.weight.text + ",";
                    dBUtils.UpdateUser(user);
                    SceneManager.LoadScene((int)SceneName.MainPage);
                }
                else if (btn.name == "Btn4")
                {
                    SceneManager.LoadScene((int)SceneName.MainPage);
                }
            }

            if (btn.name == "Btn1")
            {
                if (inquireDataPageUI.state == 0)
                {
                    inquireDataPageUI.state += 1;
                    inquireDataPageUI.ChangeSetUp();
                    SetButtonList();
                }
                else
                {
                    inquireDataPageUI.Increase();
                }

            }
            else if (btn.name == "Btn2")
            {
                if (inquireDataPageUI.state == 0)
                {
                    SceneManager.LoadScene((int)SceneName.CharacterDecorate);
                }
                else
                {
                    inquireDataPageUI.Decrease();
                }

            }


        }
        // Select Prefer Part 
        else if (m_Scene.name == "SelectPart")
        {
            if (btn.name == "Btn6")
            {
                SceneManager.LoadScene((int)SceneName.InquireData);
            }
            else
            {
                if (btn.name == "Btn1")
                {
                    user.PreferPart = "Arms";
                }
                else if (btn.name == "Btn2")
                {
                    user.PreferPart = "Abs";
                }
                else if (btn.name == "Btn3")
                {
                    user.PreferPart = "Buttocks";
                }
                else if (btn.name == "Btn4")
                {
                    user.PreferPart = "Legs";
                }
                else if (btn.name == "Btn5")
                {
                    user.PreferPart = "Whole Body";
                }

                user.Recommendation = selectPartSet.SetRecommandation(user.PreferPart);
                SceneManager.LoadScene((int)SceneName.Investigation);
                dBUtils.UpdateUser(user);
            }

        }
        // Investidation ( the level of user )
        else if (m_Scene.name == "Investigation")
        {
            if (btn.name == "Btn4")
            {
                SceneManager.LoadScene((int)SceneName.SelectPart);
            }
            else
            {
                if (btn.name == "Btn1")
                {
                    user.Level = "Easy";
                }
                else if (btn.name == "Btn2")
                {
                    user.Level = "Medium";
                }
                else if (btn.name == "Btn3")
                {
                    user.Level = "Hard";
                }
                user.Recommendation = investigateSet.SetRecommandation(user.Level);
                dBUtils.UpdateUser(user);
                isAddingWeight = true;
                SceneManager.LoadScene((int)SceneName.Trainer2);
            }

        }
        else if (m_Scene.name == "MainPage")
        {
            if (btn.name == "Btn1")
            {
                mainPageSetUp.nowState = 1;
                mainPageSetUp.ChangeState(1);
                planOrTrain = 0;
                SetButtonList();

            }
            else if (btn.name == "Btn2")
            {
                mainPageSetUp.nowState = 2;
                mainPageSetUp.ChangeState(2);
                planOrTrain = 1;
                SetButtonList();
            }
            else if (btn.name == "Btn3")
            {
                mainPageSetUp.nowState = 0;
                mainPageSetUp.ChangeState(0);
                SetButtonList();
            }
            else if (btn.name == "Btn4")
            {
                SceneManager.LoadScene((int)SceneName.Calendar);
            }
            else if (btn.name == "Btn5")
            {
                mainPageSetUp.SetMenu();
                SetButtonList();
            }

            // user page
            if (mainPageSetUp.nowState == 0)
            {
                if (btn.name == "Btn6")
                {
                    SceneManager.LoadScene((int)SceneName.InquireData);
                }
                else if (btn.name == "Btn7")
                {
                    SceneManager.LoadScene((int)SceneName.CharacterDecorate);
                }
                else if (btn.name == "Btn8")
                {
                    SceneManager.LoadScene((int)SceneName.SelectPlayer);
                }
                else if (btn.name == "Btn9")
                {
                    
                }
                else if (btn.name == "Btn10")
                {
                    SceneManager.LoadScene((int)SceneName.ExitGame);
                }

            }
            // plan page
            else if (mainPageSetUp.nowState == 1)
            {
                if (btn.name == "Btn6")
                {
                    SceneManager.LoadScene((int)SceneName.SampleScene);
                }
                else if (btn.name == "Btn7")
                {
                    SceneManager.LoadScene((int)SceneName.CharacterDecorate);
                }
                else if (btn.name == "Btn8")
                {
                    SceneManager.LoadScene((int)SceneName.SelectPlayer);
                }
                else if (btn.name == "Btn9")
                {
                }
                else if (btn.name == "Btn10")
                {
                    SceneManager.LoadScene((int)SceneName.ExitGame);
                }
            }
            // train page
            else if (mainPageSetUp.nowState == 2)
            {
                if (btn.name == "Btn6")
                {
                    SceneManager.LoadScene((int)SceneName.SelectLevel);
                }
                else if (btn.name == "Btn7")
                {
                    poseSetID = trainPageUI.nextOption();
                }
                else if (btn.name == "Btn8")
                {
                    SceneManager.LoadScene((int)SceneName.CharacterDecorate);
                }
                else if (btn.name == "Btn9")
                {
                    SceneManager.LoadScene((int)SceneName.SelectPlayer);
                }
                else if (btn.name == "Btn10")
                {
                }
                else if (btn.name == "Btn11")
                {
                    SceneManager.LoadScene((int)SceneName.ExitGame);
                }
            }

        }
        else if (m_Scene.name == "SelectLevel")
        {
            if (btn.name == "Btn1")
            {
                selectLevel.updateLevel("easy");
                poseSetLevel = "Easy";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn2")
            {
                selectLevel.updateLevel("medium");
                poseSetLevel = "Medium";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn3")
            {
                selectLevel.updateLevel("hard");
                poseSetLevel = "Hard";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn4")
            {
                SceneManager.LoadScene((int)SceneName.SampleScene);
            }

        }

        else if (m_Scene.name == "ExitGame")
        {
            if (exitGameUI.nowState == 0)
            {
                if (btn.name == "Btn1")
                {
                    exitGameUI.ChangeState(1);
                    SetButtonList();
                }
            }
            else if (exitGameUI.nowState == 1)
            {
                string level = "";
                if (btn.name == "Btn1")
                {
                    level = "Easy";
                }
                else if (btn.name == "Btn2")
                {
                    level = "Medium";
                }
                else if (btn.name == "Btn3")
                {
                    level = "Hard";
                }
                user.Recommendation = exitGameUI.SetRecommandation(level);
                dBUtils.UpdateUser(user);
                exitGameUI.ChangeState(2);
                SetButtonList();
            }
            else if (exitGameUI.nowState == 2)
            {
                Debug.Log(btn.GetComponentInChildren<TextMeshProUGUI>().text);
                user.Recommendation = exitGameUI.SetRecommandation(btn.GetComponentInChildren<TextMeshProUGUI>().text);
                dBUtils.UpdateUser(user);
                exitGameUI.ChangeState(3);
                SetButtonList();
            }
            else if (exitGameUI.nowState == 3)
            {
                if (btn.name == "Btn1")
                {
                    user.LastLogin = DateTime.Now.ToString();
                    dBUtils.UpdateUser(user);
                    Application.Quit();
                }
            }
        }
        else if (m_Scene.name == "Calendar")
        {
            if (btn.name == "Btn1")
            {
                calendarUI.SwitchMonth(1);
            }
            else if (btn.name == "Btn2")
            {
                calendarUI.SwitchMonth(-1);
            }
            else if (btn.name == "Btn45")
            {
                SceneManager.LoadScene((int)SceneName.MainPage);
            }
            else if (btn.name == "Btn46")
            {
                calendarUI.detailIsOpen = false;
                calendarUI.SetUpDetail("");
            }
            else
            {   
                // is valid day
                if (btn.GetComponentInChildren<TextMeshProUGUI>().text != "")
                {
                    calendarUI.detailIsOpen = true;
                    calendarUI.SetUpDetail(btn.GetComponentInChildren<TextMeshProUGUI>().text);
                    SetButtonList();
                }
                
            }
        }




    }

    public void Check_if_button()
    {
        foreach (Button btn in buttons)
        {
            // 若有碰到 button 
            if (Check_touch_button(btn))
            {
                // 一放到 button 上面就開始計時

                currentClickingButton = btn;

                btn.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                circleDrawer.CallDrawer();


                // 若為 CharacterDecorate 頁面 ，避免設定到選擇顏色的 button 
                if (m_Scene.name == "CharacterDecorate" && btn.name != "Btn7")
                {

                }
                else
                {
                    Color color;
                    ColorUtility.TryParseHtmlString("#FFD870", out color);
                    btn.GetComponent<Image>().color = color;

                }

                return;
            }
            else
            {
                if (currentClickingButton == btn)
                {
                    circleDrawer.StopIncreasing();
                }
                
                if (circleDrawer.circleImage == null)
                {
                    circleDrawer.circleImage = GameObject.Find("Circle").GetComponent<Image>();
                }
                circleDrawer.circleImage.GetComponent<Image>().fillAmount = 0f;
                btn.transform.localScale = new Vector3(1f, 1f, 1f);
                if (nowSelectChoice == btn.name) // 此 button 已點選 ，避免重置顏色
                {

                }
                else
                {
                    // 若為 CharacterDecorate 頁面 ，避免設定到選擇顏色的 button 
                    if (m_Scene.name == "CharacterDecorate" && btn.name != "Btn7") {
                        
                    }
                    else
                    {
                        Color color;
                        ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                        btn.GetComponent<Image>().color = color;

                    }

                }
                
            }
        }

    }

    // mouse 是否 touch button 
    bool Check_touch_button(Button btn)
    {
        float[] button_info = Get_button_info(btn);
        float buffer = 25f;
        if (mouse.transform.position.x > button_info[0] - button_info[2] / 2 + buffer
            && mouse.transform.position.x < button_info[0] + button_info[2] / 2 - buffer
            && mouse.transform.position.y > button_info[1] - button_info[3] / 2 +buffer
            && mouse.transform.position.y < button_info[1] + button_info[3] / 2 - buffer)
        {
            return true;
        }
        return false;
    }

    float[] Get_button_info(Button btn)
    {
        RectTransform buttonRectTransform = btn.GetComponent<RectTransform>();
        float[] button_info = { btn.transform.position.x, btn.transform.position.y , buttonRectTransform.sizeDelta.x , buttonRectTransform.sizeDelta.y };
        return button_info;
    }
}
