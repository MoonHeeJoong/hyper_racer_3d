using System.Threading.Tasks;
using UnityEngine;

public class CarLightController : MonoBehaviour
{ 
    Light[] carFrontLights;
    Light[] carBackLights;
    float startLightIntensity = 0.5f;
    float maxLightIntensity = 50f;
    float currentLightIntensity = 0.5f;
    float lightChangeTerm = 1f;

    float starBackLightIntensity = 0.0f;
    float maxBackLightIntensity = 0.1f;
    float currentBackLightIntensity = 0.0f;


    bool isLightOn = false;
    void Start()
    {
        carFrontLights = transform.Find("FrontLights").GetComponentsInChildren<Light>();
        carBackLights = transform.Find("BackLights").GetComponentsInChildren<Light>();

        foreach(Light light in carFrontLights)
        {
            light.enabled = false;
            light.intensity = startLightIntensity;
        }

        foreach(Light light in carBackLights)
        {
            light.enabled = false;
            light.intensity = starBackLightIntensity;
        }
    }

    void FixedUpdate(){
        if(GameManager.Instance.IsNight())
        {
            if(!isLightOn)
            {
                isLightOn = true;
                EnableFrontLights();
                EnableBackLights();
            }
        }
        else
        {
            if(isLightOn)
            {
                isLightOn = false;
                DisableFrontLights();
                DisableBackLights();
            }
        }
    }

    async void EnableFrontLights()
    {
        float elapsedTime = 0;

        while(elapsedTime < lightChangeTerm)
        {
            elapsedTime += Time.deltaTime;
            currentLightIntensity = Mathf.Lerp(startLightIntensity, maxLightIntensity, elapsedTime / lightChangeTerm);
            foreach(Light light in carFrontLights)
            {
                if(light == null) return;
                light.enabled = true;
                light.intensity = currentLightIntensity;
            }
            await Task.Yield();

            if(!isLightOn)
            {
                break;
            }
        }
    }


    async void DisableFrontLights()
    {
        float elapsedTime = 0;

        while(elapsedTime < lightChangeTerm)
        {
            elapsedTime += Time.deltaTime;
            currentLightIntensity = Mathf.Lerp(maxLightIntensity, startLightIntensity, elapsedTime / lightChangeTerm);
            foreach(Light light in carFrontLights)
            {
                if(light == null) return;
                light.intensity = currentLightIntensity;
            }
            await Task.Yield();

            if(isLightOn)
            {
                break;
            }
        }

        foreach(Light light in carFrontLights)
        {
            light.enabled = false;
        }
    }

    async void EnableBackLights()
    {
        float elapsedTime = 0;

        while(elapsedTime < lightChangeTerm)
        {
            elapsedTime += Time.deltaTime;
            currentBackLightIntensity = Mathf.Lerp(starBackLightIntensity, maxBackLightIntensity, elapsedTime / lightChangeTerm);
            foreach(Light light in carBackLights)
            {
                if(light == null) return;
                light.enabled = true;
                light.intensity = currentBackLightIntensity;
            }
            await Task.Yield();

            if(!isLightOn)
            {
                break;
            }
        }
    }

    async void DisableBackLights()
    {
        float elapsedTime = 0;

        while(elapsedTime < lightChangeTerm)
        {
            elapsedTime += Time.deltaTime;
            currentBackLightIntensity = Mathf.Lerp(maxBackLightIntensity, starBackLightIntensity, elapsedTime / lightChangeTerm);
            foreach(Light light in carBackLights)
            {
                if(light == null) return;
                light.intensity = currentBackLightIntensity;
            }
            await Task.Yield();

            if(isLightOn)
            {
                break;
            }
        }

        foreach(Light light in carBackLights)
        {
            light.enabled = false;
        }
    }
}
