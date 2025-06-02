using UnityEngine;

public class stationaryAttack : MonoBehaviour
{
    private float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1f)
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            time = 0f;
        }
    }
}
