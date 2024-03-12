using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mouse;
    public Button[] buttons;

    public bool canClickButton = true;
    Scene m_Scene;
    Scene f_Scene;

    int numOfButton;

    public CharacterDeorate characterDeorate;
    public SelectPlayer selectPlayer;

    public DBUtils dBUtils;

    Player player;

    public string nowSelectChoice;

    public CircleDrawer circleDrawer;

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
            player = dBUtils.GetPlayerByName(dBUtils.nowPlayer);
            numOfButton = 3;
        }
        else if (m_Scene.name == "CharacterDecorate")
        {
            numOfButton = 7;
            characterDeorate = GameObject.Find("Manager").GetComponent<CharacterDeorate>();
            player = dBUtils.GetPlayerByName(dBUtils.nowPlayer);
        }
        else if (m_Scene.name == "SelectPart")
        {
            numOfButton = 6;
            player = dBUtils.GetPlayerByName(dBUtils.nowPlayer);
        }
        else if (m_Scene.name == "Investigation")
        {
            player = dBUtils.GetPlayerByName(dBUtils.nowPlayer);
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
    private void ButtonClick(Button btn)
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
                dBUtils.AddPlayer();
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
                player.Gender = "Girl";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn2")
            {
                player.Gender = "Boy";
                nowSelectChoice = btn.name;
            }
            else if (btn.name == "Btn3")
            {
                dBUtils.UpdatePlayer(player);
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
                    player.Hair = ColorUtility.ToHtmlStringRGBA(color);
                }
                else if (characterDeorate.state == 1)
                {
                    player.Body = ColorUtility.ToHtmlStringRGBA(color);
                }
                else
                {
                    player.Cloth = ColorUtility.ToHtmlStringRGBA(color);
                }
                
            }
            else if (btn.name == "Btn7")
            {
                dBUtils.UpdatePlayer(player);
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
        else if (m_Scene.name == "SelectPart")
        {
            if (btn.name == "Btn1")
            {
                player.PreferPart = "Arms";

            }
            else if (btn.name == "Btn2")
            {
                player.PreferPart = "Abs";
            }
            else if (btn.name == "Btn3")
            {
                player.PreferPart = "Buttocks";
            }
            else if (btn.name == "Btn4")
            {
                player.PreferPart = "Legs";
            }
            else if (btn.name == "Btn5")
            {
                player.PreferPart = "Whole Body";
            }
            else if (btn.name == "Btn6")
            {
                SceneManager.LoadScene(8);
                dBUtils.UpdatePlayer(player);
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
                player.Level = "Easy";
            }
            else if (btn.name == "Btn2")
            {
                player.Level = "Medium";
            }
            else if (btn.name == "Btn3")
            {
                player.Level = "Hard";
            }
            else if (btn.name == "Btn4")
            {
                dBUtils.UpdatePlayer(player);
                SceneManager.LoadScene(9);
            }
            if (btn.name != "Btn4")
            {
                nowSelectChoice = btn.name;
            }
        }


    }

    public void Check_if_button(int type)
    {
        foreach (Button btn in buttons)
        {
            if (Check_touch_button(btn))
            {
                // 只是hover ，沒有點擊
                if (type == 0)
                {
                    // 放大 button
                    btn.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                    
                }

                //點擊
                else
                {
                    ButtonClick(btn);
                    canClickButton = false;
                    circleDrawer.circleImage.GetComponent<Image>().fillAmount = 0f;
                    StartCoroutine(DelayedAction());

                }

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
        if (mouse.transform.position.x > button_info[0] - button_info[2]/2 && mouse.transform.position.x < button_info[0] +button_info[2] / 2 && mouse.transform.position.y > button_info[1] - button_info[3] / 2 && mouse.transform.position.y < button_info[1] + button_info[3] / 2)
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

    // 不可連續點擊 button 
    private IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(1);

        canClickButton = true;
    }
}
