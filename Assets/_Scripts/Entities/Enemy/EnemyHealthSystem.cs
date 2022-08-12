using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    private int _HP;//Current Health Points
    [SerializeField] private int _MaxHP;//Max Health Points
    void Start()
    {
        _HP = _MaxHP;
    }

    //If we want to the enemy to gain health points (int amount)
    public void GainHP(int am)
    {
        int newHP = _HP + am;
        if (newHP > _MaxHP)
        {
            newHP = _MaxHP;
        }
        _HP = newHP;
    }
    //If we want to the enemy to lose health points (int amount)
    public void LoseHP(int am)
    {
        int newHP = _HP - am;
        if (newHP <= 0)
        {
            _HP = 0;
            Death();
        }
        else
        {
            _HP = newHP;
        }

    }
    //What happens when the enemy dies
    public void Death()
    {
        Debug.Log("Enemy is dead");
    }
}
