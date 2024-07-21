using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
    public float carSpeed;
    public float horSpeed;
    public float jumpForce;
    public float Healt;
    public float Jumpforce = 5;
    public TrailRenderer trailRenderer;
    public TrailRenderer trailRenderer2;
    public UImanager uimanager;
    public GameManager gamemanager;
    public GameObject Spoiler;

    private bool jumpSpoiler;
    private bool hasCollidedWithWall = false;
    private float originalCarSpeed;
    private bool isSpeedBoostActive = false;
    private bool carpti = false;
    private Rigidbody rb;
    private bool hasCollided = false; // Çarpýþma kontrolü için kullanýlan flag
    private Quaternion originalrotation;

    Vector3 moveVec;

    private void Start()
    {
        gamemanager = GameObject.FindFirstObjectByType<GameManager>();
        uimanager = GameObject.FindFirstObjectByType<UImanager>();
        originalrotation = transform.rotation;   
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        if (Healt <= 60 && !jumpSpoiler)
        {
            JumpSpoiler();
        }
        
       
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 hareket = new Vector3(touch.deltaPosition.x, 0, 0) * horSpeed * Time.deltaTime;
                transform.Translate(hareket);

            }
        }

      
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            TakeHeal();
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            TakeDamage();
        }

        if (other.gameObject.CompareTag("Nitro"))
        {
            Destroy(other.gameObject);

            if (!isSpeedBoostActive)
            {
                originalCarSpeed = carSpeed;
                StartCoroutine(ActivateSpeedBoost(1f));
                trailRenderer.enabled = true;
                trailRenderer2.enabled = true;
                StartCoroutine(ActivateTrailRenderer());
            }
        }

        

        if (other.gameObject.CompareTag("Finish"))
        {
            carpti = true;
            uimanager.finishScreen();
            Time.timeScale = 0f;
            
           
        }

        if (!hasCollided && other.gameObject.CompareTag("Ramp2"))
        {
            transform.DORotate(new Vector3(-15f, 0f, 0f), 0.3f, RotateMode.WorldAxisAdd).OnComplete(ReturnToOriginalRotation);
            hasCollided = true;
        }

        else if (other.gameObject.CompareTag("Ramp"))
        {
          rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        if (other.gameObject.CompareTag("Wall") && !hasCollidedWithWall)
        {
            hasCollidedWithWall = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ramp"))
        {
            
            hasCollided = false;

        }
        else if (other.gameObject.CompareTag("Ramp2"))
        {
          
            hasCollided = false;
        }

    }

    private void ReturnToOriginalRotation()
    {
        transform.DORotate(originalrotation.eulerAngles, 1f).OnComplete(() =>
        {
            transform.rotation = originalrotation;
        });
    }

    private IEnumerator ActivateSpeedBoost(float boostDuration)
    {
        isSpeedBoostActive = true;
        carSpeed += 3f;

        yield return new WaitForSeconds(boostDuration);

        carSpeed = originalCarSpeed;
        isSpeedBoostActive = false;
    }


    private IEnumerator ActivateTrailRenderer()
    {
        trailRenderer.enabled = true;
        trailRenderer2.enabled = true;

        yield return new WaitForSeconds(1f);

        trailRenderer.enabled = false;
        trailRenderer2.enabled = false;
    }


    private void TakeDamage()
    {
        Healt -= 40;

        if (Healt <= 0)
        {
            uimanager.finishScreen();
            
            Time.timeScale = 0f;

        }
    }

    private void TakeHeal()
    {
        Healt += 10;
    }

    private void JumpSpoiler()
    {
        Rigidbody rb =Spoiler.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = Spoiler.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpSpoiler = true;
    }














}
