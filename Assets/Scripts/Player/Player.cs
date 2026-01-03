using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;
    //public Animator animator;

    public LayerMask groundLayer;

    [Header("Scriptable Objects from Player - Speed and Animation settings")]
    public SOPlayer soPlayer;

    /*[Header("Speed Settings")]
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed = 10.0f;
    public float speedRun = 13.0f;

    [Header("Animation Settings")]
    public Vector2 idleScale = new Vector2(1f, 1f);
    public Vector2 jumpScale = new Vector2(0.75f, 1.5f);
    public Vector2 landScale = new Vector2(1.5f, 0.75f);
    public Ease jumpEase = Ease.OutQuad;
    public Ease landEase = Ease.InQuad;
    public Ease delayEase = Ease.InBack;

    public float jumpScaleDuration = 0.02f;
    public float landScaleDuration = 0.08f;
    public float landDelay = 0.05f;
    public float forceJump = 30.0f;
    public float doubleJumpForce = 25.0f;*/

    /*[Header("Scriptable Objects for Animation Settings - float variables")]
    public SOFloat soJumpScaleDuration;
    public SOFloat soLandScaleDuration;
    public SOFloat soLandDelay;
    public SOFloat soForceJump;
    public SOFloat soDoubleJumpForce;*/

    [Header("Animation Player Parameters")]
    public string boolRun = "Run";
    public string boolJumpDown = "JumpDown";
    public string boolJumpUp = "JumpUp";
    public string triggerJumpLanding = "JumpLanding";
    public string triggerDeath = "Death";
    
    private float _facingDirection = 1f;
    private Vector2 originalScale;
    private float _currentSpeed;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _canDoubleJump = false;
    private bool _isAlive = true;
    private Animator _currentPlayer;
    private WeaponBase _weaponBase;

    private void Awake()
    {
        
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayer.player, transform);

        FlashColor flash = GetComponent<FlashColor>();
        if (flash != null) flash.SetupFlash();

        _weaponBase = GetComponent<WeaponBase>();
        if (_weaponBase != null)
        {
            _weaponBase.SetupWeaponReferences(_currentPlayer.gameObject);
        }



    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill; // only for remove the callback
        // some modifications
        _isAlive = false; // this will stop the movimento on Update
        if (_currentPlayer != null ) _currentPlayer.SetTrigger(triggerDeath);
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.simulated = false; // player will stop collision.
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        originalScale = myRigidbody.transform.localScale;
        _facingDirection = Mathf.Sign(originalScale.x);
    }
    private void HandleScaleJump()
    {
        myRigidbody.DOKill();
        DG.Tweening.Sequence jumpSequence = DOTween.Sequence();

        Vector2 scaledJump = new Vector2(soPlayer.jumpScale.x * _facingDirection, soPlayer.jumpScale.y);
        Vector2 scaledIdle = new Vector2(soPlayer.idleScale.x * _facingDirection, soPlayer.idleScale.y);

        jumpSequence.Append(myRigidbody.transform.DOScale(scaledJump, soPlayer.jumpScaleDuration).SetEase(soPlayer.jumpEase));
        jumpSequence.Append(myRigidbody.transform.DOScale(scaledIdle, soPlayer.jumpScaleDuration).SetEase(Ease.OutQuad));
    }
    private void HandleScaleLanded()
    {
        myRigidbody.DOKill();
        DG.Tweening.Sequence landSequence = DOTween.Sequence();

        Vector2 landScaleWithDirection = new Vector2(soPlayer.landScale.x * _facingDirection, soPlayer.landScale.y);
        Vector2 originalScaleWithDirection = new Vector2(originalScale.x * _facingDirection, originalScale.y);
        Vector2 idleScaleWithDirection = new Vector2(soPlayer.idleScale.x * _facingDirection, soPlayer.idleScale.y);

        landSequence.Append(myRigidbody.transform.DOScale(landScaleWithDirection, soPlayer.jumpScaleDuration).SetEase(soPlayer.landEase));
        landSequence.Append(myRigidbody.transform.DOScale(originalScaleWithDirection, soPlayer.landScaleDuration).SetEase(Ease.InQuad));
        landSequence.Append(myRigidbody.transform.DOScale(idleScaleWithDirection, soPlayer.landDelay).SetEase(Ease.InBack));
    }

    void Update()
    {
        if (!_isAlive) return;
        HandleMovement();
        HandleJump();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
       
        if (!_isGrounded)
        {
            if (myRigidbody.linearVelocity.y > 0.1f)
            {
                _currentPlayer.SetBool(boolJumpUp, true);
                _currentPlayer.SetBool(boolJumpDown, false);
            }
            else if (myRigidbody.linearVelocity.y < -0.1f)
            {
                _currentPlayer.SetBool(boolJumpUp, false);
                _currentPlayer.SetBool(boolJumpDown, true);
            }
        }
        else
        {
            
            _currentPlayer.SetBool(boolJumpUp, false);
            _currentPlayer.SetBool(boolJumpDown, false);
        }
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (_isGrounded)
            {
                myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, soPlayer.forceJump);
                _canDoubleJump = true;
                _isGrounded = false;
                HandleScaleJump();
            }
          
            else if (_canDoubleJump)
            {
                myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, soPlayer.doubleJumpForce);
                _canDoubleJump = false; 
                HandleScaleJump();
            }
        }
    }

    private void HandleMovement()
    {
    
        _currentSpeed = Input.GetKey(KeyCode.Z) ? soPlayer.speedRun : soPlayer.speed;


        if (Input.GetKey(KeyCode.Z))
        {
            _currentSpeed = soPlayer.speedRun;
            _currentPlayer.speed = 2.0f;
        }
        else
        {
            _currentSpeed = soPlayer.speed;
            _currentPlayer.speed = 1.0f;
        }

        _horizontalInput = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _horizontalInput = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _horizontalInput = -1;
        }
        Debug.Log("Is Grounded: " + _isGrounded);

        myRigidbody.linearVelocity = new Vector2(_horizontalInput * _currentSpeed, myRigidbody.linearVelocity.y);

  
        if (_horizontalInput != 0)
        {
            _facingDirection = Mathf.Sign(_horizontalInput);
            float targetScaleX = originalScale.x * _facingDirection;

            if (Mathf.Sign(myRigidbody.transform.localScale.x) != _facingDirection)
            {
                myRigidbody.transform.DOScaleX(targetScaleX, 0.005f);
            }
            _currentPlayer.SetBool(boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(boolRun, false);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (!_isGrounded)
            {
                _isGrounded = true;
                _canDoubleJump = true; 
                HandleScaleLanded();
                _currentPlayer.SetTrigger(triggerJumpLanding);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
      
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            _isGrounded = false;
        }
    }

    public void DestroyMe()
    { 
        Destroy(gameObject);
    }

    public Animator GetCurrentAnimator()
    {
        return _currentPlayer;
    }

    public float GetFacingDirection()
    {
        return Mathf.Sign(transform.localScale.x);
    }

}
