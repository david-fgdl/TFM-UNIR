using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    private int _HP;
    [SerializeField] private int _MaxHP;
    // Start is called before the first frame update
    void Start()
    {
        _HP = _MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainHP(int amm)
    {
        int newHP = _HP + amm;
        if (newHP > _MaxHP)
        {
            newHP = _MaxHP;
        }
        _HP = newHP;
    }
    public void LoseHP(int amm)
    {
        int newHP = _HP - amm;
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
    public void Death()
    {
        Debug.Log("Enemy is dead");
    }
}
