using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;

    public Text score;
    public Text gameoverText;
    public Text timerText;

    private int scoreValue = 0;
   
    
    public float timeRemaining = 14;
    public float timertwo = 12;
    public int countdownTime;

    AudioSource audioSource;
    
    public AudioClip winMusic;
    public AudioClip loseMusic; 
    public AudioClip jumpSound;
    public AudioClip instructionClip;

    private bool timerIsActive = true;

    public ParticleSystem slimeEffect;
    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = " ";
        audioSource = GetComponent<AudioSource>();
        gameoverText.text = " ";
        audioSource.clip = instructionClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            {
            Application.Quit();
            } 
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else 
        {
            Debug.Log("times up");
            Application.Quit();
            
        }
        if (timerIsActive)
        {
        if (timertwo > 0)
        {
            timertwo -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(timertwo).ToString();
        }
        else 
        {
            Debug.Log("lose");
        }
        
        if (timertwo <= 0)
        {
            timerText.text = "Times Up!";
            timerIsActive = false;
            ScoreDisplay();
           
        }
        }
        
        
        
        
    }

    

     private void OnCollisionEnter2D(Collision2D collision)
     {
         if(collision.collider.tag == "Slime")
         {
             
             scoreValue += 1;
             score.text = "Slimes Collected: " + scoreValue.ToString() + " of 3";
             Destroy(collision.collider.gameObject);
            ScoreDisplay();
         }
         
         
        
     }
     private void OnCollisionStay2D(Collision2D collision)
     {
         if (collision.collider.tag == "Floor")
         {
             if (Input.GetKey(KeyCode.W))
             {
                 rd2d.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
                 audioSource.clip = jumpSound; 
                 audioSource.Play();
             }
             
            
         }
     }

    

    public void ScoreDisplay()
     {
         
         if (timertwo > 0)
                {
                    if (scoreValue == 3)
                    {
                        gameoverText.text = "You Win! Game by Victoria Gillis.";
                        audioSource.clip = winMusic;
                        audioSource.Play();
                    }
                }
        if (timertwo <= 0)
        {
            if (scoreValue < 3)
            {
                gameoverText.text = "You Lose! Game by Victoria Gillis.";
                audioSource.clip = loseMusic; 
                audioSource.Play();
        
                
            }
        }
        
         
     }
}
