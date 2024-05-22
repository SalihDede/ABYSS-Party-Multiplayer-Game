using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DiceController : MonoBehaviourPunCallbacks
{
    public int diceResult;
    private int currentPlayerIndex = 0;
    public bool isMyTurn = false;
    private bool isMoving = false;
    public GameObject GameManager;
    public GameObject GUI;



    public Button startButton;
    public List<GameObject> StepsList = new List<GameObject>();


    bool AlreadyAdded = false;

    public GameObject[] Steps = new GameObject[24];
    private int stepLine = 0;

    public TMP_Text diceResultText;
    public GameObject throwButton;
    int sorted = 0;

    //public int WhichTurn = 0;



    public int IsStart = 0;

    private void Update()
    {


  



            if(GameManager.GetComponent<GameManager>().PlayersSorted.Count == 2)
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

    void Start()
    {



 

        GameManager = GameObject.Find("GameManager");
        gameObject.name = GameManager.GetComponent<GameManager>().PlayersJoin.Count.ToString();

        diceResultText = GameObject.Find("DiceNumberResult").GetComponent<TMP_Text>();
        GameManager.GetComponent<GameManager>().PlayersJoin.Add(gameObject);

 
 

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

    public void StartGameButton()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
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
            diceResult = Random.Range(1, 7);
            stepLine += diceResult;

            if (stepLine >= StepsList.Count)
            {
                Debug.LogWarning("Player moved beyond the steps array.");
                stepLine = StepsList.Count - 1;
            }

            StartCoroutine(MovePlayer(new Vector3(StepsList[stepLine].transform.position.x, StepsList[stepLine].transform.position.y + 0.5f, StepsList[stepLine].transform.position.z)));
            photonView.RPC("SyncDiceResult", RpcTarget.All, diceResult);

            if (GameManager.GetComponent<GameManager>().WhoseTurn != 1)
            {

                GameManager.GetComponent<GameManager>().WhoseTurn = +1;
            }
            else
            {
                GameManager.GetComponent<GameManager>().MinigameCount = Random.Range(0,5);
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
        GameManager.GetComponent<GameManager>().MinigameList[GameManager.GetComponent<GameManager>().MinigameCount].SetActive(true);
        GameManager.GetComponent<GameManager>().Kamera.SetActive(false);



    }


    [PunRPC]
    void SyncDiceResult(int result)
    {
        diceResult = result;
        diceResultText.text = "Dice result: " + result; // Update the text component with the dice result
    }


}
