using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

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
                kart.accelerate = false;
            }

            if (joystick.Direction.y < 0f)
            {
                kart.brake = true;
            }
            else if (joystick.Direction.y >= 0f)
            {
                kart.brake = false;
            }

            kart.Turn = joystick.Direction.x;
        }
        else
        {
            kart.accelerate = false;
            kart.brake = false;
            kart.Turn = 0;
        }
    }
}
