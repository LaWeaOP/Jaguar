using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public Light light;
    public RawImage capa01;
    public RawImage capa02;
    public RawImage capa03;
    public RawImage capa04;
    public float offset;
    private double localoffset;
    public float transition;

    private void Start()
    {
        localoffset = 0;
        light.intensity = 1;
    }
    void Update()
    {
        transition = 100 / GameManager.sharedInstance.dayDuration / 6 * GameManager.sharedInstance.time;
        capa01.uvRect = new Rect(capa01.uvRect.x + (float)localoffset * Time.deltaTime, 0, 2, 1);
        capa02.uvRect = new Rect(capa02.uvRect.x + (float)((double)localoffset / 5 * 4) * Time.deltaTime, 0, 2, 1);
        capa03.uvRect = new Rect(capa03.uvRect.x + (float)((double)localoffset / 5 * 3) * Time.deltaTime, 0, 2, 1);
        capa04.uvRect = new Rect(capa04.uvRect.x + ((float)((double)localoffset / 5 * 1)) * Time.deltaTime, 0, 2, 1);

        if (GameManager.sharedInstance.currentTime == TimeOfDay.Sunset)
        {
            localoffset = Mathf.Lerp(0, offset, transition/2);
            light.intensity = Mathf.Lerp(1, 0.1f, transition/2);
        }
        else if (GameManager.sharedInstance.currentTime == TimeOfDay.Sunrise)
        {
            localoffset = Mathf.Lerp(offset, 0, transition/2);
            light.intensity = Mathf.Lerp(0.1f, 1, transition/2);
        }
    }
}