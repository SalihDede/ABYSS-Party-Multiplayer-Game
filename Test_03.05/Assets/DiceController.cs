using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DiceController : MonoBehaviourPunCallbacks
{


    private PhotonView photonView;
    public GameObject Kameraa;

    public int diceResult;
    private int currentPlayerIndex = 0;
    public bool isMyTurn = false;
    private bool isMoving = false;
    public GameObject GameManager;
    public GameObject GUI;

    public GameObject BahaGame;
    public GameObject YusufGame;
    public GameObject SemihGame;
    public GameObject AlpGame;
    public GameObject SalihGame;


    public Button startButton;
    public List<GameObject> StepsList = new List<GameObject>();


    bool AlreadyAdded = false;

    public GameObject[] Steps = new GameObject[24];
    public int stepLine = 0;

    public TMP_Text diceResultText;
    public TMP_Text NameOf;
    public GameObject throwButton;
    int sorted = 0;

    //public int WhichTurn = 0;



    public int IsStart = 0;



    void Start()
    {






        photonView = GetComponent<PhotonView>();

        GameManager = GameObject.Find("GameManager");
        gameObject.name = photonView.Controller.NickName;

        GameManager.GetComponent<GameManager>().NameForPhoton = gameObject.name;

        diceResultText = GameObject.Find("DiceNumberResult").GetComponent<TMP_Text>();
        GameManager.GetComponent<GameManager>().PlayersJoin.Add(gameObject);

        NameOf.text = photonView.Controller.NickName;


        if (photonView.IsMine)
        {

            GUI.SetActive(true);

        }


        for (int i = 1; i < 25; i++)
        {
            StepsList.Add(GameObject.Find(i.ToString()));
        }
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);

    }


    private void Update()
    {

        //GameManager.GetComponent<GameManager>().Line[GameManager.GetComponent<GameManager>().PlayersSorted.IndexOf(gameObject)].text = photonView.Controller.NickName;
        /*
        MaxPlayerImplementFunc();

        */

       GameManager.GetComponent<GameManager>().PlayersJoin.Sort((player1, player2) => player2.GetComponent<DiceController>().stepLine.CompareTo(player1.GetComponent<DiceController>().stepLine));

        if (BahaGame != null)
        {
            if (BahaGame.GetComponent<GameTwoManager>().Ranking.Count == 4)
            {
                GUI.SetActive(true);
                GameManager.GetComponent<GameManager>().MiniGameStarted = false;
            }
        }
        else
        {
            BahaGame = GameObject.Find("Baha Game");
        }
    

        if (GameManager.GetComponent<GameManager>().MiniGameStarted)
        {
             GUI.SetActive(false);
        }
        else
        {
            GUI.SetActive(true);
        }




            if(GameManager.GetComponent<GameManager>().PlayersSorted.Count == 4)
            {
                if (GameManager.GetComponent<GameManager>().PlayersSorted[0].GetComponent<DiceController>().IsStart != 0)
                {

                    foreach (GameObject Player in GameManager.GetComponent<GameManager>().PlayersSorted)
                    {
                        if (GameManager.GetComponent<GameManager>().PlayersSorted.IndexOf(Player) == GameManager.GetComponent<GameManager>().WhoseTurn)
                        {
                            Player.GetComponent<DiceController>().throwButton.SetActive(true);
                        }
                        else
                        {
                            Player.GetComponent<DiceController>().throwButton.SetActive(false);
                        }
                    }




                }
            }
      

 

       



    }
    public void NameUpdate()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("NAME", RpcTarget.All, PhotonNetwork.NickName);

        }

    }
    public void MaxPlayerImplementFunc()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("MaxPlayerImplement", RpcTarget.All, GameManager.GetComponent<GameManager>().MaxPlayer);

        }

    }

    [PunRPC]
    void MaxPlayerImplement(int result)
    {


            GameManager.GetComponent<GameManager>().MaxPlayer = result;
     


    }

    public void StartGameButton()
            {
                if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 4)
                {
                    startButton.gameObject.SetActive(false);
                    photonView.RPC("AdminStart", RpcTarget.All, 1);

                }
            }
    //PhotonNetwork.Instantiate("Playerr", new Vector3(0, 0.51f, 0), Quaternion.identity);

    public void ThrowDice()
    {
        if (photonView.IsMine)
        {
            if(GameManager.GetComponent<GameManager>().WhoseTurn == 0)
            {
                diceResult = Random.Range(1, 9);
            }
            if (GameManager.GetComponent<GameManager>().WhoseTurn == 1)
            {
                diceResult = Random.Range(1, 7);
            }
            if (GameManager.GetComponent<GameManager>().WhoseTurn == 2)
            {
                diceResult = Random.Range(1, 5);
            }
            if (GameManager.GetComponent<GameManager>().WhoseTurn == 3)
            {
                diceResult = Random.Range(1, 3);
            }
            stepLine += diceResult;

            if (stepLine >= StepsList.Count)
            {
                Debug.LogWarning("Player moved beyond the steps array.");
                stepLine = StepsList.Count - 1;
            }

            StartCoroutine(MovePlayer(new Vector3(StepsList[stepLine].transform.position.x, StepsList[stepLine].transform.position.y + 0.5f, StepsList[stepLine].transform.position.z)));
            photonView.RPC("SyncDiceResult", RpcTarget.All, diceResult,stepLine);

            if (GameManager.GetComponent<GameManager>().WhoseTurn != 3)
            {

                GameManager.GetComponent<GameManager>().WhoseTurn += 1;
            }
            else
            {

                GameManager.GetComponent<GameManager>().MinigameCount = Random.Range(0, GameManager.GetComponent<GameManager>().MinigameList.Count);
                photonView.RPC("MiniGameSelectUpdate", RpcTarget.All, GameManager.GetComponent<GameManager>().MinigameCount);

 
                GameManager.GetComponent<GameManager>().WhoseTurn = 0;
            }




            photonView.RPC("WhoseTurnUpdate", RpcTarget.All, GameManager.GetComponent<GameManager>().WhoseTurn);



        }




    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        isMoving = true;
        gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
        Vector3 startingPosition = transform.position;
        float duration = 1.0f; // Adjust this value to control the movement speed

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            // Calculate the direction from the current position to the target position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // If the direction is not zero, rotate the object to face the target
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime / duration);
            }

            yield return null;
        }

        transform.position = targetPosition;
        gameObject.GetComponent<Animator>().SetBool("IsJumping", false);
        isMoving = false;
    }


    [PunRPC]
    void AdminStart(int result)
    {
        IsStart = result;


    }


    [PunRPC]
    void IsMovingUpdate(bool result)
    {
        isMoving = result;


    }



    [PunRPC]
    void WhoseTurnUpdate(int result)
    {
        GameManager.GetComponent<GameManager>().WhoseTurn = result;


    }

    [PunRPC]
    void MiniGameSelectUpdate(int result)
    {
        
        GameManager.GetComponent<GameManager>().MinigameCount = result;

        GameManager.GetComponent<GameManager>().Kamera.SetActive(false);
        GameManager.GetComponent<GameManager>().GameGUI.SetActive(false);
        GameManager.GetComponent<GameManager>().GUI.SetActive(false);
        GameManager.GetComponent<GameManager>().MiniGameStarted = true;
        if(GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].name == "SemihGame")
        {
            if(GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameOneManager>().GameManagerrr != null)
            {
                GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameOneManager>().GameFinished = false;
                StartCoroutine(GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameOneManager>().GUIDECoroutine());
            }

        }
        if (GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].name == "Alp Game")
        {

            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().OBJCordinate.GetComponent<ObjectCoordinateMaanager>().initialCountdownTime = 15f;
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().OBJCordinate.GetComponent<ObjectCoordinateMaanager>().countdownTime = 30f;
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().OBJCordinate.GetComponent<ObjectCoordinateMaanager>().Line.SetActive(true);
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().OBJCordinate.GetComponent<ObjectCoordinateMaanager>().initialCountdownFinished = false;
                       foreach (GameObject player in GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().allObjectsMain)
                       {
                            if(player != null)
                            {
                                player.SetActive(true);
                            }


                       }
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().GameStarted = true;
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFourManager>().DoorManager.GetComponent<DoorManager>().StartDoorClosed();

        }


        if (GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].name == "SalihGame")
        {
            var currentMinigame = GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameFiveManager>();
            currentMinigame.gameActive = true;

            foreach (var puzzleMap in currentMinigame.PuzzleMapList)
            {
                var puzzleManager = puzzleMap.GetComponent<PuzzleManager>();

                if (puzzleManager.originalPositions != null)
                {
                    // Reset tiles to original positions
                    foreach (var kvp in puzzleManager.originalPositions)
                    {
                        kvp.Key.position = kvp.Value;
                    }

                    // Activate the GameObject if it's inactive
                    if (!puzzleManager.gameObject.activeInHierarchy)
                    {
                        puzzleManager.gameObject.SetActive(true);
                    }

                    // Clear current position and shuffle puzzle
                    puzzleManager.SaveCurrentPositions();
                    puzzleManager.ShufflePuzzle();
                    StartPuzzleManagerCoroutine(puzzleManager);
                    //puzzleManager.StartCoroutine(puzzleManager.UpdateCurrentPositionsPeriodically());
                }
            }
        }

        void StartPuzzleManagerCoroutine(PuzzleManager puzzleManager)
        {
            if (puzzleManager.gameObject.activeInHierarchy)
            {
            puzzleManager.StartCoroutine(puzzleManager.UpdateCurrentPositionsPeriodically());
            }
        }




        if (GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].name == "YusufGame")
        {
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameThreeManager>().gameActive = true;
            GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].GetComponent<GameThreeManager>().StartTime = false;
        }




        GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].SetActive(true);


        GameManager.GetComponent<GameManager>().MinigameList.Remove(GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount]);

    }


    [PunRPC]
    void SyncDiceResult(int result,int stepline)
    {
        diceResult = result;
        stepLine = stepline;
        diceResultText.text = result.ToString(); // Update the text component with the dice result
    }

    [PunRPC]
    void NAME(string result)
    {
        NameOf.text = result;
    }



}
