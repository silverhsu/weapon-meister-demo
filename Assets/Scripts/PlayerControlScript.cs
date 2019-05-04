using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerEnum { PLAYER_ONE, PLAYER_TWO }

// Input supports XBOX 360 controller
public class PlayerControlScript : MonoBehaviour
{
    public Rigidbody2D thisRigid2D;
    public Collider2D thisCol2D;

    public Vector2 inputLeftStick;
    public Vector2 inputRightStick;

    private float moveForceFactor = 0.5f;
    public float moveModifier = 1f;

    public GameObject attackPrefab;
    public bool stopOnAttack;
    public GameObject debugMarkerPrefab;
    private GameObject inputMarkerObj;

    public float atkCooldownTimer;
    public float atkCooldownThreshold;
    public float weaponDelayTimer;
    public float weaponDelayThreshold;

    private string weaponButtonName = "T_RightBumper";

    [HideInInspector]
    public UnityEvent WeaponButtonPressed;
    [HideInInspector]
    public UnityEvent WeaponButtonHeld;
    [HideInInspector]
    public UnityEvent WeaponButtonReleased;

    public GameObject weaponSwordPrefab;
    public GameObject weaponRapierPrefab;
    public GameObject weaponHammerPrefab;
    public GameObject weaponWhipPrefab;
    public GameObject weaponGreatSwordPrefab;

    public GameObject equippedWeaponObj;
    public WeaponControlScript weaponControl;

    public Transform handsAnchor;
    public Transform neutralHandTrans;
    public Transform heldHandTrans;
    public Transform finishHandTrans;

    public bool isMovementStopped;
    public float movementStoppedTimer;
    public float movementStoppedThreshold;

    public GameObject inputObj;

    void Start()
    {
        WeaponButtonHeld = new UnityEvent();
        thisRigid2D = this.GetComponent<Rigidbody2D>();
        thisCol2D = this.GetComponent<Collider2D>();
        inputMarkerObj = Instantiate(debugMarkerPrefab);

        equipWeapon(WeaponEnum.SWORD);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            equipWeapon(WeaponEnum.SWORD);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            equipWeapon(WeaponEnum.RAPIER);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            equipWeapon(WeaponEnum.WHIP);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            equipWeapon(WeaponEnum.GREAT_SWORD);
        }
    }


    void FixedUpdate()
    {
        inputLeftStick = new Vector2(Input.GetAxis("L_XAxis_1"), -Input.GetAxis("L_YAxis_1"));
        inputRightStick = new Vector2(Input.GetAxis("R_XAxis_1"), -Input.GetAxis("R_YAxis_1"));

        if (inputLeftStick.magnitude > 1f)
        {
            inputLeftStick.Normalize();
        }
        if (!isMovementStopped)
        {
            thisRigid2D.AddForce(inputLeftStick * moveForceFactor * thisRigid2D.mass * moveModifier, ForceMode2D.Impulse);
        }
        else
        {
            movementStoppedTimer -= Time.deltaTime;
            if(movementStoppedTimer <= 0f)
            {
                movementStoppedTimer = 0f;
                isMovementStopped = false;
            }
        }

        if (Input.GetButtonDown(weaponButtonName)){
            WeaponButtonPressed.Invoke();
        }

        if (Input.GetButton(weaponButtonName)){
            WeaponButtonHeld.Invoke();
        }

        if (Input.GetButtonUp(weaponButtonName)){
            WeaponButtonReleased.Invoke();
        }

        handsAnchor.transform.rotation = Util.UnitVectorToQuaternion(inputRightStick);
        inputMarkerObj.transform.position = this.transform.position + new Vector3(inputRightStick.x, inputRightStick.y, 0f);
        inputMarkerObj.transform.rotation = Util.UnitVectorToQuaternion(inputRightStick);
        inputObj.transform.position = this.transform.position + (3f * new Vector3(inputRightStick.x, inputRightStick.y, 0f));
    }

    public void equipWeapon(WeaponEnum newWeaponType)
    {
        //There's already a weapon equipped, delete it
        if(equippedWeaponObj != null)
        {
            Destroy(equippedWeaponObj);
            WeaponButtonPressed.RemoveAllListeners();
            WeaponButtonHeld.RemoveAllListeners();
            WeaponButtonReleased.RemoveAllListeners();
        }
        Debug.Log("Spawning weapon type: " + newWeaponType);
        switch (newWeaponType)
        {
            case WeaponEnum.SWORD:
                equippedWeaponObj = (GameObject)Instantiate(weaponSwordPrefab);
                break;

            case WeaponEnum.RAPIER:
                equippedWeaponObj = (GameObject)Instantiate(weaponRapierPrefab);
                break;

            case WeaponEnum.HAMMER:
                equippedWeaponObj = (GameObject)Instantiate(weaponHammerPrefab);
                break;

            case WeaponEnum.WHIP:
                equippedWeaponObj = (GameObject)Instantiate(weaponWhipPrefab);
                break;

            case WeaponEnum.GREAT_SWORD:
                equippedWeaponObj = (GameObject)Instantiate(weaponGreatSwordPrefab);
                break;

            default:
                equippedWeaponObj = (GameObject)Instantiate(weaponSwordPrefab);
                break;
        }
        
        weaponControl = equippedWeaponObj.GetComponent<WeaponControlScript>();

        WeaponButtonHeld.AddListener(weaponControl.OnWeaponButtonHeld);
        WeaponButtonPressed.AddListener(weaponControl.OnWeaponButtonPressed);
        WeaponButtonReleased.AddListener(weaponControl.OnWeaponButtonReleased);
    }
}