using UnityEngine;

// HabitData.cs - 습관 데이터를 저장할 클래스
[CreateAssetMenu(fileName = "DailyHabit", menuName = "Scriptable Objects/DailyHabit")]
public class DailyHabit : ScriptableObject
{
    public string date;
    public int count;
}
