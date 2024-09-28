using UnityEngine;

public class BGCollector : MonoBehaviour
{
    private GameObject[] BackGrounds;
    private GameObject[] Grounds;

    private float lastBGX;
    private float lastGroundX;
    void Awake()
    {
        BackGrounds = GameObject.FindGameObjectsWithTag("Background");
        Grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBGX = BackGrounds[0].transform.position.x;
        lastGroundX = Grounds[0].transform.position.x;

        for(int i = 1; i < BackGrounds.Length; i++) 
        {
            if(lastBGX < BackGrounds[i].transform.position.x) 
            {
                lastBGX = BackGrounds[i].transform.position.x;
            }
        }
        for (int i = 1; i < Grounds.Length; i++)
        {
            if (lastGroundX < Grounds[i].transform.position.x)
            {
                lastGroundX = Grounds[i].transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Background")) 
        {
            Vector3 temp = other.transform.position;
            float width = ((BoxCollider2D)other).size.x;
            temp.x = lastBGX + width;
            other.transform.position = temp;
            lastBGX = temp.x;
        }
        else if (other.CompareTag("Ground"))
        {
            Vector3 temp = other.transform.position;
            float width = ((BoxCollider2D)other).size.x;
            temp.x = lastGroundX + width;
            other.transform.position = temp;
            lastGroundX = temp.x;
        }
    }
}
