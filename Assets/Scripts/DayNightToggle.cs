using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DayNightToggle : MonoBehaviour
{
    // A boolean to track if it's day or night
    public bool isDay = true; // Default to Day

    // Reference to the light you want to control
    public Light sceneLight; // Drag the light from the scene here

    // Skyboxes for Day and Night
    public Material daySkybox; // Drag your day skybox material here
    public Material nightSkybox; // Drag your night skybox material here

    // Update is called once per frame
    void Update()
    {
        SetEnvironment();
    }

    // Method to set the environment based on day or night
    public void SetEnvironment()
    {
        if (isDay)
        {
            // Set the environment for Day (e.g., bright lighting)
            RenderSettings.ambientLight = Color.white; // Daytime light

            if (sceneLight != null)
            {
                sceneLight.enabled = true; // Enable the light during the day
            }

            if (daySkybox != null)
            {
                RenderSettings.skybox = daySkybox; // Set the day skybox
            }

           
        }
        else
        {
            // Set the environment for Night (e.g., dim lighting)
            RenderSettings.ambientLight = Color.black; // Nighttime light

            if (sceneLight != null)
            {
                sceneLight.enabled = false; // Disable the light during the night
            }

            if (nightSkybox != null)
            {
                RenderSettings.skybox = nightSkybox; // Set the night skybox
            }

            
        }
    }
}
