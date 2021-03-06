using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jobs : MonoBehaviour
{
    public List<float[]> coords;
    public List<GameObject> chels;
    public GameObject build;
    public List<float[]> money;
    public List<float[]> happiness;
    public void filler(out List<float[]> coords)
    
    {
        coords = new List<float[]>();
        for (int i = 0; i < 5; i++)
        {
            coords.Add(new float[2] { Random.Range(-50, 50), Random.Range(-50, 50) });
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        filler(out coords);
        for (int i=0; i<5; i++)
        {
            chels.Add(Instantiate(chel));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<5; i++)
        {
            chels[i].transform.position = new Vector3(coords[i][0],0, coords[i][1]) ;
            
        }
    }
}
