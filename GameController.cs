using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform basket;
    public Transform apple;
    public Text scoreText;
    public Text healthText;

    private int score = 0;
    private int health = 3;
    private bool isGameOver = false;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private void Update()
    {
        MoveBasket();
        CheckDrop();
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);


        basket.position = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));


        UpdateUI();
    }

    void MoveBasket()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        basket.Translate(moveDirection * Time.deltaTime * 5f);
    }
    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGameOver)
        {
            StartCoroutine(DropApple());
        }
    }
    IEnumerator DropApple()
    {

        Vector3 initialApplePosition = apple.position;

        apple.position = new Vector3(
            Random.Range(basket.position.x - 1f, basket.position.x + 1f),
            0,
            Random.Range(basket.position.z - 1f, basket.position.z + 1f)
        );

        yield return new WaitForSeconds(0.5f);

        if (IsBasketCollision())
        {

            IncreaseScore();
        }
        else
        {

            DecreaseHealth();
        }


        apple.position = initialApplePosition;


        UpdateUI();
      
    }

    bool IsBasketCollision()
    {
        return Mathf.Abs(apple.position.x - basket.position.x) < 1f && Mathf.Abs(apple.position.z - basket.position.z) < 1f;
    }

    void IncreaseScore()
    {

        score++;
    }
    void DecreaseHealth()
    {

        health--;


        if (health <= 0)
        {
            isGameOver = true;
        }
    }
    void UpdateUI()
    {

        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
    }
}
