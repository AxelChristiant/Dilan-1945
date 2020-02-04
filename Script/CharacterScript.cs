using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour
{
    public Animator myAnimator;

    public abstract IEnumerator TakeDamage(string weaponType);
    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }


    public bool facingRight;
    public bool attack;
    [SerializeField]
    protected float movementSpeed;
 
    public EdgeCollider2D punch;
    public bool TakingDamage { get; set; }

    public abstract void Death();

    // Start is called before the first frame update
    public virtual void Start()
    {
        
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ChangeDirection() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }


    public virtual void MeleeAttack() {
        punch.enabled = attack;
       
    }

    public virtual void OnTriggerEnter2D(Collider2D collision) {
        if (damageSources.Contains(collision.tag)) {
           
            StartCoroutine(TakeDamage((string)collision.tag));
                }
    }

}
