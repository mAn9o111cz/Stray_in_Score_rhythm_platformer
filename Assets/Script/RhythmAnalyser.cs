using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RhythmAnalyser : MonoBehaviour
{
    public AudioSource audioSource;
    public float threshold = 0.1f;      // 能量检测阈值
    public int windowSize = 1024;       // 窗口大小
    public int lowFrequencyRange = 100; // 只分析前100个频率bin

    private float[] spectrum;
    private bool hasDetectedBeat = false;

    public static Action onBeatDetected;

    private void Start()
    {
        spectrum = new float[windowSize];
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);     // 快速傅里叶变换

        // 这一帧是否检测到重音标识
        bool detectedThisFrame = false;

        for (int i = 0; i < lowFrequencyRange; i++)
        {
            if (spectrum[i] > threshold) // 假设能量大于阈值为重音
            {
                detectedThisFrame = true;
                break;
            }
        }

        if (detectedThisFrame && !hasDetectedBeat)
        {
            // 事件
            onBeatDetected?.Invoke();

            hasDetectedBeat = true;
        }
        else if (!detectedThisFrame)
        {
            hasDetectedBeat = false;
        }
    }
}
