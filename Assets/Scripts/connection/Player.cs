using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int NetworkID;
    public ArcadeKart kart;
    public Queue<StandartPacket> packets = new Queue<StandartPacket>();
    public StandartPacket lastPacket;

    public Player(int networkID, ArcadeKart _kart, StandartPacket lastPacket)
    {
        NetworkID = networkID;
        kart = _kart;
        this.lastPacket = lastPacket;
    }
}
