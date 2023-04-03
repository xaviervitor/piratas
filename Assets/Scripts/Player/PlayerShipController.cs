using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipController : Ship {

    [SerializeField] private GameObject CannonballPrefab;
    [SerializeField] private GameObject CannonballFrontalSpawn;
    [SerializeField] private List<GameObject> CannonballLateralSpawners;
    [SerializeField] private GameObject ShotPrefab;
    
    private PlayerInput playerInput;
    private int objInstanceID;

    private float currentSpeed = 0f;

    protected new void Start() {
        base.Start();
        playerInput = GetComponent<PlayerInput>();
        objInstanceID = gameObject.GetInstanceID();
    }

    protected new void FixedUpdate() {
        rigidbody.angularVelocity = currentAngularVelocity;
        rigidbody.velocity = -transform.up * currentSpeed;
    }

    public void OnForward(InputAction.CallbackContext context) {
        currentSpeed = Speed * context.action.ReadValue<float>();
    }

    public void OnRotate(InputAction.CallbackContext context) {
        currentAngularVelocity = -1 * AngularVelocity * context.action.ReadValue<float>();
    }

    public void OnFireFrontal(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        SpawnCannonball(CannonballPrefab, ShotPrefab, CannonballFrontalSpawn.transform, objInstanceID);
    }

    public void OnFireLateral(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        Cannonball cannonball = null;
        foreach (GameObject cannonballSpawner in CannonballLateralSpawners) {
            cannonball = SpawnCannonball(CannonballPrefab, ShotPrefab, cannonballSpawner.transform, objInstanceID);
            cannonball.playSounds = false;
        }
        if (cannonball != null) {
            cannonball.playSounds = true;
        }
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        playerInput.SwitchCurrentActionMap(Constants.ActionMap.Paused);
        MatchManager.Pause();
    }

    public void OnUnpause(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        playerInput.SwitchCurrentActionMap(Constants.ActionMap.Player);
        MatchManager.Unpause();
    }

    public void OnPauseContinueButtonClick() {
        playerInput.SwitchCurrentActionMap(Constants.ActionMap.Player);
        MatchManager.Unpause();
    }

    public void Curse() {
        Speed = 3f;
        AngularVelocity = -45f;
        currentAngularVelocity = -currentAngularVelocity;
    }

    public void Uncurse() {
        Speed = 4f;
        AngularVelocity = 90f;
    }
}
