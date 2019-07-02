using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlame : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Character2")){
            Manager.PlayerTakeDamage(damage);
            DestroyProjectile();
        }else if(other.CompareTag("FlameWalker")){
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage(damage * 2);
            DestroyProjectile();
        }else if(other.CompareTag("TheWalker")){
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage(damage);
            DestroyProjectile();
        }else if(other.CompareTag("FreezeWalker")){
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage(damage/4);
            DestroyProjectile();
        }else if(other.CompareTag("Character1")){
            return;
        }else{
            DestroyProjectile();
        }
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }
}
