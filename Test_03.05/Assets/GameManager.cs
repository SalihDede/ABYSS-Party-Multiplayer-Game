using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject[] MinigameList;

    public string NameForPhoton;

    public GameObject Spawn;
    public GameObject[] LobbyListObjects;
    public GameObject LobbyListContent;
    public GameObject UserCard;

    public bool MiniGameStarted;

    public GameObject position;
    public GameObject GUI;
    public GameObject GameGUI;
    public GameObject RunGame;
    public TMP_InputField Input;
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

    // Update is called once per frame
    void Update()
    {
       
        if(PlayersJoin.Count == 2 && PlayersSorted.Count != 2)
        {
            for (int i = 0; i < 2; i++)
            {
                if (PlayersJoin[i].GetComponent<PhotonView>().ViewID == 1001)
                {
                    PlayersSorted.Add(PlayersJoin[i]);
                }
            }



            for (int i = 0; i < 2; i++)
            {
                if (PlayersJoin[i].GetComponent<PhotonView>().ViewID == 2001)
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
        PhotonNetwork.JoinRoom(Input.text);
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

        PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 2 }, null);
    
    }

}
