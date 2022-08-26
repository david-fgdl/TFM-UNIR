using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthSystem : MonoBehaviour
{
    private int _HP;//Current Health Points
    [SerializeField] private int _MaxHP;//Max Health Points
    private NavMeshAgent _navMeshAgent;
    void Start()
    {
        _HP = _MaxHP;
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
            Hurt();
        }

    }
    //What happens when the enemy dies
    public void Death()
    {
        Debug.Log("Enemy knock out");
        _navMeshAgent.speed = 0;
        StartCoroutine(revive());
    }
    public void Hurt()
    {
        Debug.Log("Enemy got hurt");
        _navMeshAgent.speed = 1.5f;
        StartCoroutine(StopHurt());
    }

    private IEnumerator revive()
    {
        float delay = 3f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        yield return wait;
        GainHP(100);
        _navMeshAgent.speed = 3.5f;
    }

    private IEnumerator StopHurt()
    {
        float delay = 1f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        yield return wait;
        _navMeshAgent.speed = 3.5f;
    }


}
