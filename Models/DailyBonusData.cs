using UnityEngine;

[CreateAssetMenu(fileName = "DailyBonusData", menuName = "ScriptableObjects/DailyBonusData", order = 1)]

public class DailyBonusData : ScriptableObject  
{  
    public int[] dailyBonusRewards; // Array for daily bonus rewards  

    private void OnEnable() 
    {  
        dailyBonusRewards = new int[7] { 10, 50, 0, 80, 100, 150, 200 }; // Set the rewards for each day  
    }  
}
