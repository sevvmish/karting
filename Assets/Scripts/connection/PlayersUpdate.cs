using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayersUpdate : MonoBehaviour
{
    [SerializeField] private GameObject carForOnline;
    private Dictionary<int, Player> players = new Dictionary<int, Player>();
    private ArcadeKart mainKart;
    private Vector3[] positions = new Vector3[] 
    {
        new Vector3(0,0,0),
        new Vector3(1.5f,0,2),
        new Vector3(-1.5f, 0, 2),
        new Vector3(2.5f, 0, -2),
        new Vector3(-2.5f, 0, -2)
    };

    public void SetData(ArcadeKart mainKart)
    {
        this.mainKart = mainKart;
        //Player p = new Player(Globals.NetworkID, mainKart, new StandartPacket(Globals.NetworkID, 0, Globals.SecretID, false, false, 0));
        //players.Add(Globals.NetworkID, p);
    }

    public void AddMainPlayerPosition(Vector3 position)
    {
        Player p = new Player(Globals.NetworkID, mainKart, new StandartPacket(Globals.NetworkID, 0, Globals.SecretID, false, false, 0,
            mainKart.transform.position.x, mainKart.transform.position.y, mainKart.transform.position.z));
        players.Add(Globals.NetworkID, p);
        mainKart.transform.position = position;
        Globals.Approved = true;
    }

    public void PacketHandler(StandartPacket packet)
    {        
        if (players.ContainsKey(packet.NetworkID))
        {            
            players[packet.NetworkID].packets.Enqueue(packet);
        }
        else if (packet.NetworkID != Globals.NetworkID)
        {
            addPlayer(packet);
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (var player in players.Values)
        {
            Vector3 speed = Vector3.zero;
            bool isFresh = false;
            StandartPacket lastPacket = new StandartPacket();

            if (player.packets.Count > 0)
            {
                isFresh = true;
                lastPacket = player.packets.Dequeue();
                player.lastPacket = lastPacket;
            }
            else
            {
                lastPacket = player.lastPacket;
            }

            
            if (player.kart != null && lastPacket.NetworkID == Globals.NetworkID)
            {
                player.kart.accelerate = lastPacket.Accelerate;
                player.kart.brake = lastPacket.Brake;
                player.kart.Turn = lastPacket.Turn;
            }
            

            if (lastPacket.NetworkID != Globals.NetworkID)
            {
                Vector3 newPos = new Vector3(lastPacket.PositionX, lastPacket.PositionY, lastPacket.PositionZ);                
                player.kart.transform.position = newPos;
            }
            
        }
    }

    private void addPlayer(StandartPacket packet)
    {
        GameObject car = Instantiate(carForOnline,
            positions[players.Count], 
            Quaternion.Euler(0,0,0));

        Player p = new Player(packet.NetworkID, car.GetComponent<ArcadeKart>(), packet);
        players.Add(packet.NetworkID, p);
    }
}
