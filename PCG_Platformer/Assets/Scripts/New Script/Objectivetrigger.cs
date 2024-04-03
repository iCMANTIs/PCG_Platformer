using UnityEngine;
using UnityEngine.SceneManagement;  

public class Objectivetrigger : MonoBehaviour
{
    public GameObject successPanel;  

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))  
        {
            successPanel.SetActive(true);
            Time.timeScale = 0f;
            
        }
    }
}
