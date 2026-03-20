using DG.Tweening;
using UnityEngine;

public class BeatReactiveObject : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.1f;
    private float aniDuration = 0.2f;

    private Vector3 originalScale;
    private Vector3 changedScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        changedScale = originalScale * scaleMultiplier;
    }

    void OnEnable()
    {
        RhythmAnalyser.onBeatDetected += OnBeat;
        bpmRhythm.onBeat += OnBeat;
    }

    void OnDisable()
    {
        RhythmAnalyser.onBeatDetected -= OnBeat;
        bpmRhythm.onBeat -= OnBeat;
    }

    public void OnBeat()
    {
        // ¶¯»­Ð§¹û
        transform.localScale = originalScale;

        transform.DOScale(changedScale, aniDuration / 2).
            OnComplete(() => transform.DOScale(originalScale, aniDuration / 2));
    }
}
