using UnityEngine;
public class Lightswitch : MonoBehaviour
{
    public Light lights; 
    public float activationRange = 3f; 
    public AudioClip switchSound; 
    private bool isLightsOn = false; 
    private AudioSource audioSource; 

    void Start()
    {
        lights.enabled = false;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = switchSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (Vector3.Distance(transform.position, hit.point) <= activationRange)
                {
                    ToggleLights();
                }
            }
        }
    }

    void ToggleLights()
    {
        isLightsOn = !isLightsOn; 

        lights.enabled = isLightsOn;

        audioSource.Play();

        if (isLightsOn)
        {
            Debug.Log("Lights turned on.");
        }
        else
        {
            Debug.Log("Lights turned off.");
        }
    }
}