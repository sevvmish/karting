using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using DG.Tweening;

public class MainPlayerDrivingHelper : MonoBehaviour
{
    private ArcadeKart kart;
    private Rigidbody _rigidbody;
    private Transform nextPoint;
    private Vector3 lastPosition;

    private float _timer;
    private readonly float updateDataTime = 1f;

    private void Start()
    {
        kart = GetComponent<ArcadeKart>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetNextPoint(Transform nextPoint)
    {        
        this.nextPoint = nextPoint;
    }

    private void Update()
    {        
        if (_timer > updateDataTime)
        {
            float angle = Vector3.Angle(kart.transform.forward, (nextPoint.position - kart.transform.position));

            if (angle > 85 && (lastPosition - kart.transform.position).magnitude < 0.1f && kart.IsWallHelperOn)
            {
                //print(gameObject.name + ": USEDUNSTUCK  on main = angle - " + angle);

                //kart.transform.DOLookAt(nextPoint.position, 0.3f);
                kart.transform.DOLookAt(new Vector3(nextPoint.position.x, kart.transform.position.y, nextPoint.position.z), 0.3f);
                _rigidbody.velocity = Vector3.zero;
            }

            lastPosition = kart.transform.position;

        }
        else
        {
            _timer += Time.deltaTime;
        }

    }
}
