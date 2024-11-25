using System;
using System.Collections;
using UnityEngine;

public class TimeService : MonoBehaviour
{
    readonly TimeSettings settings;
    DateTime curTime;
    readonly TimeSpan sunriseTime;
    readonly TimeSpan sunsetTime;

    public DateTime CurTime => curTime;

    public event Action OnSunrise = delegate { };
    public event Action OnSunset = delegate { };
    public event Action<int> OnHourChange = delegate { };

    readonly Observable<bool> isDayTime;
    readonly Observable<int> curHour;

    //public IEnumerator coroutine;

    public TimeService(TimeSettings settings)
    {
        this.settings = settings;
        curTime = DateTime.Now + TimeSpan.FromHours(settings.startHour);
        sunriseTime = TimeSpan.FromHours(settings.sunriseHour);
        sunsetTime = TimeSpan.FromHours(settings.sunsetHour);

        isDayTime = new Observable<bool>(IsDayTime());
        curHour = new Observable<int>(curTime.Hour);

        isDayTime.ValueChanged += day => (day ? OnSunrise : OnSunset)?.Invoke();
        //curHour.ValueChanged += _ => OnHourChange?.Invoke(_);
        //curHour.AddListener(OnHourChange);
    }

    public void UpdateListner()
    {
        curHour.AddListener(OnHourChange);
    }

    public void UpdateTime(float deltaTime)
    {
        curTime = curTime.AddSeconds(deltaTime * settings.timeMultiplier);
        isDayTime.Value = IsDayTime();
        curHour.Value = curTime.Hour;
    }

    public float CalcluateSunAngle()
    {
        bool isDay = IsDayTime();
        float startDegree = isDay ? 0 : 180;
        TimeSpan start = isDay ? sunriseTime : sunsetTime;
        TimeSpan end = isDay ? sunsetTime : sunriseTime;

        TimeSpan totalTime = CalculateDifference(start, end);
        TimeSpan elapsedTime = CalculateDifference(start, curTime.TimeOfDay);

        double percentage = elapsedTime.TotalMinutes / totalTime.TotalMinutes;
        return Mathf.Lerp(startDegree, startDegree + 180, (float)percentage);
    }

    bool IsDayTime() => curTime.TimeOfDay > sunriseTime && curTime.TimeOfDay < sunsetTime;

    TimeSpan CalculateDifference(TimeSpan from, TimeSpan to)
    {
        TimeSpan difference = to - from;
        return difference.TotalHours < 0 ? difference + TimeSpan.FromHours(24) : difference;
    }

}
