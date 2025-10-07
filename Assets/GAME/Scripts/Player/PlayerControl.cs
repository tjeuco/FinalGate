using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform pointCheckGround;
    [SerializeField] private Transform pointFireIdle;
    [SerializeField] private Transform pointFireLie;
    [SerializeField] private LayerMask layerGroundCheck;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private PlayerDataSO playerDataSO;

    private bool isGround = false;
    private Rigidbody2D rg;

    private PlayerCollision playerCollision;
    private bool isLieDown = false;

    [Header("-----Actions------")]
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference attackAction;
    [SerializeField] InputActionReference movementAction;
    [SerializeField] InputActionReference lieAction;
    [SerializeField] InputActionReference actAction;

    [SerializeField] Transform btnLieDown, btnAct;// bat tat khi khong du dieu khien

    Vector3 inputJoy;

    [SerializeField] PlayerAnimation anim;
    PLAYER_STATE playerState = PLAYER_STATE.IsIdle;
    public PLAYER_STATE PlayerState => playerState;

    private bool activeMine = false; //// bien kich no min
    public bool ActiveMine => activeMine;

    void Start()
    {
        isLieDown =false;  
        this.rg = GetComponent<Rigidbody2D>();
        this.anim = GetComponentInChildren<PlayerAnimation>();
        this.playerCollision = GetComponent<PlayerCollision>();
    }

    void Update()
    {
        this.anim.updateInputY(inputJoy.y);

        PlayerMove();
        PlayerJump();
        PlayerShoot();
        AutodetectState();
        PlayerAction();

    }

    void PlayerMove()
    {
        inputJoy = movementAction.action.ReadValue<Vector2>();
       // Debug.Log("Joystick Click: " + inputJoy);
        rg.linearVelocity = new Vector2(inputJoy.x * playerDataSO.speed, rg.linearVelocity.y);
        if (Mathf.Abs(inputJoy.x) > 0.1f)
        {
            float face = inputJoy.x > 0 ? 1 : -1;
            this.transform.localScale = new Vector3(face, 1, 1);
        }
    }
    void PlayerJump()
    {
        isGround = Physics2D.OverlapCircle(pointCheckGround.position, 0.1f, layerGroundCheck);
        if (jumpAction.action.WasPressedThisFrame())
        {
            if (playerDataSO.numberJump < 2)
            {
                rg.linearVelocity = new Vector2(rg.linearVelocityX,playerDataSO.jumpForce);
                playerDataSO.numberJump++;
                AudioManager.Instance.PlayJumpMusic();
            }
        }
        if (isGround) 
        { 
            playerDataSO.numberJump = 1; 
        }  
    }
    void PlayerShoot()
    {
        if (attackAction.action.WasPressedThisFrame())
        {
            GameObject bullet = LazyPooling.Instance.GetObject(bulletPrefab);
            bullet.gameObject.SetActive(true);
            if (isLieDown)
            {
                bullet.transform.position = pointFireLie.position;
            }
            else
            {
                bullet.transform.position = pointFireIdle.position;
            }

            BulletPlayer bulletScript = bullet.GetComponent<BulletPlayer>();

            if (DirectionBulletFromJoy() == null)
                bulletScript.SetDirectionBullet(transform.localScale.x > 0 ? Vector3.right : Vector3.left);
            else
                bulletScript.SetDirectionBullet((Vector3)DirectionBulletFromJoy());
            
            AudioManager.Instance.PlayBulletMusic();
        }
    }
    
    void AutodetectState()
    {

        if (Mathf.Abs(this.rg.linearVelocityX) > 0.1f)
        {
            this.playerState = PLAYER_STATE.IsRun;
        }
        else
        {
            this.playerState = PLAYER_STATE.IsIdle;
        }
        if (Mathf.Abs(this.rg.linearVelocityY) > 0.1f)
        {
            this.playerState = PLAYER_STATE.IsJump;
        }
        if (this.isLieDown == true)
        {
            this.playerState = PLAYER_STATE.IsLieDown;
        }
    }
    public Vector3? DirectionBulletFromJoy()
    {
        // sử dụng bàn phím
        Vector2 dir = inputJoy.normalized;

        // Nếu input đúng theo 8 hướng cơ bản (nguyên)
        if (Mathf.Abs(dir.x) == 1 && dir.y == 0) // trái/phải
            return new Vector3(dir.x, 0, 0);
        if (Mathf.Abs(dir.y) == 1 && dir.x == 0) // lên/xuống
            return new Vector3(0, dir.y, 0);
        if (Mathf.Abs(dir.x) == 1 && Mathf.Abs(dir.y) == 1) // chéo
            return new Vector3(dir.x, dir.y, 0).normalized;
        
        //joystickk
        if (inputJoy.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(inputJoy.y, inputJoy.x) * Mathf.Rad2Deg;

            float snappedAngle = Mathf.Round(angle / 45f) * 45f;

            Vector3 direction = new Vector3(
            Mathf.Cos(snappedAngle * Mathf.Deg2Rad),
            Mathf.Sin(snappedAngle * Mathf.Deg2Rad),
            0f
        ).normalized;

            return direction;
        }
        return null;
    }
    void PlayerAction()
    {
        if (this.lieAction == null || this.actAction == null) 
            return;

        if (this.lieAction.action.WasPressedThisFrame() && isGround)
        {
            this.isLieDown = !this.isLieDown;
        }
        
        if (this.movementAction.action.WasPressedThisFrame() || this.jumpAction.action.WasPressedThisFrame())
        {
            this.isLieDown = false;
        }

        /////// Dat min
        if (this.actAction.action.WasPressedThisFrame())
        {
            if (this.playerCollision.CarryObjects.Count <= 0)
            {
                return;
            }
            var itemDrop = this.playerCollision.CarryObjects[0];
            if (itemDrop != null)
            {
                this.playerCollision.DropItem(itemDrop);
                this.activeMine = true;
                Debug.Log("Drop Item:" + itemDrop.name);
            }

        }


    }
    public enum PLAYER_STATE
    {
        IsIdle,
        IsRun,
        IsJump,
        IsLieDown,
        IsDie
    }
}
