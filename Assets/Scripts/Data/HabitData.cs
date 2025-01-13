using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HabitData", menuName = "Scriptable Objects/HabitData")]
public class HabitData : ScriptableObject
{
    public int targetDate;
    public List<DailyHabit> habits = new List<DailyHabit>();
}
