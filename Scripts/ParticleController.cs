using System.Drawing.Drawing2D;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private float destroyTime = 1.5f;

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
