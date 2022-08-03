using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private bool _isChasing;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (_isChasing)
        {
            _navMeshAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else
        {
            //Patrulla
        }
        
    }
}
