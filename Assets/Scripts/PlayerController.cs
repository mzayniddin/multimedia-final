using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int score = 0;

    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;

        SetScoreText();
        winTextObject.SetActive(false);
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
        if (score >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;

            SetScoreText();
        }
    }
}
