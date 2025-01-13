using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;  // �� ��¥�� ǥ���� ������
    [SerializeField] private Transform gridParent;   // Grid Layout Group�� ���� �θ� ��ü
    [SerializeField] private Color[] activityColors; // Ȱ������ ���� (4�ܰ�)
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
        // PlayerPrefs���� ������ �ε� �Ǵ� ���� ����
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

            // ��¥ ������ �������� ǥ���Ϸ��� �߰� UI ��� �ʿ�
            //AddDateTooltip(cell, habit);
        }
    }

    private void AddDateTooltip(GameObject cell, DailyHabit habit)
    {
        // ���� UI ���� (���콺 ������ ǥ��)
        var tooltip = cell.AddComponent<TooltipTrigger>();
        tooltip.tooltipText = $"{habit.date}: {habit.count}ȸ ����";
    }

    // Ư�� ��¥�� Ȱ���� ������Ʈ
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