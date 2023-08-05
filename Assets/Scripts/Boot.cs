using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    public GameObject[] effects;
    // Start is called before the first frame update
    
    public void FollowMousePosition(){
        Vector3 mousePosition = Input.mousePosition;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x, Camera.main.ScreenToWorldPoint(mousePosition).y, -1f);
        GenerateEffect();
    }

    void GenerateEffect(){
        int randomIndex = Random.Range(0, effects.Length);
        GameObject effect = Instantiate(effects[randomIndex], transform.position, Quaternion.identity);

        float randomForce = Random.Range(50f, 200f);
        if(transform.position.x < 0)
            effect.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomForce, 0f));
        else
            effect.GetComponent<Rigidbody2D>().AddForce(new Vector2(-randomForce, 0f));
        
        Destroy(effect, 1.5f);
    }
}
