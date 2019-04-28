using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public float projStrength;

    private Transform target;
    private Rigidbody2D rb;
    Vector2 moveDirection;
    private float projStrengthCounter;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        projStrengthCounter = projStrength;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(projStrengthCounter < 0){
            destroyProj();
            projStrengthCounter = projStrength;
        }else{
            projStrengthCounter-= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Character1") || other.CompareTag("Character2")){
            Manager.PlayerTakeDamage(5);
            destroyProj();
        }

        if(other.CompareTag("BlockDamage")){
            destroyProj();
        }
    }

    void destroyProj(){
        Destroy(gameObject);
    }

    public void SetTarget(Transform target){
        this.target = target;
    }
}
