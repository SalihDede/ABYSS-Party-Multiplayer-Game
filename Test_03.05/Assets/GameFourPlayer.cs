using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameFourPlayer : MonoBehaviour
{
    // Game objects for different objects in the scene
    public GameObject chair; public GameObject bluePlate; public GameObject happyPillow; public GameObject Fork;
    public GameObject Spoon; public GameObject Knife; public GameObject Hat;
    public GameObject Sword; public GameObject Glassess; public GameObject Computer;
    public GameObject Calculator; public GameObject Brown; public GameObject Red; public GameObject Blue;
    public GameObject Green; public GameObject Orange; public GameObject Yellow; public GameObject Purple;
    public GameObject LargeBox; public GameObject MediumBox; public GameObject SmallBox;
    public GameObject Radio; public GameObject Bag; public GameObject Ball; public GameObject Slipper;
    public GameObject Pencil; public GameObject WateringCan; public GameObject sadPillow; public GameObject Phone;

    private PhotonView photonView;

    private GameObject OBJCordinate;


    // Line object 
    public GameObject Line;

    // Audio source for playing sound effects
    public AudioSource audioSource;

    // Arrays of possible positions for different objects
    private Vector3[] objManager = new Vector3[]
   {
        new Vector3(25f, 25f, 25f),
        new Vector3(25f, 25f, 25f),
   };
    private Vector3[] redChair = new Vector3[]
   {
        new Vector3(49.766f, 7.637f, 17.446f),
        new Vector3(27.48f, 1.37f, 67.34f),
   };
    private Vector3[] plate = new Vector3[]
    {
        new Vector3(50.021f, 7.436f, 13.914f),
        new Vector3(29.575f, 3.091f, -41.069f),
    };
    private Vector3[] pillowH = new Vector3[]
    {
        new Vector3(82.02351f, 9.121099f, 0.8273819f),
        new Vector3(86.41f, 9.177f, 36.8f),
    };
    private Vector3[] fork = new Vector3[]
    {
        new Vector3(50.32991f, 8.719487f, 20.5955f),
        new Vector3(15.08f, 1.422f, -36.147f),
    };
    private Vector3[] spoon = new Vector3[]
    {
        new Vector3(50.575f, 8.736f, 20.757f),
        new Vector3(50.63f, 8.736f, 29.4f),
    };
    private Vector3[] knife = new Vector3[]
    {
        new Vector3(50.599f, 8.719487f, 20.883f),
        new Vector3(86.007f, 10.373f, 56.945f),
    };
    private Vector3[] hat = new Vector3[]
    {
        new Vector3(85.31f, 9.126f, 37.648f),
        new Vector3(0.046f, 1.31f, -37.331f),
    };
    private Vector3[] sword = new Vector3[]
    {
        new Vector3(82.60853f, 8.433764f, 36.10616f),
        new Vector3(73.54f, 7.92f, 9.81f),
    };
    private Vector3[] glassess = new Vector3[]
    {
        new Vector3(82.22437f, 9.277376f, 36.28313f),
        new Vector3(29.426f, 1.403f, -40.799f),
    };
    private Vector3[] computer = new Vector3[]
   {
        new Vector3(81.75115f, 9.704586f, 56.36886f),
        new Vector3(72.981f, 8.319f, 67.003f),
        new Vector3(29.81f, 6.06f, 66.89f)
   };
    private Vector3[] calcu = new Vector3[]
    {
        new Vector3(83.07209f, 9.691993f, 56.08224f),
        new Vector3(49.98f, 8.7191f, 25.831f),
        new Vector3(30.057f, 2.352f, -40.94f)
    };
    private Vector3[] brownBook = new Vector3[]
    {
        new Vector3(85.86394f, 9.270824f, 4.318f),
        new Vector3(60.598f, 8.758f, 7.851f),
    };
    private Vector3[] redBook = new Vector3[]
    {
        new Vector3(83.06701f, 9.278195f, 35.90474f),
        new Vector3(85.86394f, 9.270824f, 4.318f),
    };
    private Vector3[] blueBook = new Vector3[]
    {
        new Vector3(82.1f, 9.13f, 2.293f),
        new Vector3(29.066f, 3.859f, 66.833f),
    };
    private Vector3[] greenBook = new Vector3[]
    {
        new Vector3(72.89f, 10.02f, 6.63f),
        new Vector3(34.199f, 1.53f, 66.84f),
    };
    private Vector3[] orangeBook = new Vector3[]
    {
        new Vector3(85.1582f, 9.734f, 56.15f),
        new Vector3(36.963f, 3.574f, -35.85f),
    };
    private Vector3[] yellowBook = new Vector3[]
    {
        new Vector3(73.34819f, 8.018076f, 2.08f),
        new Vector3(71.75f, 7.99f, 2.394f),
    };
    private Vector3[] purpleBook = new Vector3[]
   {
        new Vector3(34.51f, 2.53f, -40.38f),
        new Vector3(81.73f, 9.2f, 0.639f),
   };
    private Vector3[] possiblePositions = new Vector3[]
    {
        new Vector3(3.212101f, 1.381771f, -35.789f),
        new Vector3(-1.8f, 1.381771f, -37.98f),
    };
    private Vector3[] secondObjectPositions = new Vector3[]
    {
        new Vector3(2.28f, 1.381771f, -39.79728f),
        new Vector3(-2.33f, 1.381771f, -39.79728f),
    };
    private Vector3[] thirdObjectPositions = new Vector3[]
    {
        new Vector3(-2f, 1.381771f, -35.83f),
        new Vector3(81.53f, 7.98f, -16.775f),
    };
    private Vector3[] radio = new Vector3[]
   {
        new Vector3(15.22907f, 1.670282f, -37.024f),
        new Vector3(80.478f, 8.147f, 38.373f),
   };
    private Vector3[] bag = new Vector3[]
   {
        new Vector3(33.756f, 3.886f, 66.385f),
        new Vector3(73.12132f, 8.034624f, 6.135824f),
   };
    private Vector3[] ball = new Vector3[]
    {
        new Vector3(86.6f, 8.316042f, -11.83404f),
        new Vector3(36.77f, 1.674f, 67.075f),
        new Vector3(80.836f, 8.316042f, 54.05f)
    };

    private Vector3[] slipper = new Vector3[]
    {
        new Vector3(81.41604f, 7.841867f, -1.08615f),
        new Vector3(30.642f, 1.45f, 66.69f),
        new Vector3(86.04f, 7.94f, -12.839f)
    };
    private Vector3[] wateringcan = new Vector3[]
   {
        new Vector3(5.210814f, 1.94f, 30.90473f),
        new Vector3(34.25f, 1.971f, -36.369f),
   };

    private Vector3[] pencil = new Vector3[]
   {
        new Vector3(23.92394f, 1.98f, 4.31f),
   };
    private Vector3[] pillowS = new Vector3[]
   {
        new Vector3(26.5585f, 1.446077f, -41.06846f),
        new Vector3(15.18f, 1.491f, -39.612f),
   };
    private Vector3[] phone = new Vector3[]
  {
        new Vector3(73.06637f, 7.976323f, 0.6416251f),
        new Vector3(55.37f, 8.713f, 8.486f),
  };

    // Audio clips for different sound effects
    public AudioClip tenSeconds;
    public AudioClip coinSound;
    public AudioClip gameMusic;

    // Player's score
    public int score = 0;
    public TMP_Text GuideText;

    // Flag to track if guidance text has been shown
    private bool guideTextShown = true;
    // UI elements for displaying score and selected objects
    public TMP_Text scoreText;
    public TMP_Text selectedObjectsText;

    // List of selected objects
    public List<GameObject> selectedObjects = new List<GameObject>();

    public GameObject GameManagerr;
    // Flag to show or hide the list of selected objects
    private bool showSelectedObjects = false;
    public Camera Kamera;

    void Start()
    {



        GameManagerr = GameObject.Find("Alp Game");
        GameManagerr.GetComponent<GameFourManager>().Starters.Add(gameObject);
        photonView = GetComponent<PhotonView>();
        OBJCordinate = GameObject.Find("CoordinateManager");
        

        LargeBox = GameObject.Find("Box Large");
        MediumBox = GameObject.Find("Box Medium");
        SmallBox = GameObject.Find("Box Small");
        Ball = GameObject.Find("Ball");
        Bag = GameObject.Find("Bag");
        Hat = GameObject.Find("Hat");
        Calculator = GameObject.Find("Calculator");
        bluePlate = GameObject.Find("Plate Cyan");
        Radio = GameObject.Find("Radio");
        Slipper = GameObject.Find("Slipper");
        Sword = GameObject.Find("Sword");
        Brown = GameObject.Find("Book Brown");
        Red = GameObject.Find("Book Red");
        Blue = GameObject.Find("Book Blue");
        Green = GameObject.Find("Book Green");
        Orange = GameObject.Find("Book Orange");
        Yellow = GameObject.Find("Book Yellow");
        chair = GameObject.Find("Chair Red");
        Computer = GameObject.Find("Laptop");
        happyPillow = GameObject.Find("Pillow Happy");
        sadPillow = GameObject.Find("Pillow Sad");
        WateringCan = GameObject.Find("Watering Can");
        Pencil = GameObject.Find("Pencil");
        Fork = GameObject.Find("Fork");
        Knife = GameObject.Find("Knife");
        Spoon = GameObject.Find("Spoon");
        Purple = GameObject.Find("Book Purple");
        Phone = GameObject.Find("Phone");
        Glassess = GameObject.Find("Glassese");

        photonView = GetComponent<PhotonView>();



        // Initialize audio source if it's not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Hide the selected objects list initially
        selectedObjectsText.gameObject.SetActive(false);

        // Set initial score text
        scoreText.text = "Point " + score;





        List<GameObject> allObjects = new List<GameObject>()
        {
            chair, bluePlate, happyPillow, Fork, Spoon, Knife, Hat, Sword, Glassess, Computer,
            Calculator, Brown, Red, Blue, Green, Orange, Yellow, Purple , LargeBox, MediumBox, SmallBox,
            Radio, Bag, Ball, Slipper, WateringCan, sadPillow , Phone
        };
        if(GameManagerr.GetComponent<GameFourManager>().allObjectsMain.Count == 0)
        {
            GameManagerr.GetComponent<GameFourManager>().allObjectsMain.AddRange(allObjects);
        }


        for (int i = 0; i < 15; i++)
        {
            selectedObjects.Add(allObjects[i]);
        }

        selectedObjects.Sort((a, b) => string.Compare(a.name, b.name));


        string selectedNames = "";
        foreach (GameObject obj in selectedObjects)
        {
            selectedNames += obj.name + "\n";
        }


        // Set the text of the selected objects UI element
        selectedObjectsText.text = selectedNames;

        // Set font size of the selected objects text
        selectedObjectsText.fontSize = 9;

        // Select a random index from the fork array
        int randomIndex = Random.Range(0, fork.Length);

        // Set the initial position of the object manager
        transform.position = objManager[randomIndex];

        // Check if all game objects are assigned
        if (Fork != null && Spoon != null && Knife != null && Hat != null && Sword != null && Glassess != null && chair != null && bluePlate != null && happyPillow != null
            && Computer != null && Calculator != null && Brown != null && Red != null && Blue != null && Green != null && Orange != null && Yellow != null && Purple != null
            && LargeBox != null && MediumBox != null && SmallBox != null && Radio != null && Bag != null && Ball != null && Slipper != null && WateringCan != null
            && Pencil != null && sadPillow != null && Phone != null)
        {
            // Set the initial positions of all game objects based on the random index
            Fork.transform.position = fork[randomIndex]; Spoon.transform.position = spoon[randomIndex]; Knife.transform.position = knife[randomIndex];
            Hat.transform.position = hat[randomIndex]; Sword.transform.position = sword[randomIndex]; Glassess.transform.position = glassess[randomIndex];
            chair.transform.position = redChair[randomIndex]; bluePlate.transform.position = plate[randomIndex]; happyPillow.transform.position = pillowH[randomIndex];
            Computer.transform.position = computer[randomIndex]; Calculator.transform.position = calcu[randomIndex]; Brown.transform.position = brownBook[randomIndex];
            Red.transform.position = redBook[randomIndex]; Blue.transform.position = blueBook[randomIndex]; Green.transform.position = greenBook[randomIndex];
            Orange.transform.position = orangeBook[randomIndex]; Yellow.transform.position = yellowBook[randomIndex]; Purple.transform.position = purpleBook[randomIndex];
            LargeBox.transform.position = possiblePositions[randomIndex]; MediumBox.transform.position = secondObjectPositions[randomIndex];
            SmallBox.transform.position = thirdObjectPositions[randomIndex]; Radio.transform.position = radio[randomIndex];
            Bag.transform.position = bag[randomIndex]; Ball.transform.position = ball[randomIndex]; Slipper.transform.position = slipper[randomIndex];
            WateringCan.transform.position = wateringcan[randomIndex]; Pencil.transform.position = pencil[randomIndex];
            sadPillow.transform.position = pillowS[randomIndex]; Phone.transform.position = phone[randomIndex];
        }



  
    }


   


        [PunRPC]
        void MyScorePublish(int scoreparameter)
        {

            score = scoreparameter;
        
        }
        [PunRPC]
        void Winner(string name)
        {
            if (!GameManagerr.GetComponent<GameFourManager>().IsWin)
            {
               // GameManagerr.GetComponent<GameTwoManager>().Win.gameObject.SetActive(true);
               // GameManagerr.GetComponent<GameTwoManager>().Win.text = "Winner is " + name;
            }
            if (!GameManagerr.GetComponent<GameFourManager>().Ranking.Contains(gameObject))
            {
                GameManagerr.GetComponent<GameFourManager>().Ranking.Add(gameObject);
            }

        }

    void Update()
    {

            if(gameObject.GetComponent<PhotonView>().IsMine)
            {
                photonView.RPC("MyScorePublish", RpcTarget.All, score);
            }
       


        if (OBJCordinate.GetComponent<ObjectCoordinateMaanager>().countdownTime <= 1)
        {
            photonView.RPC("Winner", RpcTarget.All, photonView.OwnerActorNr.ToString());
        }





        if (Input.GetMouseButtonDown(0)) // If left mouse button is clicked
        {
            // Create a ray from the mouse position
            RaycastHit hit;
            Ray ray = Kamera.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast to check if the ray hits anything
            if (Physics.Raycast(ray, out hit))
            {
                // Handle the clicked object
                if (selectedObjects.Contains(hit.collider.gameObject))
                {
                    // Play the coin sound
                    if (coinSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(coinSound);
                    }

                    // Hide the clicked object and then show it again (this is likely a visual effect for feedback)
                    hit.collider.gameObject.SetActive(false);


                    // Increase the score and update the score text
                    score++;
                    scoreText.text = "Point: " + score;

                    // Remove the object from the list of selected objects
                    selectedObjects.Remove(hit.collider.gameObject);

                    // Update the text that displays the list of selected objects
                    UpdateSelectedObjectsText();
                }

                // Handle the pencil object
                if (hit.collider.gameObject == Pencil)
                {
                    // Generate a random Z coordinate
                    float randomZ = Random.Range(-3f, 45f);

                    // Generate a random X coordinate
                    float randomX = Random.Range(9.5f, 30.5f);

                    // Teleport the pencil to the new random coordinates
                    Pencil.transform.position = new Vector3(randomX, Pencil.transform.position.y, randomZ);
                }

            }
        }

        // Handle Tab key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the visibility of the selected objects list
            showSelectedObjects = !showSelectedObjects;
            selectedObjectsText.gameObject.SetActive(showSelectedObjects);

            if (guideTextShown)
            {
                GuideText.text = "";
                guideTextShown = false;
            }
        }

    }


    private void UpdateSelectedObjectsText()
    {
        // Create a string to store the names of selected objects
        string selectedNames = "";

        // Loop through all selected objects
        foreach (GameObject obj in selectedObjects)
        {
            // Add the object's name to the string
            selectedNames += obj.name + "\n";
        }

        // Set the text of the selected objects UI element
        selectedObjectsText.text = selectedNames;
    }

}
