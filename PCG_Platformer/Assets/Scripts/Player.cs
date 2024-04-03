/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 0.5f;
    GameObject currentFloor;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;
    int score;
    float scoreTime;
    [SerializeField] GameObject replayButton;
    [SerializeField] Text fallText; 
    [SerializeField] float fallDistanceThreshold = 8f; 
    [SerializeField] float textDisplayTime = 3f; 
    private Vector3 lastPosition; 
    private List<string> messages = new List<string> 
{
    "Oof, you lost a lot of progress. That’s a deep frustration, a real punch in the gut.",
    "Oh no, it happened again. Keep on trying, don’t let it get to you.",
    "The pain I feel now is the happiness I had before.",
    "Whenever I climb, I am followed by a dog called Ego.",
    "The soul would have no rainbow had the eyes no tears.",
    "To live is to suffer. To survive is to find meaning in the suffering.",
    "Of all sad words of tongue or pen, the saddest are these, 'It might have been'.",
    "There are no regrets in life, just lessons.",
    "Your failure here is a metaphor. To learn for what, please resume climbing.",
    "There I was again tonight, forcing laughter, faking smiles.",
};

    void Start()
    {
        Hp = 9999;
        score = 0;
        scoreTime = 0f;
        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        UpdateScore();
        CheckFallDistance();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Normal")
        {
            if(other.contacts[0].normal == new Vector2(0f, 1f))
            {
            currentFloor = other.gameObject;
            ModifyHp(1);
            }
        }
        else if(other.gameObject.tag == "Nails")
        {
            if(other.contacts[0].normal == new Vector2(0f, 1f))
            {
            currentFloor = other.gameObject;
            ModifyHp(-3);
            }
        }
        else if(other.gameObject.tag == "Ceiling")
        {
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathLine")
        {
            Die();
        }
    }
    void ModifyHp(int num)
    {
        Hp += num;
        if(Hp > 10)
        {
            Hp = 10;
        }
        else if(Hp <= 0)
        {
            Hp = 0;
            Die();
        }
        UpdateHpBar();
    }
    void UpdateHpBar()
    {
        for(int i = 0; i < HpBar.transform.childCount; i++)
        {
            if(Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if(scoreTime>2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = "underground" + score.ToString() + "Level";
        }
    }
    void Die()
    {
        Time.timeScale = 0f;
        replayButton.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    void CheckFallDistance()
    {
        float distanceFell = lastPosition.y - transform.position.y;
        if (distanceFell >= fallDistanceThreshold)
        {
            ShowRandomText();
        }
        lastPosition = transform.position;
    }

    void ShowRandomText()
    {
        int randomIndex = Random.Range(0, messages.Count);
        fallText.text = messages[randomIndex];
        fallText.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(textDisplayTime));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fallText.gameObject.SetActive(false);
    }

}*/
