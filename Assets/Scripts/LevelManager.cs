using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("Path")]
    public Transform[] path;

    
    public Transform startingPoint;

    public int currency;

    private void Awake()
    {
        main = this;
        currency = 100;
    }


    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount  <= currency)
        {
            //buy
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("not enough to buy this");
            return false;
        }
    }
}