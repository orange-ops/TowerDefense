using UnityEngine;
using System;

public class PlayerEconomy : MonoBehaviour
{
    [SerializeField] private int _gold = 200;
    [SerializeField] private int _health= 3;

    //events
    public event Action<int> OnGoldChanged;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnAllHealthLost;

    private void Start()
    {
        OnGoldChanged?.Invoke(_gold);
        OnHealthChanged?.Invoke(_health);
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        OnGoldChanged?.Invoke(_gold);
    }

    public bool SpendGold(int amount)
    {
        if (_gold >= amount)
        {
            _gold -= amount;
            OnGoldChanged?.Invoke(_gold);
            return true;
        }
        else
        {
            Debug.Log("Not enough gold!");
            return false;
        }
    }

    public void LoseHealth(int damage)
    {
        if (_health > damage)
        {
            _health -= damage;
            OnHealthChanged?.Invoke(_health);
        }
        else
        {
            _health = 0;
            OnHealthChanged?.Invoke(_health);
            OnAllHealthLost?.Invoke(_health);

        }
    }


}
