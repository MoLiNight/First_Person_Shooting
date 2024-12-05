using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool is_normal;
    public GameObject normalCamera;
    public GameObject scopedCamera;

    // Start is called before the first frame update
    void Start()
    {
        is_normal = true;
        normalCamera.SetActive(is_normal);
        scopedCamera.SetActive(!is_normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            is_normal = !is_normal;
            normalCamera.SetActive(is_normal);
            scopedCamera.SetActive(!is_normal);
        }
    }
}
