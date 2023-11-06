using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 3f;
    public bool canJump = false;
    public bool ducked = false;
    public Vector3 scale;
    public GameObject camera;
    public SphereCollider sphere;
    private float time = 0f;
    private bool canAdd = true;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scale = transform.localScale;
        sphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAdd && time <= 40)
        {
            StartCoroutine(waitAdd());
            canAdd = false;
        }
        if (camera.transform.position.x + 40f >= transform.position.x)
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 3f);
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump)
        {
            if (transform.position.y <= -34)
            {
                Vector3 a = new Vector3(rb.velocity.x, 12f, rb.velocity.z);
                rb.velocity = a;
            }
            if (ducked)
            {
                transform.localScale = scale;
                sphere.radius = 0.5f;
                ducked = false;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity += Vector3.forward * 30f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity += Vector3.back * 30f * Time.deltaTime;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                transform.localScale = new Vector3(3, 1f, 3);
                sphere.radius = 0.2f;
                ducked = true;
            }
            else if (Input.GetKeyUp(KeyCode.Slash))
            {
                transform.localScale = new Vector3(3, 3, 3);
                sphere.radius = 0.5f;
                ducked = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            canJump = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
            canJump = false;
    }

    IEnumerator waitAdd()
    {
        yield return new WaitForSeconds(3f);
        canAdd = true;
        lowJumpMultiplier += 1;
        fallMultiplier += 1;
        time += 3f;
    }
}
