using UnityEngine;
using System;
using System.Collections;

public class bpmRhythm : MonoBehaviour
{
    public float bpm = 100;

    public static Action onBeat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(bpmCor());
    }

    IEnumerator bpmCor()
    {
        while (true)
        {
            onBeat?.Invoke();
            yield return new WaitForSeconds(60 / bpm);
        }

    }
}
