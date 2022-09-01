/* SCRIPT PARA CONTROLAR LA IA DEL ENEMIGO */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{

    /* VARIABLES */

    // PARAMETROS
    [SerializeField] private float _radius; //  Radio del FOV
    [Range(0,360)]
    [SerializeField] private float _angle; //  Angulo del FOV

    // REFERENCIAS
    private NavMeshAgent _navMeshAgent; //  Enemigo
    private GameObject _playerRef; //  Jugador
    [SerializeField] private LayerMask _targetMask; //  Capa del jugador
    [SerializeField] private LayerMask _obstructionMask; //  Capa del entorno
    [SerializeField] private Transform[] _waypoints; //  Puntos de patrulla

    // FLAGS
    [SerializeField] private bool _isChasing; //  El enemigo persigue al jugador
    private bool _canSeePlayer; //  El enemigo puede ver al jugador
    
    // VARIABLES AUXILIARES
    private int _waypointIndex; //  Punto al que va el enemigo
    private Vector3 _targetPosition; // Posicion a la que el enemigo debe ir

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // INICIALIZACION DE VALORES
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _waypointIndex = 0;

        UpdateDestination();

    }

    // METODO START
    // Start es llamado una vez antes de cada frame
    private void Start()
    {

        // OBTENCION DE REFERENCIAS
        _playerRef = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(FOVRoutine());
    }

    // METODO UPDATE
    // Update es llamado una vez por frame
    private void Update()
    {
        _isChasing = _canSeePlayer;
        //  DETERMINAR LA POSICION DEL ENEMIGO EN FUNCION DE SU ESTADO
        if (_isChasing)
        {
            _navMeshAgent.destination = _playerRef.transform.position;
        }
        else
        {
            Patrolling();
        }
        
    }

    /* METODOS DE LA IA DEL ENEMIGO */

    // METODO DE PATRULLA
    private void Patrolling()
    {
        if (Vector3.Distance(transform.position, _targetPosition)<1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    // METODO AUXILIAR DE PATRULLA: ITERAR INDICES
    private void IterateWaypointIndex()
    {
        // SI EL ENEMIGO YA HA RECORRIDO TODOS LOS PUNTOS RESETEAR. SI NO, AVANZAR HASTA EL SIGUIENTE
        _waypointIndex++;
        if(_waypointIndex == _waypoints.Length)
        {
            _waypointIndex = 0;
            
            // REMEZCLAR EL VECTOR
            for (int i = 0; i < _waypoints.Length; i++)
            {
                Transform temp = _waypoints[i];
                int rnd = UnityEngine.Random.Range(0, i);
                _waypoints[i] = _waypoints[rnd];
                _waypoints[rnd] = temp;
            }
        }
    }

    // METODO AUXILIAR DE PATRULLA: ACTUALIZAR DESTINO
    private void UpdateDestination()
    {
        // ACTUALIZAR DESTINO CON LOS WAYPOINTS
        _targetPosition = _waypoints[_waypointIndex].position;
        _navMeshAgent.destination = _targetPosition;
    }

    // RUTINA PARA CONTROLAR EL RITMO DE CHEQUEO DEL FOV DEL ENEMIGO
    private IEnumerator FOVRoutine()
    {
        // CADA 0,2 SEGUNDOS EL ENEMIGO CHEQUEA SU FOV
        float delay = 0.2f;

        while (true)
        {
            yield return new WaitForSeconds(delay);
            FOVCheck();
        }

    }

    // METODO PARA CHEQUEAR EL FOV DEL ENEMIGO
    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);
          
        if (rangeChecks.Length != 0)  // Chequear si el jugador estA en el radio del enemigo
        {
            Transform target = rangeChecks[0].transform; // El jugador estA en el radio del enemigo
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)  // Chequear si el jugador estA en el Angulo del enemigo
            {

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //RaycastHit hit;
                //!Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, _obstructionMask)

                RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToTarget, distanceToTarget, _obstructionMask);
                
                if (hits.Length == 0) // Chequear si algo bloquea la vista del enemigo
                {
                    _canSeePlayer = true; // Nada bloquea la vista del enemigo
                }
                else
                {
                    _canSeePlayer = false; // Algo bloquea la vista del enemigo
                }
            }else
            {
                _canSeePlayer = false; // El jugador ya no estA en el Angulo
            }
        }else if (_canSeePlayer)
        {
            _canSeePlayer = false; // El jugador ya no estA en el radio
        }
    }


    /* GETTERS */

    // RADIO
    public float GetRadius()
    {
        return _radius;
    }

    // ANGULO
    public float GetAngle()
    {
        return _angle;
    }

    // FLAG DE VISION DEL JUGADOR
    public bool GetCanSeePlayer()
    {
        return _canSeePlayer;
    }

    // REFERENCIA DEL JUGADOR
    public GameObject GetPlayerRef()
    {
        return _playerRef;
    }

}
