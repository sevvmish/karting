using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Vector3 pointToLookAt;
    private Transform _transform;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetRespawnPoint(Vector3 newpoint, Vector3 look)
    {
        respawnPoint = newpoint;
        pointToLookAt = look;
    }

    public void SpawnInLastRespawnPoint()
    {
        _rigidbody.velocity = Vector3.zero;
        _transform.position = respawnPoint;
        _transform.LookAt(pointToLookAt);
    }
}
