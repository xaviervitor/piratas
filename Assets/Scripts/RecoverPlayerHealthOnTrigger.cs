using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverPlayerHealthOnTrigger : MonoBehaviour {
    [SerializeField] private GameObject Parent;
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject FullCircle;
    [SerializeField] private GameObject ParticleSystem;
    [SerializeField] private float recoverSpeed = 0.25f;
    [SerializeField] private float shrinkSpeed = 0.5f;
    
    float healthRecoverPercentage = 1f;
    float totalHealthRecover = 1.5f;
    bool DepletedCoroutineStarted = true;

    void Update() {
        if (healthRecoverPercentage > 0f) {
            FullCircle.transform.localScale = new Vector3(healthRecoverPercentage, healthRecoverPercentage, 1);        
        } else {
            if (DepletedCoroutineStarted == true) {
                ParticleSystem particleSystemComponent = ParticleSystem.GetComponent<ParticleSystem>();
                particleSystemComponent.Stop();
                StartCoroutine(OnHealthAvailableDepleted());
                DepletedCoroutineStarted = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") return;
    
        if (healthRecoverPercentage > 0f) {
            healthRecoverPercentage -= recoverSpeed * Time.deltaTime;
            collider.GetComponent<Ship>().ChangeHealth(totalHealthRecover * recoverSpeed * Time.deltaTime);
        }
    }

    IEnumerator OnHealthAvailableDepleted() {
        Transform circleTransform = Circle.transform;
        SpriteRenderer spriteRenderer = Circle.GetComponent<SpriteRenderer>();
        Color color;
        float step;
        while (circleTransform.localScale.x > 0f) {
            step = shrinkSpeed * Time.deltaTime;
            circleTransform.localScale = new Vector3(circleTransform.localScale.x - step, circleTransform.localScale.y - step, 1f);    
            color = spriteRenderer.color;
            color.a = color.a - step;
            spriteRenderer.color = color;
            yield return null;
        }
        circleTransform.localScale = new Vector3(0f, 0f, 1f);
        color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;
        Destroy(Parent);
    }
}
