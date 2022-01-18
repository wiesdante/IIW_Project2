using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayFX : MonoBehaviour
{
    public ParticleSystem red, blue, green, purple, yellow, orange;

    public void StartSpray(Colors color)
    {
        switch (color)
        {
            case Colors.BLUE:
                if (!blue.isPlaying)
                {
                    blue.Play();
                }
                break;
            case Colors.GREEN:
                if(!green.isPlaying)
                green.Play();
                break;
            case Colors.PURPLE:
                if(!purple.isPlaying)
                purple.Play();
                break;
            case Colors.RED:
                if (!red.isPlaying)
                    red.Play();
                break;
            case Colors.YELLOW:
                if (!yellow.isPlaying)
                    yellow.Play();
                break;
            case Colors.ORANGE:
                if (!orange.isPlaying)
                    orange.Play();
                break;
        }
    }

    public void StopSpray(Colors color)
    {
        switch (color)
        {
            case Colors.BLUE:
                blue.Stop();
                break;
            case Colors.GREEN:
                green.Stop();
                break;
            case Colors.PURPLE:
                purple.Stop();
                break;
            case Colors.RED:
                red.Stop();
                break;
            case Colors.YELLOW:
                yellow.Stop();
                break;
            case Colors.ORANGE:
                orange.Stop();
                break;
        }
    }
}
