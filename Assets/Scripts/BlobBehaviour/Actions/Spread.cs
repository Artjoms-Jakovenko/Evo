using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : MonoBehaviour
{
    public GameObject asset;
    public float spreadTime = 20.0F;

    float lastSpreadTime = 0.0F;
    void Start()
    {
        lastSpreadTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastSpreadTime + spreadTime)
        {
            lastSpreadTime = Time.time;
            var grasses = ObjectManager.GetInstance().GetAllWithTagCombination(new List<ObjectTag>() { ObjectTag.Small, ObjectTag.Edible });
            if (ObjectManager.GetInstance().CountNearObjects(8.0F, gameObject, grasses) < 8)
            {
                Vector3 randomDestination3 = Vector3.zero;
                randomDestination3.x = Random.Range(0.0F, 10.0F);
                randomDestination3.y = 1000.0F;
                randomDestination3.z = Random.Range(0, 10);

                randomDestination3 += transform.position;

                RaycastHit hit;
                if (Physics.Raycast(randomDestination3, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, 0x0400))
                {
                    GameObject tempObject = Instantiate(asset);
                    tempObject.transform.position = hit.point;

                    ObjectManager.GetInstance().AddObject(tempObject);
                }
            }
        }
    }
}
