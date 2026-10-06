using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject indirection;
    private InputAction movementInput;
    private InputAction sightRotationInput;
    private InputAction boostInput;
    private Rigidbody playerRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        PlayerMovement();
        PlayerBoost();
    }
    int timesPressed = 0;
    // Update is called once per frame
    void Update()
    {
        SightRotation();
        BoostTime();
    }
    //---------------------------------------------------MOVEMENT FUNCTIONS-----------------------------------------------\\
    private const float playerSpeed = 10f;
    private void PlayerMovement()
    {
        Vector2 movementInputFrameValue = movementInput.ReadValue<Vector2>();
        Vector3 priorVelocityDueToGravity = Vector3.up * playerRB.linearVelocity.y;

        playerRB.linearVelocity = (transform.forward * movementInputFrameValue.y * playerSpeed) +
                                  (transform.right * movementInputFrameValue.x * playerSpeed) + 
                                  (priorVelocityDueToGravity);    
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
    private const float initialBoostForceMag = 100f;
    private void BoostTime()
    {
        if (boostInput.triggered && !boostTime)
        {
            boostTime = true;
            boostForceMag = initialBoostForceMag;
        }
    }
    private float boostForceMag;
    private const float boostForceDep = 20f;
    private void PlayerBoost()
    {
        if (boostTime)
        {
            playerRB.linearVelocity += (playerRB.linearVelocity.normalized * boostForceMag);
            boostForceMag -= boostForceDep;
        }

        if (boostForceMag <= 0)
            boostTime = false;
    }
    //-----------------------------------------------------BOOST FUNCTIONS------------------------------------------------\\
}