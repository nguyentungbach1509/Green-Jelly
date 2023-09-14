using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
The mark point to help player realize the area where boss will cast skill
to avoid
*/
public class MarkPoint : MonoBehaviour
{
    public ColumnEnergy columnEnergy;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroyPoint() {
        yield return new WaitForSeconds(.5f);
        Instantiate(columnEnergy, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
