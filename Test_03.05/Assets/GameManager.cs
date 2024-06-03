using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{


    public List<GameObject> MinigameList = new List<GameObject>();
    public string NameForPhoton;


    public int MaxPlayer;
    public Slider MaxPlayerSlider;

    public TMP_InputField NickName;
    public TMP_Text NickNameShowCase;

    public TMP_Text[] Line;

    public GameObject Spawn;
    public GameObject[] LobbyListObjects;
    public GameObject LobbyListContent;
    public GameObject UserCard;

    public bool MiniGameStarted;

    public GameObject position;
    public GameObject GUI;
    public GameObject StartGUI;
    public GameObject GameGUI;
    public GameObject RunGame;
    public TMP_InputField InputName;
    public TMP_InputField RoomName;
    public TMP_InputField Name;
    public GameObject ShopPanel;
    public List<GameObject> PlayersSorted = new List<GameObject>();
    public List<GameObject> PlayersTemp = new List<GameObject>();
    public List<GameObject> PlayersJoin = new List<GameObject>();
    public int WhoseTurn = 0;
    public int MinigameCount = 0;

    public GameObject Kamera;


    // Start is called before the first frame update
    void Start()
    {



        PhotonNetwork.ConnectUsingSettings();
    }
    public void PlayButton()
    {
        if (NickName.text != "")
        {
            PhotonNetwork.NickName = NickName.text;
            StartGUI.SetActive(false);
        }



    }
    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayersSorted[0].GetComponent<DiceController>().Kameraa.gameObject.SetActive(true);
            PlayersSorted[1].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[2].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[3].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayersSorted[0].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[1].GetComponent<DiceController>().Kameraa.gameObject.SetActive(true);
            PlayersSorted[2].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[3].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayersSorted[0].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[1].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[2].GetComponent<DiceController>().Kameraa.gameObject.SetActive(true);
            PlayersSorted[3].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayersSorted[0].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[1].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[2].GetComponent<DiceController>().Kameraa.gameObject.SetActive(false);
            PlayersSorted[3].GetComponent<DiceController>().Kameraa.gameObject.SetActive(true);
        }



        MaxPlayer = (int)(MaxPlayerSlider.value);
        NickNameShowCase.text = NickName.text;
        /*
        foreach(GameObject player in PlayersJoin)
        {

            if(player.GetComponent<PhotonView>().ViewID/1000==1)
            {
                MaxPlayer = player.GetComponent<DiceController>().GameManager.GetComponent<GameManager>().MaxPlayer;
            }

        }



        */



        if (PlayersJoin.Count == 4 && PlayersSorted.Count != 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (PlayersJoin[i].GetComponent<PhotonView>().ViewID / 1000 == 1)
                {
                    PlayersSorted.Add(PlayersJoin[i]);
                }
            }



            for (int i = 0; i < 4; i++)
            {
                if (PlayersJoin[i].GetComponent<PhotonView>().ViewID / 1000 == 2)
                {
                    PlayersSorted.Add(PlayersJoin[i]);
                    Debug.Log("alo");
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (PlayersJoin[i].GetComponent<PhotonView>().ViewID / 1000 == 3)
                {
                    PlayersSorted.Add(PlayersJoin[i]);
                    Debug.Log("alo");
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (PlayersJoin[i].GetComponent<PhotonView>().ViewID / 1000 == 4)
                {
                    PlayersSorted.Add(PlayersJoin[i]);
                    Debug.Log("alo");
                }
            }

        }


    }

    public void Shop()
    {
        ShopPanel.SetActive(true);
    }
    public void Play()
    {
        ShopPanel.SetActive(false);
    }

    public void JoinRandomRoom()
    {
        string roomName = "Room_" + Random.Range(0, 1000000); // Generate a unique room name
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4; // Set maximum players
        PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
    }





    public void CreateRoom()
    {

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4; // Set maximum players
        PhotonNetwork.CreateRoom(RoomName.text, options);

    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(InputName.text);
    }


    public override void OnCreatedRoom()
    {

        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);

    }

    // Callback for when Photon successfully connects to the Master Server
    public override void OnConnectedToMaster()
    {

        Debug.Log("Connected to Photon Master Server. Ready to join or create a room.");
        GUI.SetActive(true);

    }





    public override void OnJoinedRoom()
    {

        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.Instantiate("Playerr", Spawn.transform.position, Quaternion.identity);
        GUI.SetActive(false);

    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {

        Debug.Log("Player entered room: " + newPlayer.NickName);
        //PhotonNetwork.Instantiate("Playerr", new Vector3(0, 0.51f, 0), Quaternion.identity);
        GUI.SetActive(false);

    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        base.OnPlayerLeftRoom(otherPlayer);

    }



    public void CreateRoomButton()
    {
        PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 4 }, null);
        if (MaxPlayer == 4)
        {

        }
        if (MaxPlayer == 3)
        {
            PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 3 }, null);
        }
        if (MaxPlayer == 2)
        {
            PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 2 }, null);
        }
        if (MaxPlayer == 1)
        {
            PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 1 }, null);
        }

    }

}