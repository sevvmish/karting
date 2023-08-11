using DG.Tweening;
using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform GetMainCameraTransfor { get => mainCamera.transform; }

    private Camera mainCamera;
    private Transform cameraBody;
    private Transform playerTransform;
    private ArcadeKart playerKart;
    private Vector3 cameraPositionShift = new Vector3(0,2f,-3f);//-4
    private Vector3 cameraRotationShift = new Vector3(20,0,0);
        
    private readonly float timerCooldown = 0.1f;
    private Vector3 speed;
    private float zCoord;
    
    public void SetCameraController(Camera _camera, Transform _player)
    {
        mainCamera = _camera;
        playerTransform = _player;
        playerKart = _player.GetComponent<ArcadeKart>();

        GameObject g = new GameObject("Camera Body");
        cameraBody = g.transform;

        mainCamera.transform.parent = cameraBody;
        mainCamera.transform.localEulerAngles = Vector3.zero;
        mainCamera.transform.position = Vector3.zero;

        cameraBody.eulerAngles = cameraRotationShift;
        cameraBody.position = cameraPositionShift + _player.position;
    }

    
    private void Update()
    {

        zCoord = math.lerp(cameraPositionShift.z, 0, playerKart.LocalSpeed());

        cameraBody.position = Vector3.SmoothDamp(
            cameraBody.position,
            playerTransform.position
             + playerTransform.right * cameraPositionShift.x
             + playerTransform.up * cameraPositionShift.y
             + playerTransform.forward * zCoord,
            ref speed,
            timerCooldown
            );      
        cameraBody.LookAt(playerTransform.position + Vector3.up * 1.2f);
        

        /*
        zCoord = math.lerp(cameraPositionShift.z, 0, playerKart.LocalSpeed());


        cameraBody.DOMove(playerTransform.position
             + playerTransform.right * cameraPositionShift.x
             + playerTransform.up * cameraPositionShift.y
             + playerTransform.forward * cameraPositionShift.z, timerCooldown);

        cameraBody.DOLookAt(playerTransform.position + Vector3.up * 1.7f, timerCooldown);
        */
    }
}
