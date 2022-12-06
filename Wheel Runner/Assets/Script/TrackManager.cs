using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject[] trackPrefab;
    public float spawn = 0;//vị trí của track tiếp theo
    public float trackLength = 30;//giá trị khoảng cách giữa các đường
    private int numberOfTrack = 5;
    public Transform player;
    private List<GameObject> activeTrack = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<numberOfTrack; i++)
        {
            if (i == 0)
            {
                SpawnTrack(0);
            }
            else
            {
                SpawnTrack(Random.Range(0, trackPrefab.Length));
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z - 35 >= spawn - (numberOfTrack * trackLength))
        {
            SpawnTrack(Random.Range(1, trackPrefab.Length));
            DeleteTrack();
        }
    }
    public void SpawnTrack(int trackIndex)//hàm sinh track
    {
        GameObject go = Instantiate(trackPrefab[trackIndex], transform.forward * spawn, transform.rotation);
        activeTrack.Add(go);
        spawn += trackLength;
    }
    private void DeleteTrack()//xóa track khi đã đi qua
    {
        Destroy(activeTrack[0]);
        activeTrack.RemoveAt(0);
    }
}
