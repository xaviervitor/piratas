using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipController : Ship {

    public GameObject CannonballPrefab;
    public GameObject CannonballFrontalSpawn;
    public List<GameObject> CannonballLateralSpawners;

    protected new void Start() {
        base.Start();
    }

    public void OnForward(InputAction.CallbackContext context) {
        currentSpeed = Speed * context.action.ReadValue<float>();
    }

    public void OnRotate(InputAction.CallbackContext context) {
        currentTorque = -1 * Torque * context.action.ReadValue<float>();
    }

    public void OnFireFrontal(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        Instantiate(CannonballPrefab, CannonballFrontalSpawn.transform);
    }

    public void OnFireLateral(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        foreach (GameObject cannonballSpawner in CannonballLateralSpawners) {
            Instantiate(CannonballPrefab, cannonballSpawner.transform);
        }
    }
}
