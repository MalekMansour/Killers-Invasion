using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WebsiteOpener : MonoBehaviour
{
    public GameObject websitesParent; 
    public GameObject panel;

    public Dictionary<GameObject, Sprite> websiteThumbnails = new Dictionary<GameObject, Sprite>(); 

    private Dictionary<GameObject, GameObject> buttonToWebsiteMap = new Dictionary<GameObject, GameObject>();
    private List<GameObject> availableWebsites = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < websitesParent.transform.childCount; i++)
        {
            GameObject website = websitesParent.transform.GetChild(i).gameObject;
            availableWebsites.Add(website);

            Sprite thumbnail = Resources.Load<Sprite>("Thumbnails/" + website.name);
            if (thumbnail != null)
            {
                websiteThumbnails[website] = thumbnail;
            }
        }

        for (int i = 0; i < panel.transform.childCount; i++)
        {
            GameObject button = panel.transform.GetChild(i).gameObject;
            Button btnComponent = button.GetComponent<Button>();

            if (btnComponent != null)
            {
                btnComponent.onClick.AddListener(() => OnButtonClick(button));
            }
            else
            {
                Debug.LogWarning("No Button component found on: " + button.name);
            }
        }
    }

    void OnButtonClick(GameObject button)
    {
        if (buttonToWebsiteMap.ContainsKey(button))
        {
            OpenWebsite(buttonToWebsiteMap[button]);
        }
        else
        {
            if (availableWebsites.Count > 0)
            {
                int randomIndex = Random.Range(0, availableWebsites.Count);
                GameObject assignedWebsite = availableWebsites[randomIndex];
                availableWebsites.RemoveAt(randomIndex);

                buttonToWebsiteMap[button] = assignedWebsite;
                OpenWebsite(assignedWebsite);
                UpdateButtonThumbnail(button, assignedWebsite);
            }
            else
            {
                Debug.LogWarning("No more websites available to assign.");
            }
        }
    }

    void OpenWebsite(GameObject website)
    {
        for (int i = 0; i < websitesParent.transform.childCount; i++)
        {
            websitesParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        website.SetActive(true);
    }

    void UpdateButtonThumbnail(GameObject button, GameObject website)
    {
        Image imageComponent = button.GetComponentInChildren<Image>();
        if (imageComponent != null && websiteThumbnails.ContainsKey(website))
        {
            imageComponent.sprite = websiteThumbnails[website];
        }
        else
        {
            Debug.LogWarning("Thumbnail not found for: " + website.name);
        }
    }
}
