using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    private Resolution[] allowedRes;

    public void VSyncCount()
    {
        QualitySettings.vSyncCount = 0; // 0-4, 0 is use target framerate
        
    }

    public void LimitFramerate(int _target)
    {
        Application.targetFrameRate = _target;
        //Application.targetFrameRate = -1; //gives platforms default framerate

    }
    
    /// <summary>
    /// Gets the screens resolutions, displays them in the dropdown, sets the default and saves the choice.
    /// </summary>
    private void Resolutions()
    {
        allowedRes = Screen.resolutions;
        
        List<string> resOptions = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < allowedRes.Length; i++)
        {
            string option = allowedRes[i].width + "x" + allowedRes[i].height;
            resOptions.Add(option);

            if(allowedRes[i].width == Screen.currentResolution.width && allowedRes[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
	                      
        resolutionDropdown.AddOptions(resOptions);
        // Loads any settings saved in playerprefs, or sets it to the current screen resolution
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resIndex = PlayerPrefs.GetInt("Resolution");
            resolutionDropdown.value = resIndex;
            resolutionDropdown.RefreshShownValue();
            SetResolution(resIndex);
        }
        else
        {
            resolutionDropdown.value = currentResolution;
            resolutionDropdown.RefreshShownValue();
        }
    }
    
    /// <summary>
    /// Setting the chosen resolution from the dropdown on the UI
    /// </summary>
    public void SetResolution(int resIndex)
    {
        PlayerPrefs.SetInt("Resolution", resIndex);
        Resolution resolution = allowedRes[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Loads any saved player prefs
    /// </summary>
    public void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resIndex = PlayerPrefs.GetInt("Resolution");
            resolutionDropdown.value = resIndex;
            resolutionDropdown.RefreshShownValue();
            SetResolution(resIndex);            
            
        }
    }

    private void Awake()
    {
        Resolutions();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
