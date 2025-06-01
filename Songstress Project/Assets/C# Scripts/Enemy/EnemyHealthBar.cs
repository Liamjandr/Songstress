using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    EnemyHP hp;

    //float time;

    void Start()
    {
        hp = GetComponentInParent<EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {


        slider.value = hp.overwhelm/hp.enemyHealth;

        /*time += Time.deltaTime;
        if(time >= 2f)
        {
            Debug.Log(hp.overwhelm);
            time = 0f;
        }*/

    }
}
