using UnityEngine;

// HabitData.cs - ���� �����͸� ������ Ŭ����
[CreateAssetMenu(fileName = "DailyHabit", menuName = "Scriptable Objects/DailyHabit")]
public class DailyHabit : ScriptableObject
{
    public string date;
    public int count;
}
