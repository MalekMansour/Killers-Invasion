using UnityEngine;
using UnityEngine.UI;

public class Website : MonoBehaviour
{
    public InputField searchBar; // Reference to the search bar input field
    public string urlPrefix = "https://www.thedarkwiki.com/redirect/main/666/"; // URL prefix

    private bool isToggledOn = false;

    void Start()
    {
        if (searchBar == null)
        {
            Debug.LogError("Search bar reference is missing!");
        }
    }

    void Update()
    {
        // Check if the canvas is toggled on
        if (isToggledOn)
        {
            // Generate a random URL
            string randomUrl = GenerateRandomUrl();
            searchBar.text = randomUrl;
        }
    }

    public void ToggleWebsite(bool toggle)
    {
        isToggledOn = toggle;
    }

    private string GenerateRandomUrl()
    {
        string randomNumbers = Random.Range(100000000, 999999999).ToString();
        string randomUrl = urlPrefix + randomNumbers + ".onion";
        return randomUrl;
    }
}

