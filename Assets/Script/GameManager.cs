using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextLevelName;
    public int noteCollected=0;
    public int targetNote = 5;
    public UnityEvent onTargetCompleted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSceneWithDelay(float delayTime = 0)
    {
        StartCoroutine(LoadSceneWithDelayHelper(delayTime));
    }
    IEnumerator LoadSceneWithDelayHelper(float delayTime = 0)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(nextLevelName);
    }

    public void IncreaseNote()
    {
        noteCollected++;
        if (noteCollected >= 5)
        {
            onTargetCompleted?.Invoke();
        }

    }
}
