using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    [SerializeField] Animator animator;

    //Health Bar
    public Image HealthBar;
    [SerializeField] private float HPThreshold = 106f;
    private float playerHP;
    private float Enemy = 6f;
    public GameObject player;
    //Passive Health
    private float regenCD = 5f;
    private float regenTimer = 0f;
    private float passiveRegen = 1f;

    float timer = 0f;
    void Start()
    {

    }

    private void Awake()
    {
        playerHP = HPThreshold;
    }

    // Update is called once per frame
    void Update()
    {
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
        timer += Time.deltaTime;
        if (hp <= 0)
        {
            Destroy(player);
        }
        //else if (timer > 2f)
        //{
        //    Debug.Log(hp);
        //    timer = 0f;
        //}
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