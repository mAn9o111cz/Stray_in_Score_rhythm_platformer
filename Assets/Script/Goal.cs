using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    public int count;
    public int targetCount;
    public UnityEvent goalActive;
    public string toLevelName;
    void OnEnable()
    {
        PickUpHelper.onCollocted += UpdateCount;
    }

    void OnDisable()
    {
        PickUpHelper.onCollocted -= UpdateCount;
    }


    public void UpdateCount()
    {
        count++;
        CheckCount();
    }

    public void CheckCount() 
    {
        
       if(count >= targetCount)
        {
            goalActive?.Invoke();
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(toLevelName);
        }

    }
}
