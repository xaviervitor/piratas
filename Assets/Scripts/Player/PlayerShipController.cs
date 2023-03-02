using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipController : Ship {

    [SerializeField] private GameObject CannonballPrefab;
    [SerializeField] private GameObject CannonballFrontalSpawn;
    [SerializeField] private List<GameObject> CannonballLateralSpawners;

    private int objInstanceID;

    protected new void Start() {
        base.Start();
        objInstanceID = gameObject.GetInstanceID();
    }

    public void OnForward(InputAction.CallbackContext context) {
        currentSpeed = Speed * context.action.ReadValue<float>();
    }

    public void OnRotate(InputAction.CallbackContext context) {
        currentTorque = -1 * Torque * context.action.ReadValue<float>();
    }

    public void OnFireFrontal(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        GameObject instantiatedCannonball = Instantiate(CannonballPrefab, CannonballFrontalSpawn.transform.position, CannonballFrontalSpawn.transform.rotation);
        Cannonball cannonball = (Cannonball) instantiatedCannonball.transform.GetChild(0).GetComponent<Cannonball>();
        cannonball.ownerInstanceID = objInstanceID;
    }

    public void OnFireLateral(InputAction.CallbackContext context) {
        if (context.action.ReadValue<float>() != 1 || !context.performed) return;
        foreach (GameObject cannonballSpawner in CannonballLateralSpawners) {
            GameObject instantiatedCannonball = Instantiate(CannonballPrefab, cannonballSpawner.transform.position, cannonballSpawner.transform.rotation);
            Cannonball cannonball = (Cannonball) instantiatedCannonball.transform.GetChild(0).GetComponent<Cannonball>();
            cannonball.ownerInstanceID = objInstanceID;
        }
    }
}
