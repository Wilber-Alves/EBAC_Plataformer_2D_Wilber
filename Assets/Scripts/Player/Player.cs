using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    

    [Header("Speed Settings")]
    
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed = 10.0f;
    public float speedRun = 20.0f ;
    public float forceJump = 20.0f;

    [Header("Animation Settings")]

    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float jumpScaleDuration = 0.2f;
    public Ease ease = Ease.OutBack;

    private float _currentSpeed;
 
    void Start()
    {
 
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleScaleJump();
    }

    private void HandleMovement()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.linearVelocity = new Vector2(_currentSpeed, myRigidbody.linearVelocity.y);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.linearVelocity = new Vector2(-_currentSpeed, myRigidbody.linearVelocity.y);
        }


        if(myRigidbody.linearVelocity.x > 0)
        {
            myRigidbody.linearVelocity += friction;

        }
        else if(myRigidbody.linearVelocity.x < 0)
        {
            myRigidbody.linearVelocity -= friction; ;
        }

    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.linearVelocity = Vector2.up * forceJump;
            myRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidbody.transform);
          
        }

    }
    private void HandleScaleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.transform.DOScaleY(jumpScaleY,jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            myRigidbody.transform.DOScaleX(jumpScaleX,jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        }
    }

}
