using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    string temp;
    public Light light;
    public RawImage capa01;
    public RawImage capa02;
    public RawImage capa03;
    public RawImage capa04;
    public float offset;
    private float localoffset;

    void Update()
    {
        capa01.uvRect = new Rect(capa01.uvRect.x + offset * Time.deltaTime, 0, 2, 1);
        capa02.uvRect = new Rect(capa02.uvRect.x + (float)((double)localoffset / 5 * 4) * Time.deltaTime, 0, 2, 1);
        capa03.uvRect = new Rect(capa03.uvRect.x + (float)((double)localoffset / 5 * 3) * Time.deltaTime, 0, 2, 1);
        capa04.uvRect = new Rect(capa04.uvRect.x + ((float)((double)localoffset / 5 * 1)) * Time.deltaTime, 0, 2, 1);

        if (GameManager.sharedInstance.currentTime.ToString() != temp)
        {
            if (GameManager.sharedInstance.currentTime == TimeOfDay.Sunlight)
            {
                Sunlight();
            }
            else if (GameManager.sharedInstance.currentTime == TimeOfDay.Sunset)
            {
                Sunset();
            }
            else if (GameManager.sharedInstance.currentTime == TimeOfDay.Night)
            {
                Night();
            }
            else if (GameManager.sharedInstance.currentTime == TimeOfDay.Sunrise)
            {
                Sunrise();
            }
        }
        temp = GameManager.sharedInstance.currentTime.ToString();
    }

    private void Sunlight()
    {
        //light.color.
    }

    private void Sunset()
    {
        light.intensity = Mathf.Lerp(1, 0.1f, GameManager.sharedInstance.dayDuration / 6);
    }

    private void Night()
    {

    }

    private void Sunrise()
    {
        light.intensity = Mathf.Lerp(0.1f, 1, GameManager.sharedInstance.dayDuration / 6);
    }
}
