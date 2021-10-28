using System;
using UnityEngine;

public class Economic : MonoBehaviour
{
    [SerializeField]
    private int startMoney = 100;

    public static event Action<Team, int> OnMoneyChanged = delegate { };

    public int Money { get; protected set; } = 0;

    Base _base;

    private void Start()
    {
        _base = GetComponent<Base>();

        AddMoney(startMoney);
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        OnMoneyChanged(_base.Team, Money);
    }

    public bool TakeMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            OnMoneyChanged(_base.Team, Money);

            return true;
        }
        else return false;
    }
}
