using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float blend;
    private Animator animator;

    private bool waitDestroy = false;
    private float destroyTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", blend);
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Target>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitDestroy)
        {
            destroyTime -= Time.deltaTime;
        }
        if(destroyTime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShootToTarget()
    {
        GameObject particle = Instantiate(Resources.Load<GameObject>("Prefabs/ParticleSystem"));
        particle.transform.position = gameObject.transform.position;

        waitDestroy = true;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Target>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }
}
