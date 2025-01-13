using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;  // 각 날짜를 표시할 프리팹
    [SerializeField] private Transform gridParent;   // Grid Layout Group을 가진 부모 객체
    [SerializeField] private Color[] activityColors; // 활동량별 색상 (4단계)
    [SerializeField] private Button createButton;

    private HabitData habitData;
    private Dictionary<string, Image> dateCells = new Dictionary<string, Image>();

    private void Awake()
    {
        createButton.onClick.AddListener(CreateHabbit);
    }

    void Start()
    {
        LoadOrCreateHabitData();
    }

    private void CreateHabbit()
    {
        habitData = GenerateEmptyData(30);
        SaveHabitData();
    }

    private void LoadOrCreateHabitData()
    {
        // PlayerPrefs에서 데이터 로드 또는 새로 생성
        string savedData = PlayerPrefs.GetString("HabitData", "");
        if (string.IsNullOrEmpty(savedData) == false)
        {
            habitData = JsonUtility.FromJson<HabitData>(savedData);
            LoadHabit();
        }
    }

    private HabitData GenerateEmptyData(int targetDay)
    {
        HabitData data = new HabitData();
        DateTime today = DateTime.Today;

        for (int i = 364; i >= 0; i--)
        {
            DateTime date = today.AddDays(-i);
            data.habits.Add(new DailyHabit
            {
                date = date.ToString("yyyy-MM-dd"),
                count = 0
            });
        }

        return data;
    }

    public GameObject Daliy30Habbit;
    public GameObject Daliy60Habbit;
    public GameObject Daliy90Habbit;

    private void LoadHabit()
    {
        var list =  habitData.targetDate switch
        {
            30 => Daliy30Habbit,
            60 => Daliy60Habbit,
            90 => Daliy90Habbit,
            _ => throw new ArgumentException("Invalid target date")  // default cas
        };

        foreach (var habit in habitData.habits)
        {
            /*GameObject cell = Instantiate(cellPrefab, gridParent);
            Image cellImage = cell.GetComponent<Image>();

            cellImage.color = activityColors[habit.count];
            dateCells[habit.date] = cellImage;*/

            // 날짜 정보를 툴팁으로 표시하려면 추가 UI 요소 필요
            //AddDateTooltip(cell, habit);
        }
    }

    private void AddDateTooltip(GameObject cell, DailyHabit habit)
    {
        // 툴팁 UI 구현 (마우스 오버시 표시)
        var tooltip = cell.AddComponent<TooltipTrigger>();
        tooltip.tooltipText = $"{habit.date}: {habit.count}회 수행";
    }

    // 특정 날짜의 활동량 업데이트
    public void UpdateActivity(string date, int count)
    {
        var habit = habitData.habits.Find(h => h.date == date);
        if (habit != null)
        {
            habit.count = Mathf.Clamp(count, 0, 3);
            if (dateCells.ContainsKey(date))
            {
                dateCells[date].color = activityColors[habit.count];
            }
            SaveHabitData();
        }
    }

    private void SaveHabitData()
    {
        string json = JsonUtility.ToJson(habitData);
        PlayerPrefs.SetString("HabitData", json);
        PlayerPrefs.Save();
    }
}