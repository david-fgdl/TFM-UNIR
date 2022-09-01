/* SCRIPT PARA CONTROLAR EL SISTEMA DE SALUDO DEL ENEMIGO */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthSystem : MonoBehaviour
{

    /* VARIABLES */

    // VALORES DE SALUD
    private int _HP;  // Puntos de salud actual
    [SerializeField] private int _maxHP;  // Puntos de salud maxima

    // REFERENCIAS
    private NavMeshAgent _navMeshAgent;

    /* METODOS BASICOS */
    
    // METODO START
    // Start es llamado una vez antes del primer frame
    private void Start()
    {
        _HP = _maxHP;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /* METODOS PARA REGULAR LA SALUD DEL ENEMIGO */

    // METODO PARA INCREMENTAR LA SALUD DEL ENEMIGO EN UN VALOR "am"
    public void GainHP(int amount)
    {
        int newHP = _HP + amount;
        if (newHP > _maxHP)
        {
            newHP = _maxHP;
        }
        _HP = newHP;
    }

    // METODO PARA REDUCIR LA SALUD DEL ENEMIGO EN UN VALOR "am"
    public void LoseHP(int amount)
    {
        int newHP = _HP - amount;
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

    // METODO PARA BLOQUEAR AL ENEMIGO SI ESTE ES NOQUEADO
    public void Death()
    {
        Debug.Log("Enemy knock out");
        _navMeshAgent.speed = 0;
        StartCoroutine(Revive());
    }

    // METODO PARA REDUCIR LA VELOCIDAD DEL ENEMIGO SI ESTE ES DAÑADO
    public void Hurt()
    {
        Debug.Log("Enemy got hurt");
        _navMeshAgent.speed = 1.5f;
        StartCoroutine(StopHurt());
    }

    // RUTINA PARA GESTIONAR EL TIEMPO DE RECUPERACION DEL ENEMIGO TRAS SER NOQUEADO
    private IEnumerator Revive()
    {
        float delay = 3f;
        yield return new WaitForSeconds(delay);;
        GainHP(100);
        _navMeshAgent.speed = 3.5f;
    }

    // RUTINA PARA GESTIONAR EL TIEMPO DE RECUPERACION DEL ENEMIGO TRAS SER DAÑADO
    private IEnumerator StopHurt()
    {
        float delay = 1f;
        yield return new WaitForSeconds(delay);
        _navMeshAgent.speed = 3.5f;
    }


}
