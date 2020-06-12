using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DounGameOver : MonoBehaviour
{
   
    float FallingSpeed;
    public float DounTime;
    public float TargetY;
    float Count = 0;
    private　float startX;


    // Start is called before the first frame update
    void Start()
    {
        float startY;
        float di;
        startY = transform.position.y;
        di = startY - TargetY;
        FallingSpeed = di / DounTime;
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        Count++;
        if (DounTime > Count)
        {
            // dxは任意の値
            transform.position += new Vector3(0, -FallingSpeed, 0);
           
        }
        else if(DounTime < Count)
        {
            UpDoun();
        }
    }
    void UpDoun()
    {
        float sin = Mathf.Sin(Time.time * 2);
        sin *= -1;
        // dxは任意の値
        float y = TargetY + (sin*2);
        transform.position = new Vector3(startX, y, 0);

       // transform.rotation = Quaternion.Euler(0, 0, sin * 10);
    }
}
