using UnityEngine;

public class TimeUnitChange : MonoBehaviour
{
    public delegate void TimeChange(int currentSecond);
    public static event TimeChange timeChangeEvent;

    private int _currentMoment = 0;
    private float _currentTime = 0.0f;
    public float loopTime = 0.1f;

    void Start()
    {
        timeChangeEvent?.Invoke(++_currentMoment);
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= loopTime)
        {
            timeChangeEvent?.Invoke(++_currentMoment);
            _currentTime = 0.0f;
        }
    }
}
