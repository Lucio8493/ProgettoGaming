using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerTestScript : MonoBehaviour
{
    [SerializeField] protected float speed;
    private Rigidbody rb;
    private int score;
    private bool deActivation = false;
    [SerializeField] protected Text scoreText;
    [SerializeField] protected Text winText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();
        winText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(deActivation);
            score++;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Punteggio: " + score.ToString();
        if(score >= 10)
        {
            winText.text = "You Win!";
        }
    }
}
