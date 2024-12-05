using UnityEngine;
using UnityEngine.UI;

public class ShootAreaController : MonoBehaviour
{
    public GameObject AirWall;
    public GameObject TargetList;

    private bool gameFinished;
    private bool running;

    private Material waiting;
    private Material shooting;
    private Material victory;
    private GameObject TipText;

    // Start is called before the first frame update
    void Start()
    {
        running = false;
        gameFinished = false;

        TipText = GameObject.Find("TipText");
        waiting = Resources.Load<Material>("Materials/Waiting");
        shooting = Resources.Load<Material>("Materials/Shooting");
        victory = Resources.Load<Material>("Materials/Victory");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameFinished == false)
        {
            if (running == true)
            {
                bool finished = true;
                foreach (Transform child in TargetList.transform)
                {
                    if (child.gameObject.activeSelf == true)
                    {
                        finished = false;
                        break;
                    }
                }
                if (finished)
                {
                    gameFinished = true;
                    AirWall.SetActive(false);
                    TipText.GetComponent<Text>().text = "";
                    gameObject.GetComponent<Renderer>().material = victory;
                }
            }
            if (running == true && Input.GetKeyDown(KeyCode.P))
            {
                running = false;
                AirWall.SetActive(false);

                foreach (Transform child in TargetList.transform)
                {
                    child.gameObject.SetActive(true);
                }
                TargetList.SetActive(false);

                TipText.GetComponent<Text>().text = "";
                gameObject.GetComponent<Renderer>().material = waiting;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameFinished == false)
        {
            AirWall.SetActive(true);
            TargetList.SetActive(true);
            TipText.GetComponent<Text>().text = "按键 P 中断挑战";
            running = true;
            gameObject.GetComponent<Renderer>().material = shooting;
        }
    }
}
