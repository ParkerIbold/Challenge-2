using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text lossText;
    public Text livesText;
    public Text countText;
    public Text winText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private int count;
    private int lives;
    private Rigidbody2D rb2d;

    public float speed;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        count = 0;
        winText.text = "";
        SetCountText();

        lives = 3;
        lossText.text = "";
        SetLivesText();

        musicSource.clip = musicClipOne;
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 8)
            musicSource.Stop();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            
            SetLivesText();
        }

        if (count == 4) 
        {
            transform.position = new Vector3(82.0f, transform.position.y, 0f);
            lives = 3;
            SetLivesText();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            lossText.text = "You Lose";
            gameObject.SetActive(false);
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == 8)
        {
           winText.text = "You Win!";
          
        }
    }
}
