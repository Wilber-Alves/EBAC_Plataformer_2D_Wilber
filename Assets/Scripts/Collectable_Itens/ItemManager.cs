using EDGEE.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{

    public float totalCoins;
    void Start()
    {
        Reset();
    }
    private void Reset()
    {
        totalCoins = 0;


    }
    public void AddCoins(float amount = 0.5f)
    {
        totalCoins += amount;
    }
}
