using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPack;
using System.Linq;

public class Packets
{
    
}

[MemoryPackable]
public partial struct StandartPacket
{
    public StandartPacket(int networkID, int order, int secretID, bool accelerate, bool brake, float turn, float x, float y, float z)
    {
        NetworkID = networkID;
        Order = order;
        SecretID = secretID;
        Accelerate = accelerate;
        Brake = brake;
        Turn = turn;
        PositionX = x;
        PositionY = y;
        PositionZ = z;
    }

    public int NetworkID { get; set; }
    public int Order { get; set; }
    public int SecretID { get; set; }
    public bool Accelerate { get; set; }
    public bool Brake { get; set; }
    public float Turn { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }

    public static byte[] GetBytes(StandartPacket packet)
    {
        byte[] result = new byte[] { 1 };
        result = result.Concat(MemoryPackSerializer.Serialize(packet)).ToArray();
        return result;
    }

    public static StandartPacket GetPacket(byte[] data)
    {
        return MemoryPackSerializer.Deserialize<StandartPacket>(data);
    }
}


[MemoryPackable]
public partial struct StartPacket
{
    public StartPacket(int networkID, int secretID, float positionX, float positionY, float positionZ)
    {
        NetworkID = networkID;
        SecretID = secretID;
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;
    }

    public int NetworkID { get; set; }
    public int SecretID { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }

    public static byte[] GetBytes(StartPacket packet)
    {
        byte[] result = new byte[] { 0 };
        result = result.Concat(MemoryPackSerializer.Serialize(packet)).ToArray();
        return result;
    }

    public static StartPacket GetPacket(byte[] data)
    {
        return MemoryPackSerializer.Deserialize<StartPacket>(data);
    }

}

