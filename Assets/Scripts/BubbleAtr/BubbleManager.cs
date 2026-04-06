using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject BubbleBase;
    private float hue = 0f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            GameObject test = Instantiate(BubbleBase, pos, Quaternion.identity );
            Color color = Color.HSVToRGB(hue, 1, 1);
            test.GetComponent<SpriteRenderer>().color = color;

            hue += 0.05f;
            if (hue > 1f)
                hue = 0f;

            Destroy(test, 1.5f);
        }
    }
}
