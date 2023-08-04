using DG.Tweening;
using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private ArcadeKart kart;
    private Rigidbody _rigidbody;
    private Transform kartTransform;
    private Transform nextPoint;
    private Vector3 previousPoint;
    private float x_offsetOnStart;

    private float _timer;
    private readonly float updateDataTime = 0.1f;

    private void Start()
    {
        kart = GetComponent<ArcadeKart>();
        kartTransform = kart.transform;
        x_offsetOnStart = transform.position.x;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetNextPoint(Transform nextPoint)
    {
        previousPoint = kartTransform.position;
        this.nextPoint = nextPoint;
    }

    private void Update()
    {
        if (GameManager.Instance.IsRaceStarted)
        {
            kart.accelerate = true;
        }

        if (_timer > updateDataTime)
        {
            Vector3 newNextPosition = nextPoint.position + nextPoint.right * x_offsetOnStart;
            float angle = Vector3.Angle(kartTransform.forward, (newNextPosition - kartTransform.position));
            float distance = (newNextPosition - kartTransform.position).magnitude;
            float x_step = kartTransform.InverseTransformPoint(newNextPosition).x;

            //print(gameObject.name + ": angle - " + angle + ", dist - " + distance);

            if (angle > 2 && angle < 70 && (distance > 2))
            {                
                if (x_step < 0)
                {
                    kart.Turn = -1;
                }
                else
                {
                    kart.Turn = 1;
                }

            }
            else if (angle > 85 && kart.IsWallHelperOn)
            {
                //print(gameObject.name + ": USEDUNSTUCK = angle - " + angle + ", dist - " + distance);
                                
                kart.transform.DOLookAt(new Vector3(newNextPosition.x, kartTransform.position.y, newNextPosition.z), 0.3f);

                /*
                if ((GameManager.Instance.MainPlayerTransform.position - kartTransform.position).magnitude < 15)
                {
                    kart.transform.DOLookAt(new Vector3(newNextPosition.x, kartTransform.position.y, newNextPosition.z), 0.3f);
                }
                else
                {
                    //kart.transform.LookAt(new Vector3(newNextPosition.x, kartTransform.position.y, newNextPosition.z));
                    kart.transform.DOLookAt(new Vector3(newNextPosition.x, kartTransform.position.y, newNextPosition.z), 0.3f);
                }
                */

                _rigidbody.velocity = Vector3.zero;

                /*
                float toYAngle = angle - 5;

                if (x_step < 0)
                {
                    kart.Rigidbody.MoveRotation(Quaternion.AngleAxis(toYAngle, Vector3.up));
                }
                else
                {
                    kart.Rigidbody.MoveRotation(Quaternion.AngleAxis(-toYAngle, Vector3.up));
                }*/
            }
            else
            {
                kart.Turn = 0;
            }
        }
        else
        {
            _timer += Time.deltaTime;
        }
        
    }
}
