using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mp;
    public GameObject targetMap;
    void Start()
    {
        mp = FindAnyObjectByType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mp.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && mp.currentChunk == targetMap)
            mp.currentChunk = null;
    }
}
