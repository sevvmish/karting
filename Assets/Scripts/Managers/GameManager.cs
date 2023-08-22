using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject mainCartPrefab;
    [SerializeField] private Joystick joystick;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI packetText;


    public bool IsRaceStarted { get => isRaceStarted; } 

    //TODEL
    [SerializeField] private Button restartB;
    private float _timerForUpdate;

    public Transform MainPlayerTransform { get; private set; }
    private CameraController cameraController;
    private InputController inputController;
    private WebsocketConnection connection;
    private PlayersUpdate playersUpdate;
    private ArcadeKart mainArcadeKart;
    
    private bool isRaceStarted;
    private readonly float delayBeforeRaceStart = 3f;        
    private float mainRaceTimer;
    private float mainGameTimer;

    // Start is called before the first frame update
    void Awake()
    {
        //Application.runInBackground = true;

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
        g.AddComponent<MainPlayerDrivingHelper>();
        MainPlayerTransform = g.transform;
        MainPlayerTransform.position = Vector3.zero;
        mainArcadeKart = g.GetComponent<ArcadeKart>();

        cameraController = GetComponent<CameraController>();
        cameraController.SetCameraController(GameObject.Find("Main Camera").GetComponent<Camera>(), MainPlayerTransform);

        inputController = GetComponent<InputController>();
        inputController.SetInputController(joystick, MainPlayerTransform);

        restartB.onClick.AddListener(() => { SceneManager.LoadScene("tests"); });

        Globals.NetworkID = UnityEngine.Random.Range(-9999999, 9999999);
        playersUpdate = GetComponent<PlayersUpdate>();
        playersUpdate.SetData(mainArcadeKart);
                
        print("network id is set to " + Globals.NetworkID);
        connection = GetComponent<WebsocketConnection>();
        connection.StartConnection(mainArcadeKart, packetText, playersUpdate, inputController);

        /*
        //bot
        GameObject b = Instantiate(mainCartPrefab, new Vector3(1.5f, 0, 2), Quaternion.identity);
        b.name = "bot1";        
        b.transform.position = new Vector3(1.5f,0,2);
        b.AddComponent<BotController>();

        b = Instantiate(mainCartPrefab, new Vector3(-1.5f, 0, 2), Quaternion.identity);
        b.name = "bot2";
        b.transform.position = new Vector3(-1.5f, 0, 2);
        b.AddComponent<BotController>();

        b = Instantiate(mainCartPrefab, new Vector3(2.5f, 0, -2), Quaternion.identity);
        b.name = "bot3";
        b.transform.position = new Vector3(2.5f, 0, -2);
        b.AddComponent<BotController>();

        b = Instantiate(mainCartPrefab, new Vector3(-2.5f, 0, -2), Quaternion.identity);
        b.name = "bot4";
        b.transform.position = new Vector3(-2.5f, 0, -2);
        b.AddComponent<BotController>();
        */
    }

    
    private void Update()
    {
        mainGameTimer += Time.deltaTime;

        if (_timerForUpdate > 1)
        {
            _timerForUpdate = 0;
            timerText.text = mainRaceTimer.ToString("f0");
        }
        else
        {
            _timerForUpdate += Time.deltaTime;
        }

        

        if (!isRaceStarted)
        {
            if (mainGameTimer >= delayBeforeRaceStart)
            {
                isRaceStarted = true;
            }
        }
        else
        {
            mainRaceTimer += Time.deltaTime;
        }

        
    }
    

}
