using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private AudioSource audioSource;

    private bool waitDestroy = false;
    private float destroyTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitDestroy)
        {
            destroyTime -= Time.deltaTime;
        }
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        waitDestroy = true;

        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;

        if (collision.gameObject.CompareTag("StaticTarget") || collision.gameObject.CompareTag("KineticTarget"))
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Audios/ShootToTarget");
            audioSource.clip = audioClip;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("KineticTarget"))
        {
            TargetController controller = collision.gameObject.GetComponent<TargetController>();
            controller.ShootToTarget();

            destroyTime = 1.5f;
            ScoreController scoreController = ScoreController.GetInstance();
            scoreController.CalculateScore(collision.gameObject, gameObject);
        }
    }
}
