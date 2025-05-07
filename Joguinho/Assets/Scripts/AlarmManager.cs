using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public AudioSource alarmSound;

    public void PlayAlarm()
    {
        if (!alarmSound.isPlaying)
            alarmSound.Play();
    }

    public void StopAlarm()
    {
        if (alarmSound.isPlaying)
            alarmSound.Stop();
    }
}
