using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using System;

public class InputController : MonoBehaviour
{
    private Joystick joystick;
    private ArcadeKart kart;

    public void SetInputController(Joystick _joystick, ArcadeKart _kart)
    {
        joystick = _joystick;
        kart = _kart;
    }

    void Update()
    {
        if (joystick.Direction.magnitude > 0f)
        {
            if (joystick.Direction.y > 0f)
            {
                kart.accelerate = true;
            }
            else if (joystick.Direction.y <= 0f)
            {
                //kart.accelerate = false;
            }

            if (joystick.Direction.y < -0.8f)
            {
                kart.brake = true;
                kart.accelerate = false;
            }
            else if (joystick.Direction.y >= 0.8f)
            {
                kart.brake = false;
            }

            if (Math.Abs(joystick.Direction.x) > 0.1f)
            {
                kart.Turn = joystick.Direction.x;
            }
            else
            {
                kart.Turn = 0;
            }
            
        }
        else
        {
            //kart.accelerate = false;
            kart.brake = false;
            kart.Turn = 0;
        }

        if (!GameManager.Instance.IsRaceStarted)
        {
            kart.accelerate = false;
        }
    }
}
