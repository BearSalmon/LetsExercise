using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public GameObject mouse;
    public Button[] buttons;

    Scene m_Scene;
    Scene f_Scene;

    int numOfButton;


    // imported components
    public CharacterDeorate characterDeorate;
    public SelectPlayer selectPlayer;
    public CircleDrawer circleDrawer;
    public DBUtils dBUtils;
    public TrainPageUI trainPageUI;
    public MainPageSetUp mainPageSetUp;
    public SelectLevel selectLevel;
    public InquireDataPageUI inquireDataPageUI;

    User user;

    public string nowSelectChoice; // for button

    public int poseSetID;

    public Button currentClickingButton;

    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        f_Scene = SceneManager.GetActiveScene();
        SetButtonList();
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


    // 每次 change scene 後都會呼叫 ， 重新設定 button list
    void SetButtonList()
    {
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
            numOfButton = 3;
        }
        else if (m_Scene.name == "CharacterDecorate")
        {
            numOfButton = 7;
            characterDeorate = GameObject.Find("Manager").GetComponent<CharacterDeorate>();
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        }
        else if (m_Scene.name == "InquireData")
        {
            numOfButton = 3;
            inquireDataPageUI = GameObject.Find("Manager").GetComponent<InquireDataPageUI>();
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        }
        else if (m_Scene.name == "SelectPart")
        {
            numOfButton = 6;
            user = dBUtils.GetUserByName(dBUtils.nowPlayer);
        }
        else if (m_Scene.name == "Investigation")
        {
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
            if (mainPageSetUp.nowState == 0)
            {
                numOfButton = 3;
            }
            else if (mainPageSetUp.nowState == 1)
            {
                numOfButton = 3;
            }
            else
            {
                numOfButton = 6;
            }
        }
        else if (m_Scene.name == "SelectLevel")
        {
            selectLevel = GameObject.Find("Manager").GetComponent<SelectLevel>();
            numOfButton = 4;
        }


        mouse = GameObject.Find("Mouse");
        dBUtils = GetComponent<DBUtils>();
        circleDrawer = GetComponent<CircleDrawer>();
        nowSelectChoice = "";

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
        if ( m_Scene.name == "GameStart")
        {
            if (btn.name == "Btn1")
            {
                SceneManager.LoadScene(1);
            }

        }
        else if (m_Scene.name == "CheckIfNew")
        {
            if (btn.name == "Btn1")
            {
                dBUtils.AddUser();
                SceneManager.LoadScene(3);
            }
            else if (btn.name == "Btn2")
            {
                SceneManager.LoadScene(2);
            }

        }
        else if (m_Scene.name == "SelectPlayer")
        {
            if (btn.name == "Btn1")
            {
                selectPlayer.Save();
                SceneManager.LoadScene(9);
            }
            else if (btn.name == "Btn2")
            {
                selectPlayer.backOption();
            }
            else if (btn.name == "Btn3")
            {
                selectPlayer.nextOption();
            }

        }

        else if (m_Scene.name == "Trainer")
        {
            if (btn.name == "Btn1")
            {
                SceneManager.LoadScene(5);
            }

        }
        else if (m_Scene.name == "SelectSex")
        {
            if (btn.name == "Btn1")
            {
                user.Gender = "Girl";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn2")
            {
                user.Gender = "Boy";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn3")
            {
                dBUtils.UpdateUser(user);
                SceneManager.LoadScene(6);
            }

        }
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
                    SceneManager.LoadScene(7);
                }
                    
            }
            
        }
        else if (m_Scene.name == "InquireData")
        {
            if (btn.name == "Btn3" && inquireDataPageUI.state != 2)
            {
          
                inquireDataPageUI.state += 1;
                inquireDataPageUI.ChangeSetUp();
                SetButtonList();

            }
            else if (btn.name == "Btn3" && inquireDataPageUI.state == 2)
            {
                user.Age = inquireDataPageUI.age_num;
                user.Height = inquireDataPageUI.height_num;
                user.Weight += inquireDataPageUI.weight + ",";

                dBUtils.UpdateUser(user);
                SceneManager.LoadScene(8);
            }
            else if (btn.name == "Btn1")
            {
                inquireDataPageUI.Increase();
            }
            else if (btn.name == "Btn2")
            {
                inquireDataPageUI.Decrease();
            }

            
        }
        else if (m_Scene.name == "SelectPart")
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
            else if (btn.name == "Btn6")
            {
                SceneManager.LoadScene(8);
                dBUtils.UpdateUser(user);
            }

            if (btn.name != "Btn6")
            {
                nowSelectChoice = btn.name;
            }

        }
        else if (m_Scene.name == "Investigation")
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
            else if (btn.name == "Btn4")
            {
                dBUtils.UpdateUser(user);
                SceneManager.LoadScene(9);
            }
            if (btn.name != "Btn4")
            {
                nowSelectChoice = btn.name;
            }
        }
        else if (m_Scene.name == "MainPage")
        {
            if (btn.name == "Btn1")
            {
                mainPageSetUp.nowState = 1;
                mainPageSetUp.ChangeState(1);
                SetButtonList();

            }
            else if (btn.name == "Btn2")
            {
                mainPageSetUp.nowState = 2;
                mainPageSetUp.ChangeState(2);
                SetButtonList();
            }
            else if (btn.name == "Btn3")
            {
                mainPageSetUp.nowState = 0;
                mainPageSetUp.ChangeState(0);
                SetButtonList();
            }

            // user page
            if (mainPageSetUp.nowState == 0)
            {

            }
            // plan page
            else if (mainPageSetUp.nowState == 1)
            {

            }
            // train page
            else
            {
                if (btn.name == "Btn4")
                {
                    SceneManager.LoadScene(10);
                }
                else if (btn.name == "Btn5")
                {
                    poseSetID = trainPageUI.nextOption();
                }
                else if (btn.name == "Btn6")
                {
                    poseSetID = trainPageUI.backOption();
                }
            }
            
        }
        else if (m_Scene.name == "SelectLevel")
        {
            if (btn.name == "Btn1")
            {
                selectLevel.updateLevel("easy");
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn2")
            {
                selectLevel.updateLevel("medium");
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn3")
            {
                selectLevel.updateLevel("hard");
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn4")
            {

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
