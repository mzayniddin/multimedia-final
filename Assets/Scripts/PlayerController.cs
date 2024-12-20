using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int score = 0;
    // Winning Score
    public int winScore = 12;

    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public GameObject newGameObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;

        SetScoreText();
        winTextObject.SetActive(false);
        newGameObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

        // Check for the win
        if (score >= winScore)
        {
            winTextObject.SetActive(true);
            newGameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            SoundManager.Instance.PlayWinSound();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You lose!";
            newGameObject.SetActive(true);
            SoundManager.Instance.PlayLoseSound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            SoundManager.Instance.PlayCoinSound(); 
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }
    }
}
