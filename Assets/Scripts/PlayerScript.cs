using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject indirection;
    [SerializeField] private LayerMask floorLayer;
    private InputAction movementInput;
    private InputAction sightRotationInput;
    private InputAction boostInput;
    private Rigidbody playerRB;

    private bool finishedSpawning = false;

    private float playerHealth = 100f;
    void Start()
    {
        movementInput = InputSystem.actions.FindAction("Move");
        sightRotationInput = InputSystem.actions.FindAction("Look");
        boostInput = InputSystem.actions.FindAction("Boost");

        playerRB = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        indirection.transform.rotation = Quaternion.identity;
    }
    private void FixedUpdate()
    {
        if (finishedSpawning)
        {
            PlayerMovement();
            PlayerBoost();
        }
    }
    void Update()
    {
        if (finishedSpawning)
        {
            SightRotation();
            BoostTime();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            playerHealth -= 5f;

        }
    }
    //---------------------------------------------------MOVEMENT FUNCTIONS-----------------------------------------------\\
    private const float playerSpeed = 10f;
    private const float gravVelChangePerPhysTick = -9.81f * (1f / 50f);
    private void PlayerMovement()
    {
        Vector2 movementInputFrameValue = movementInput.ReadValue<Vector2>();
        Vector3 priorVelocityDueToGravity = Vector3.up * playerRB.linearVelocity.y;

        if (Physics.Raycast(indirection.transform.position , -Vector3.up , 1f , floorLayer))
            playerRB.linearVelocity = (transform.forward * movementInputFrameValue.y * playerSpeed) +
                                      (transform.right * movementInputFrameValue.x * playerSpeed);
        else 
            playerRB.linearVelocity = (transform.forward * movementInputFrameValue.y * playerSpeed) +
                                      (transform.right * movementInputFrameValue.x * playerSpeed) + 
                                      (Vector3.up * gravVelChangePerPhysTick + priorVelocityDueToGravity);    
    }
    //---------------------------------------------------MOVEMENT FUNCTIONS-----------------------------------------------\\

    //---------------------------------------------------ROTATION FUNCTIONS-----------------------------------------------\\
    private const float cameraRotationSpeed = 0.2f;
    private void SightRotation()
    {
        Vector2 rotationInputDeltaValue = sightRotationInput.ReadValue<Vector2>();
        
        transform.Rotate(Vector3.up * rotationInputDeltaValue.x * cameraRotationSpeed);
        VerticalRotation(rotationInputDeltaValue.y);
    }
    float xAxisRotationCurrent;
    float xAxisRotationPotential;
    float xAxisRotationProspect;
    private void VerticalRotation(float rotationInputDeltaValueY)
    {
        xAxisRotationCurrent = indirection.transform.rotation.eulerAngles.x;
        xAxisRotationPotential = (Vector3.left * rotationInputDeltaValueY * cameraRotationSpeed).x;
        xAxisRotationProspect = xAxisRotationCurrent + xAxisRotationPotential;

        if (xAxisRotationCurrent > 322)
        {
            if ((xAxisRotationProspect) > 322)
                indirection.transform.Rotate(Vector3.left * rotationInputDeltaValueY * cameraRotationSpeed);
        }

        else if (xAxisRotationCurrent < 50)
        {
            if ((xAxisRotationProspect) < 50)
                indirection.transform.Rotate(Vector3.left * rotationInputDeltaValueY * cameraRotationSpeed);
        }
    }
    //---------------------------------------------------ROTATION FUNCTIONS-----------------------------------------------\\

    //-----------------------------------------------------BOOST FUNCTIONS------------------------------------------------\\
    private bool boostTime = false;
    private const float initialVelForceMag = 100f;
    private void BoostTime()
    {
        if (boostInput.triggered && !boostTime)
        {
            boostTime = true;
            boostVelMag = initialVelForceMag;
        }
    }
    private float boostVelMag;
    private const float boostForceDep = 20f;
    private void PlayerBoost()
    {
        if (boostTime)
        {
            playerRB.linearVelocity += ((playerRB.linearVelocity - playerRB.linearVelocity.y * Vector3.up).normalized * boostVelMag);
            boostVelMag -= boostForceDep;
        }

        if (boostVelMag == 0)
            boostTime = false;
    }
    //-----------------------------------------------------BOOST FUNCTIONS------------------------------------------------\\

    //----------------------------------------------------ANIMATOR FUNCTIONS----------------------------------------------\\
    private void FinishedSpawning()
    {
        Destroy(gameObject.GetComponent<Animator>());
        finishedSpawning = true;
    }
    //----------------------------------------------------ANIMATOR FUNCTIONS----------------------------------------------\\

    //-----------------------------------------------------HEALTH FUNCTIONS----------------------------------------------\\
    //private void HealthUpdate()
    //{
    //    if (playerHeatlh < )
    //}
    //-----------------------------------------------------HEALTH FUNCTIONS----------------------------------------------\\
}