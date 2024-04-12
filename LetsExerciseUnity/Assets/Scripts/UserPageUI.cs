using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class UserPageUI : MonoBehaviour
{
    public RectTransform chartContainer;
    public GameObject pointPrefab;
    public Color lineColor = Color.blue;

    DBUtils dBUtils;
    public GameObject girl;
    public GameObject boy;

    public RawImage hair_girl;
    public RawImage body_girl;
    public RawImage cloth_girl;

    public RawImage hair_boy;
    public RawImage body_boy;
    public RawImage cloth_boy;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI duration;
    public TextMeshProUGUI calories;
    public TextMeshProUGUI curWeight;
    public TextMeshProUGUI BMI;

    User user;
    string[] dataValues;

    // Start is called before the first frame update
    void Start()
    {
        dBUtils = GameObject.Find("WholeManager").GetComponent<DBUtils>();
        
        user = dBUtils.GetUserByName(dBUtils.nowPlayer);

        if (user.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
        }
        else
        {
            girl.SetActive(false);
            boy.SetActive(true);
        }
        UserSetUp();

        List<int> dataPoints = new List<int>();
        dataValues = user.Weight.Split(',');

        foreach (string value in dataValues)
        {
            int dataPoint;
            if (int.TryParse(value, out dataPoint))
            {
                dataPoints.Add(dataPoint);
            }
        }
        DrawLineChart(dataPoints);
        curWeight.text = dataValues[dataValues.Length - 1];
        duration.text = user.Duration.ToString();
        calories.text = user.Calories.ToString();
        int weight = int.Parse(dataValues[dataValues.Length - 1]);
        float heightInMeters = user.Height / 100f;
        float bmi = weight / (heightInMeters * heightInMeters);
        BMI.text = bmi.ToString("F1");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UserSetUp()
    {
        playerName.text = user.Name;
        if (user.Gender == "Girl")
        {
            girl.SetActive(true);
            boy.SetActive(false);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Hair, out color);
            hair_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Body, out color);
            body_girl.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Cloth, out color);
            cloth_girl.color = color;
        }
        else
        {
            girl.SetActive(false);
            boy.SetActive(true);
            Color color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Hair, out color);
            hair_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Body, out color);
            body_boy.color = color;
            ColorUtility.TryParseHtmlString("#".ToString() + user.Cloth, out color);
            cloth_boy.color = color;
        }

    }

    void DrawLineChart(List<int> dataPoints) 
    {
        float chartHeight = chartContainer.rect.height;
        float fixedChartWidth = 500f; // 假设为 500f

        if (dataPoints.Count == 1)
        {
            float yMax = Mathf.Max(dataPoints.ToArray());
            Vector2 startPoint = new Vector2(0, dataPoints[0] / yMax * chartHeight * 5);
            Vector2 endPoint = new Vector2(fixedChartWidth, dataPoints[0] / yMax * chartHeight * 5);
            DrawLine(startPoint, endPoint);
        }
        else
        {
            float xStep = fixedChartWidth / (dataPoints.Count - 1);
            float yMax = Mathf.Max(dataPoints.ToArray());

            Vector2 startPoint = new Vector2(0, dataPoints[0] / yMax * chartHeight * 5);
            for (int i = 1; i < dataPoints.Count; i++)
            {
                Vector2 endPoint = new Vector2(i * xStep, dataPoints[i] / yMax * chartHeight * 5);
                DrawLine(startPoint, endPoint);
                startPoint = endPoint;
            }
        }
        
    }

    void DrawLine(Vector2 startPoint, Vector2 endPoint)
    {
        GameObject line = new GameObject("Line", typeof(Image));
        line.transform.SetParent(chartContainer, false);
        Image lineImage = line.GetComponent<Image>();
        lineImage.color = lineColor;

        RectTransform lineRect = line.GetComponent<RectTransform>();
        Vector2 direction = endPoint - startPoint;
        float distance = direction.magnitude;
        lineRect.sizeDelta = new Vector2(distance, 10f);
        lineRect.anchoredPosition = startPoint + direction / 2f;
        lineRect.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

    }

}
