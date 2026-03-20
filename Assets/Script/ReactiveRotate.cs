using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ReactiveRotate : MonoBehaviour
{
    public float anglePerMove = 15f;
    public float aniDuration = 0.2f;

    void OnEnable()
    {
        //RhythmAnalyser.onBeatDetected += OnBeat;
        bpmRhythm.onBeat += OnBeat;
    }

    void OnDisable()
    {
        //RhythmAnalyser.onBeatDetected -= OnBeat;
        bpmRhythm.onBeat -= OnBeat;
    }

    public void OnBeat()
    {
        DOTween.Complete(gameObject);
        // ¶¯»­Ð§¹û
        transform.DORotate(transform.eulerAngles + new Vector3(0,0,anglePerMove), aniDuration);

        //StopAllCoroutines();
        //StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        float elapsedTime = 0;
        while (elapsedTime < aniDuration)
        {
            transform.Rotate(Vector3.forward * 15);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
