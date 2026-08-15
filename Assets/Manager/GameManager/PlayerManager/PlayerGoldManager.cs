using System;

public class PlayerGoldManager
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
