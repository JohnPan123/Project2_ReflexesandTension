using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject indirection;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject boostBar;
    private Animator boostBarAnimator;
    [SerializeField] private LayerMask floorLayer;
    
    private InputAction movementInput;
    private InputAction sightRotationInput;
    private InputAction boostInput;

    private Rigidbody playerRB;
    private Renderer playerRDR;

    private bool finishedSpawning = false;
    private float playerHealth = 100f;
    void Start()
    {
        GameManager.ResetVictoryState();
        GameManager.NewGamePlayerAlive();

        movementInput = InputSystem.actions.FindAction("Move");
        sightRotationInput = InputSystem.actions.FindAction("Look");
        boostInput = InputSystem.actions.FindAction("Boost");

        playerRB = GetComponent<Rigidbody>();
        playerRDR = GetComponent<Renderer>();
        boostBarAnimator = boostBar.GetComponent<Animator>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        indirection.transform.rotation = Quaternion.identity;
    }
    private void FixedUpdate()
    {
        if (GameManager.GetVictoryState()) ;

        else if (finishedSpawning)
        {
            PlayerMovement();
            PlayerBoost();
        }
    }
    void Update()
    {
        if (GameManager.GetVictoryState())
        {
            transform.localScale -= new Vector3(0, (0.20f * Time.deltaTime) , 0);
        }
            

        else if (finishedSpawning)
        {
            HealthCheck();
            SightRotation();
            BoostTime();
        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject objectPlayerCollidedWith = collision.gameObject;

        if (objectPlayerCollidedWith.CompareTag("Bullet"))
        {
            playerHealth -= 5f;
            playerRDR.material.color = new Color(1f - (playerHealth / 100f), (playerHealth / 100f), 0, 1);
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
    private const float initialVelForceMag = 100f;
    private void BoostTime()
    {
        if (boostInput.triggered && boostBarAnimator.GetBool("ReadyToBoost"))
        {
            boostBarAnimator.SetBool("PlayerPushedToBoost", true);
            boostVelMag = initialVelForceMag;
        }
    }
    private float boostVelMag;
    private const float boostForceDep = 13f;
    private void PlayerBoost()
    {
        if (boostBarAnimator.GetBool("IsBoosting"))
        {
            playerRB.linearVelocity += ((playerRB.linearVelocity - playerRB.linearVelocity.y * Vector3.up).normalized * boostVelMag);
            boostVelMag -= boostForceDep;
        }
    }
    //-----------------------------------------------------BOOST FUNCTIONS------------------------------------------------\\

    //----------------------------------------------------ANIMATOR FUNCTIONS----------------------------------------------\\
    private void FinishedSpawningPlayer()
    {
        Destroy(gameObject.GetComponent<Animator>());
        floor.GetComponent<Animator>().SetBool("GameStart" , true);

        finishedSpawning = true;
    }
    //----------------------------------------------------ANIMATOR FUNCTIONS----------------------------------------------\\

    //-----------------------------------------------------HEALTH FUNCTIONS----------------------------------------------\\
    private void HealthCheck()
    {
        if (playerHealth <= 0)
        {
            if (GameManager.CheckIfPlayerIsAlive()) KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GameManager.KillThePlayer();
        GetComponent<ParticleSystem>().Play();

        Destroy(GetComponent<MeshFilter>());
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(boostBar);
    }
    //-----------------------------------------------------HEALTH FUNCTIONS----------------------------------------------\\
}