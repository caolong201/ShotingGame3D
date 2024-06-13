using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float MoveSpeed , gravityModifier,jumpPower, runSpeed = 12f;
    public CharacterController CharCon;
    private Vector3 moveInput;
    public Transform camTrans;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    private bool canjump , canDoubleJump;
    public Transform groudCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;


    public GameObject bullet;
    public Transform firePoint;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        float yStore = moveInput.y;
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();                        
        moveInput = moveInput * MoveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * MoveSpeed;
        }


        moveInput.y = yStore;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (CharCon.isGrounded)
        {
            moveInput.y  = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        //chặn nhảy nhiều lần 

        canjump = Physics.OverlapSphere(groudCheckPoint.position, .25f ,whatIsGround).Length > 0;


        if(canjump)
        {
            canDoubleJump = false;
        }



        //Handle jumping nhảy

        if(Input.GetKeyDown(KeyCode.Space) && canjump) 
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        }else if(canDoubleJump && Input.GetKeyDown(KeyCode.Space) )
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        CharCon.Move(moveInput * Time.deltaTime);

        //ControCamera
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X") , Input.GetAxisRaw("Mouse Y")) * mouseSensitivity ;
        if (invertX)
        {
            mouseInput.x= -mouseInput.x; 
        }
        if (invertY )
        {
            mouseInput.y= -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3( -mouseInput.y, 0f, 0f));

        //Handle Shooting xử lý sự kiện bắn đạn khi người chơi nhấn nút chuột trái 
        //Raycast là một phương pháp được sử dụng trong lập trình game để xác định xem một "tia" (hay "ray") có va chạm với các đối tượng trong không gian 3D hay không
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            Instantiate ( bullet,firePoint.position, firePoint.rotation);
        }




        anim.SetFloat("moveSpeed" ,moveInput.magnitude);
        anim.SetBool("Onround", canjump);

      
    }
}
 