using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown dropdown;
    Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        Resolution[] resolution = Screen.resolutions;
        resolutions = resolution.Distinct().ToArray();
        string[] stringResolution = new string[resolutions.Length];
        for (int i = 0; i < resolutions.Length; i++)
        {
            //stringResolution[i] = resolutions[i].ToString();
            stringResolution[i] = resolutions[i].width.ToString() + " X " + resolutions[i].height.ToString();
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(stringResolution.ToList());
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
    }

    public void SetResolution()
    {
        Screen.SetResolution(resolutions[dropdown.value].width, resolutions[dropdown.value].height, true);
    }
}
