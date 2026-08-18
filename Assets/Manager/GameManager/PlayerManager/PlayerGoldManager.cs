using System;
using UnityEngine;

public class PlayerGoldManager : MonoBehaviour
{
    public event Action<int> GoldAmountChanged;
    private int currentGold = 0;
    public int GetGold()
    {
        return currentGold;
    }
    public void SetGold(int newGoldAmount)
    {
        currentGold = newGoldAmount;
        GoldAmountChanged?.Invoke(currentGold);
    }
}
