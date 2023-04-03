using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {
    [SerializeField] protected float Speed = 4f;
    [SerializeField] protected float AngularVelocity = 90f;
    [SerializeField] public ShipSkin ActiveShipSkin;
    [SerializeField] private GameObject Smoke;
    [SerializeField] private GameObject DeathExplosionPrefab;
    [SerializeField] protected float maxHealth = 4f;
    protected float health;

    public delegate void HealthChangedDelegate(float health, float maxHealth);
    public event HealthChangedDelegate HealthChangedEvent;

    public delegate void ShipDestroyedDelegate();
    public event ShipDestroyedDelegate ShipDestroyedEvent;

    protected new Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;

    protected Vector2 currentVelocity = Vector2.zero;
    protected float currentAngularVelocity = 0f;

    void OnEnable() {
        ShipDestroyedEvent += DestroyShip;
    }

    void OnDisable() {
        ShipDestroyedEvent -= DestroyShip;        
    }

    protected void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    protected void Update() {

    }

    protected void FixedUpdate() {

    }

    private void UpdateSprite(float health) {
        spriteRenderer.sprite = ActiveShipSkin.sprites[Mathf.CeilToInt(health - 1)];
        if (health > 0f && health <= 1f) {
            Smoke.SetActive(true);
        } else {
            Smoke.SetActive(false);
        }
    }
    
    public virtual void DestroyShip() {
        Instantiate(DeathExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void ChangeHealth(float amount) {
        health += amount;
        if (health <= 0f) {
            health = 0f;
            if (ShipDestroyedEvent != null) {
                ShipDestroyedEvent();
            }
        } else if (health > maxHealth) {
            health = maxHealth;
        } else {
            UpdateSprite(health);
        }
        if (HealthChangedEvent != null) {
            HealthChangedEvent(health, maxHealth);
        }
    }

    public Cannonball SpawnCannonball(GameObject cannonballPrefab, GameObject shotPrefab, Transform spawn, int objInstanceID) {
        GameObject instantiatedCannonball = Instantiate(cannonballPrefab, spawn.position, spawn.rotation);
        Cannonball cannonball = (Cannonball) instantiatedCannonball.transform.GetChild(0).GetComponent<Cannonball>();
        cannonball.ownerInstanceID = objInstanceID;
        GameObject instantiatedShot = Instantiate(shotPrefab, spawn.position, spawn.rotation, transform);
        
        return cannonball;
    }
}
