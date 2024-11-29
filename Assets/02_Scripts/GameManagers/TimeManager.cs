using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] Light sun;
    [SerializeField] Light moon;
    [SerializeField] AnimationCurve lightIntensityCurve;
    [SerializeField] float maxSunIntensity = 1;
    [SerializeField] float maxMoonIntensity = 0.5f;

    [SerializeField] Color dayAmbientLight;
    [SerializeField] Color nightAmbientLight;
    [SerializeField] Volume volume;
    [SerializeField] Material skyboxMaterial;

    ColorAdjustments colorAdjustments;

    [SerializeField] TimeSettings timeSettings;
    [SerializeField] TempGameMgr tempGameMgr;
    [SerializeField] EscapeMgr escapeMgr;

    TimeService service;
    IEnumerator coroutine;
    public bool IsDay { get; private set; }
    private void Start()
    {
        service = new TimeService(timeSettings);
        //volume.profile.TryGet(out colorAdjustments);
        coroutine = SpawnAggressiveAnimal();
        service.OnHourChange += time => CheckTime(time);
        service.OnSunrise += () => CheckDay(true);
        service.OnSunset += () => CheckDay(false);
    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        UpdateSkyBlend();
    }

    void UpdateSkyBlend()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.up);
        float blend = Mathf.Lerp(0, 1, lightIntensityCurve.Evaluate(dotProduct));
        skyboxMaterial.SetFloat("_Blend", blend);
    }

    void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Lerp(0, maxSunIntensity, lightIntensityCurve.Evaluate(dotProduct));
        moon.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightIntensityCurve.Evaluate(dotProduct));

        if (colorAdjustments == null) return;
        colorAdjustments.colorFilter.value = Color.Lerp(nightAmbientLight, dayAmbientLight, lightIntensityCurve.Evaluate(dotProduct));
    }

    void RotateSun()
    {
        float rotation = service.CalcluateSunAngle();
        sun.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.right);
    }

    void UpdateTimeOfDay()
    {
        service.UpdateTime(Time.deltaTime);
        //if (timeText != null)
        //{
        //    timeText.text = service.CurTime.ToString("hh:mm");
        //}
    }

    void CheckTime(int i)
    {
        if (i == 20)
        {
            if (coroutine != null)
            {
                StartCoroutine(coroutine);
            }
        }

        if (i == 5)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        if(i == 7)
        {
            escapeMgr.ComeShip();
        }
        if(i == 12)
        {
            escapeMgr.LeaveShip();
        }
    }

    void CheckDay(bool isDay)
    {
        if (isDay)
        {
            tempGameMgr.OnDay();
            IsDay = true;
        }
        else
        {
            tempGameMgr.OnNight();
            IsDay = false;
        }
    }

    IEnumerator SpawnAggressiveAnimal()
    {
        while (true)
        {
            //플레이어 근처에 선공 동물 소환
            tempGameMgr.SpawnAnimals();
            yield return new WaitForSeconds(25f);
        }
    }
}
