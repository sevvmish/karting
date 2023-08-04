using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ControlPoint : MonoBehaviour
{
    public Transform NextPoint;

    private HashSet<ArcadeKart> pilots = new HashSet<ArcadeKart>();
    private readonly float distanceForRespawnLimit = 5f;
    
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player") && other.TryGetComponent(out ArcadeKart pilot) && !pilots.Contains(pilot))
        {
            pilots.Add(pilot);

            if (Vector3.Distance(transform.position, other.transform.position) > 5)
            {
                other.gameObject.GetComponent<RespawnController>().SetRespawnPoint
                    (transform.position + Vector3.up + transform.right * (UnityEngine.Random.Range(-2,3)), NextPoint.position);
            }
            else
            {
                other.gameObject.GetComponent<RespawnController>().SetRespawnPoint
                    (other.transform.position + Vector3.up, NextPoint.position);
            }
            
            if (other.TryGetComponent(out MainPlayerDrivingHelper main))
            {
                main.SetNextPoint(NextPoint);
            }

            if (other.TryGetComponent(out BotController bot))
            {
                bot.SetNextPoint(NextPoint);
            }            
        }
        
    }

}
