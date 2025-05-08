using UnityEngine;

public class justToCheckVector : MonoBehaviour
{
    public Transform player;
    private float time;
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1)
        {
            Debug.Log(player.position);
            time = 0;
        }
    }
}
