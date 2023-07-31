using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using TMPro;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject mainCartPrefab;
    [SerializeField] private Joystick joystick;
    [SerializeField] private TextMeshProUGUI timerText;

    private Transform playerTransform;
    private CameraController cameraController;
    private InputController inputController;
    private ArcadeKart mainArcadeKart;

    //TODELETE
    private float _timer;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameObject g = Instantiate(mainCartPrefab);
        g.name = "Main Player";
        playerTransform = g.transform;
        playerTransform.position = Vector3.zero;
        mainArcadeKart = g.GetComponent<ArcadeKart>();

        cameraController = GetComponent<CameraController>();
        cameraController.SetCameraController(GameObject.Find("Main Camera").GetComponent<Camera>(), playerTransform);

        inputController = GetComponent<InputController>();
        inputController.SetInputController(joystick, playerTransform.GetComponent<ArcadeKart>());
    }

    
    private void Update()
    {
        _timer += Time.deltaTime;
        timerText.text = _timer.ToString("f0") + " = " + mainArcadeKart.LocalSpeed().ToString("f0");
    }
    

}
