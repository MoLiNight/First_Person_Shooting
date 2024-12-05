using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
    public int index = 0;
    public float timer = 10.0f;

    Skybox skybox;
    public Material[] materials;

    private Light sceneLight;
    public Color[] lightColors;
    public GameObject lightObject;
    
    // Start is called before the first frame update
    void Start()
    {
        skybox = GetComponent<Skybox>();
        sceneLight = lightObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if( timer < 0)
        {
            timer = 10.0f;
            index = (index + 1) % materials.Length;
            skybox.material = materials[index];
            sceneLight.color = lightColors[index];
        }
    }
}
