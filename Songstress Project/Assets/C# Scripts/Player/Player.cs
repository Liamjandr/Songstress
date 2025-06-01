using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    [SerializeField] Animator animator;
    //Health Bar
    public Image HealthBar;
    [SerializeField] private float HPThreshold = 106f;
    public Sprite death;
    private SpriteRenderer sp;
    [SerializeField] private GameObject deathScreen;
    private float playerHP;
    private float Enemy = 6f;
    public GameObject player;
    //Passive Health
    private float regenCD = 5f;
    private float regenTimer = 0f;
    private float passiveRegen = 1f;


    public Animation retry;
    Movement movement;
    private bool died = false;
    void Start()
    {

    }

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        playerHP = HPThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (died == false) deathScreen.SetActive(false);
        healthbar();
        AttackAnim();
    }

    private void healthbar()
    {
        HealthBar.fillAmount = playerHP/HPThreshold;
        if(regenTimer > 0f) regenTimer -= Time.deltaTime;

        //Debug.Log(playerHP);
        PlayerDeath(player, playerHP);

        if (playerHP >= HPThreshold) return;
        if (Input.GetKeyUp("7")) playerHP++;
        if (regenTimer <= 0f) { playerHP += passiveRegen * Time.deltaTime; }

    }

    void PlayerDeath(GameObject player, float hp)
    {      
        if (hp <= 0 && died == false)
        {
            movement.enabled = false;
            animator.SetTrigger("Death");
            died = true;
            animator.enabled = false;
            sp.sprite = death;
            deathScreen.SetActive(true);
        }
    }
    void AttackAnim()
    {
        if (Input.GetKey(KeyCode.U))
        {
            animator.SetBool("AttackState", true);
            Debug.Log("attacking");
        }
        else
        {
            animator.SetBool("AttackState", false);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        playerHP -= damageAmount;
        regenTimer = regenCD;
        Debug.Log("Player took damage: " + damageAmount);
    }
}