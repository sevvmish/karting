using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using System;
using DG.Tweening;

public class InputController : MonoBehaviour
{
    private Joystick joystick;
    private Transform playerTransform;
    private int order = 0;

    private bool accelerateToSend;
    private bool breakToSend;
    private float turnToSend;

    public void SetInputController(Joystick _joystick, Transform playerTransfor)
    {
        this.playerTransform = playerTransfor;
        joystick = _joystick;
    }

    public StandartPacket GetMainPacket()
    {        
        StandartPacket p = new StandartPacket(Globals.NetworkID, order, Globals.SecretID, accelerateToSend, breakToSend, turnToSend,
            playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        order++;
        turnToSend = 0;
        accelerateToSend = false;
        breakToSend = false;
        return p;
    }

    void Update()
    {
        //Packet = new StandartPacket(Globals.NetworkID, order, Globals.SecretID, true, false, 0);
        //order++;
        accelerateToSend = true;

        if (Globals.Approved && joystick.Direction.magnitude > 0f)
        {
            if (joystick.Direction.y > 0f)
            {
                //Packet.Accelerate = true;
                accelerateToSend = true;
            }
            else if (joystick.Direction.y <= 0f)
            {
                //kart.accelerate = false;
            }

            if (joystick.Direction.y < -0.8f)
            {
                //Packet.Brake = true;
                //Packet.Accelerate = false;
                breakToSend = true;
                accelerateToSend = false;
            }
            else if (joystick.Direction.y >= 0.8f)
            {
                //Packet.Brake = false;
                breakToSend = false;
            }

            if (Math.Abs(joystick.Direction.x) > 0.1f)
            {
                //Packet.Turn = joystick.Direction.x;
                turnToSend += joystick.Direction.x;
            }
            else
            {
                //Packet.Turn = 0;
            }
            
        }
        else
        {
            //kart.accelerate = false;
            //Packet.Brake = false;
            //Packet.Turn = 0;
            //breakToSend = false;
        }

        if (!GameManager.Instance.IsRaceStarted)
        {
            accelerateToSend = false;
        }
    }
}
