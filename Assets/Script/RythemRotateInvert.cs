using DG.Tweening;
using UnityEngine;

public class RythemRotateInvert : MonoBehaviour
{
    [SerializeField] private float anglePerMove = 15f;
    private float aniDuration = 0.2f;
    private int beatCount = 0;
    private GameObject pivotObject;
    private Sequence rotationSequence;

    void Start()
    {
        //CreatePivot();
        pivotObject.transform.rotation = Quaternion.identity;
    }

    void CreatePivot()
    {
        pivotObject = new GameObject("RotationPivot");
        pivotObject.transform.position = transform.position;

        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            pivotObject.transform.position = transform.position - new Vector3(0, renderer.bounds.extents.y, 0);
        }

        pivotObject.transform.SetParent(transform.parent);
        transform.SetParent(pivotObject.transform);
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
        rotationSequence?.Kill();
    }

    public void OnBeat()
    {
        beatCount++;
        rotationSequence?.Kill();

        rotationSequence = DOTween.Sequence();

        switch (beatCount % 4)
        {
            case 1: // 第一拍：中间→右15度
                rotationSequence.Append(transform.DORotate(
                    new Vector3(0, 0, anglePerMove),
                    aniDuration)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBack));
                break;

            case 2: // 第二拍：右15度→中间
                rotationSequence.Append(transform.DORotate(
                    Vector3.zero,
                    aniDuration)
                    .From(new Vector3(0, 0, anglePerMove))
                    .SetEase(Ease.InOutSine));
                break;

            case 3: // 第三拍：中间→左15度
                rotationSequence.Append(transform.DORotate(
                    new Vector3(0, 0, -anglePerMove),
                    aniDuration)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBack));
                break;

            case 0: // 第四拍：左15度→中间
                rotationSequence.Append(transform.DORotate(
                    Vector3.zero,
                    aniDuration)
                    .From(new Vector3(0, 0, -anglePerMove))
                    .SetEase(Ease.InOutSine));
                break;
        }
    }

    //void OnDestroy()
    //{
    //    if (pivotObject != null)
    //    {
    //        transform.SetParent(pivotObject.transform.parent);
    //        Destroy(pivotObject);
    //    }
    //    rotationSequence?.Kill();
    //}
}