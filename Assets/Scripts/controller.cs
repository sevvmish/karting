using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class controller : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    ArcadeKart kart; 

    // Start is called before the first frame update
    void Start()
    {
        kart = gameObject.GetComponent<ArcadeKart>();
        GameObject g = new GameObject();
        g.name = "efbhwerhkfbfbhrbj";
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Direction.magnitude > 0f)
        {
            if (joystick.Direction.y > 0f)
            {
                kart.accelerate = true;
            }
            else
            {
                kart.accelerate = false;
            }

            if (joystick.Direction.y < 0f)
            {
                kart.brake = true;
            }
            else
            {
                kart.brake = false;
            }

            if (joystick.Direction.x != 0)
            {
                kart.Turn = joystick.Direction.x;
            }
        }
    }
}
