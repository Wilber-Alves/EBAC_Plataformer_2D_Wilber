using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;


public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    private Vector2 originalScale;

    [Header("Speed Settings")]
    
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed = 10.0f;
    public float speedRun = 20.0f ;
    public float forceJump = 20.0f;


    [Header("Animation Settings")]
    
    public Vector2 idleScale = new Vector2(1f, 1f);
    public Vector2 jumpScale = new Vector2(0.75f, 1.5f);
    public Vector2 landScale = new Vector2(1.5f, 0.75f);
    public float jumpScaleDuration = 0.2f;
    public float landScaleDuration = 0.1f;
    public float landDelay = 0.05f;
    public Ease jumpEase = Ease.OutQuad;
    public Ease landEase = Ease.InQuad;
    public Ease delayEase = Ease.InBack;

    private float _currentSpeed;


    void Start()
    {
       myRigidbody = GetComponent<Rigidbody2D>();
       originalScale = myRigidbody.transform.localScale;
    }
    private void HandleScaleJump()
    {
        myRigidbody.DOKill();
        DG.Tweening.Sequence jumpSequence = DOTween.Sequence();
        jumpSequence.Append(myRigidbody.transform.DOScale(jumpScale,jumpScaleDuration).SetEase(jumpEase));
        jumpSequence.Append(myRigidbody.transform.DOScale(idleScale,jumpScaleDuration).SetEase(Ease.OutQuad));
       
    }
    private void HandleScaleLanded()
    {
        myRigidbody.DOKill();
        DG.Tweening.Sequence landSequence = DOTween.Sequence();
        landSequence.Append(myRigidbody.transform.DOScale(landScale,landScaleDuration).SetEase(landEase));
        landSequence.Append(myRigidbody.transform.DOScale(originalScale,landScaleDuration).SetEase(Ease.InQuad));
        landSequence.Append(myRigidbody.transform.DOScale(idleScale,landDelay).SetEase(Ease.InBack));
    }
    void Update()
    {
        HandleMovement();
        HandleJump();
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.linearVelocity = Vector2.up * forceJump;
            HandleScaleJump();
        }
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
    void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            HandleScaleLanded();
        }

    }
}
