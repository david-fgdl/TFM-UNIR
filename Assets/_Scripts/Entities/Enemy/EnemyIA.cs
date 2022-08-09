using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private bool _isChasing;
    [SerializeField] private float _radius;
    [Range(0,360)]
    [SerializeField] private float _angle;
    private GameObject _playerRef;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;
    [SerializeField] private Transform[] _waypoints;
    private int _waypointIndex;
    private Vector3 targetPosition;
    private bool _canSeePlayer;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _waypointIndex = 0;
        UpdateDestination();
    }
    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        if (_isChasing)
        {
            _navMeshAgent.destination = _playerRef.transform.position;
        }
        else
        {
            Patrolling();
        }
        
    }

    private void Patrolling()
    {
        if (Vector3.Distance(transform.position, targetPosition)<1)
        {
            IterateWayPointIndex();
            UpdateDestination();
        }
    }

    private void IterateWayPointIndex()
    {
        _waypointIndex++;
        if(_waypointIndex == _waypoints.Length)
        {
            _waypointIndex = 0;
            for (int i = 0; i < _waypoints.Length; i++)
            {
                Transform temp = _waypoints[i];
                int rnd = UnityEngine.Random.Range(0, i);
                _waypoints[i] = _waypoints[rnd];
                _waypoints[rnd] = temp;
            }
        }
    }

    private void UpdateDestination()
    {
        targetPosition = _waypoints[_waypointIndex].position;
        _navMeshAgent.destination = targetPosition;
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //RaycastHit hit;
                //!Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, _obstructionMask)
                RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToTarget, distanceToTarget, _obstructionMask);
                if (hits.Length == 0) 
                {
                    _canSeePlayer = true;
                }
                else
                {
                    _canSeePlayer = false;
                }
            }else
            {
                _canSeePlayer = false;
            }
        }else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }
    }

    public float getRadius()
    {
        return _radius;
    }
    public float getAngle()
    {
        return _angle;
    }
    public bool getCanSeePlayer()
    {
        return _canSeePlayer;
    }

    public GameObject getplayerRef()
    {
        return _playerRef;
    }
}
