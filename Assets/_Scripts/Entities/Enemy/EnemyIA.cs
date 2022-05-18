using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent NavMeshAgent;
    [SerializeField] bool Chasing;
    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Chasing)
        {
            NavMeshAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else
        {
            //Patrulla
        }
        
    }
}
