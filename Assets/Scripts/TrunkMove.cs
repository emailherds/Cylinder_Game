using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrunkMove : MonoBehaviour
{
    public float speed = 2f;
    public bool canAdd = false;
    public float time = 0;
    public GameObject player1;
    public GameObject player2;
    public GameObject camera;
    public TextMeshProUGUI message;
    public GameObject canvas;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(waitAdd());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (Vector3.up * speed * Time.deltaTime);
        if (canAdd&&time < 40)
        {
            StartCoroutine(waitAdd());
            speed += 1f;
            canAdd = false;
        }
        if(player1.transform.position.y <= -40 || player1.transform.position.x < camera.transform.position.x)
        {
            canvas.SetActive(true);
            message.text = "Blue Wins!";
            audio.Stop();
            Time.timeScale = 0;
        }
        if (player2.transform.position.y <= -40 || player2.transform.position.x < camera.transform.position.x)
        {
            canvas.SetActive(true);
            message.text = "Red Wins!";
            audio.Stop();
            Time.timeScale = 0;
        }
    }

    IEnumerator waitAdd()
    {
        yield return new WaitForSeconds(1f);
        canAdd = true;
        time++;
    }


}
