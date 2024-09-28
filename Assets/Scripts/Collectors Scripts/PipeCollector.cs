using UnityEngine;

public class PipeCollector : MonoBehaviour
{
    private GameObject[] pipeHolders;
    private float distance = 3.5f;
    private float lastPipeX;
    private float pipeMin = -1.2f;
    private float pipeMax = 1.1f;
    private void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");
        lastPipeX = pipeHolders[0].transform.position.x;
        for(int i = 1; i < pipeHolders.Length; i++) 
        {
            if (lastPipeX < pipeHolders[i].transform.position.x) 
            {
                lastPipeX = pipeHolders[i].transform.position.x;
            }
        }
        for(int i = 0; i < pipeHolders.Length; i++) 
        {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PipeHolder")) 
        {
            Vector3 temp = other.transform.position;
            temp.x = lastPipeX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);
            other.transform.position = temp;
            lastPipeX = temp.x;
        }
    }
}
