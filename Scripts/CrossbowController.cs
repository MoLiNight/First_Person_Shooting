using UnityEngine;
using UnityEngine.UI;

public class CrossbowController : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;

    private int arrowNum;

    private float blend;
    public GameObject arrowInModel;
    private Transform arrowOriginTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        blend = 1;
        arrowNum = 5;
        arrowOriginTransform = arrowInModel.transform;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 获取当前动画状态信息
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonUp(0))
        {
            if (animatorStateInfo.IsName("Filling"))
            {
                animator.SetBool("Filling", false);
            }
        }

        if (animatorStateInfo.IsName("Empty"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (arrowNum > 0)
                {
                    Filling();
                    SetArrowNum(--arrowNum);
                }
            }
        }
        else
        if (animatorStateInfo.IsName("Filling"))
        {
            blend = Mathf.Min(1.0f, animatorStateInfo.normalizedTime);
            animator.SetFloat("Blend", blend);
            if (animatorStateInfo.normalizedTime >= 1.0f)
            {
                animator.SetBool("Filling", false);
                audioSource.Pause();
            }
            else
            if (Input.GetMouseButton(0) == false)
            {
                animator.SetBool("Filling", false);
                audioSource.Pause();
            }
        }
        else
        if (animatorStateInfo.IsName("Hold"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootArrow();
            }
        }
        else
        if (animatorStateInfo.IsName("Shoot"))
        {
            if (animatorStateInfo.normalizedTime >= 1.0f)
            {
                animator.SetBool("Shooting", false);
            }
        }
    }

    void Filling()
    {
        arrowInModel.SetActive(true);
        animator.SetBool("Filling", true);

        AudioClip audioClip = Resources.Load<AudioClip>("Audios/Filling");
        audioSource.clip = audioClip;
        audioSource.pitch = 1.25f;
        audioSource.Play();
    }

    void ShootArrow()
    {
        animator.SetBool("Shooting", true);
        arrowInModel.SetActive(false);

        AudioClip audioClip = Resources.Load<AudioClip>("Audios/Shooting");
        audioSource.clip = audioClip;
        audioSource.pitch = 1;
        audioSource.Play();

        GameObject newArrow = GameObject.Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;
        newArrow.transform.position = arrowOriginTransform.position;
        newArrow.transform.rotation = arrowOriginTransform.rotation;

        Rigidbody rigidbody = newArrow.GetComponent<Rigidbody>();
        rigidbody.AddForce(newArrow.transform.forward * 0.3f, ForceMode.Impulse);
    }

    public void SetArrowNum(int num)
    {
        arrowNum = num;

        GameObject ArrowNumText = GameObject.Find("ArrowNumText");
        ArrowNumText.GetComponent<Text>().text = "ArrowNum: " + arrowNum.ToString();
    }
}
