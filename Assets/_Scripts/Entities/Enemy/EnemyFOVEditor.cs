/* SCRIPT QUE OFRECE UNA AYUDA VISUAL DESDE EL EDITOR A LA HORA DE REALIZAR EL ENEMIGO*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if (UNITY_EDITOR) 
[CustomEditor(typeof(EnemyIA))]
public class EnemyFOVEditor : Editor
{

    /* METODOS DE DIBUJADO */

    // METODO QUE SOLO SE USA COMO AYUDA VISUAL DURANTE EL DESARROLLO. DIBUJA EL RADIO Y EL ANGULO DEL FOV, ASI COMO UNA LINEA SI EL ENEMIGO PUEDE VER AL JUGADOR
    private void OnSceneGUI()
    {
        EnemyIA Enemy = (EnemyIA)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(Enemy.transform.position, Vector3.up, Vector3.forward, 360, Enemy.GetRadius());
        Vector3 viewAngle1 = DirectionFromAngle(Enemy.transform.eulerAngles.y, -Enemy.GetAngle() / 2);
        Vector3 viewAngle2 = DirectionFromAngle(Enemy.transform.eulerAngles.y, Enemy.GetAngle() / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(Enemy.transform.position, Enemy.transform.position + viewAngle1 * Enemy.GetRadius());
        Handles.DrawLine(Enemy.transform.position, Enemy.transform.position + viewAngle2 * Enemy.GetRadius());

        if (Enemy.GetCanSeePlayer())
        {
            Handles.color = Color.green;
            Handles.DrawLine(Enemy.transform.position, Enemy.GetPlayerRef().transform.position);
        }
    }

    // METODO AUXILIAR PARA DIBUJAR LA DIRECCION DESDE EL ANGULO
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
#endif
