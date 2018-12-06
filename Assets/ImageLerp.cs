using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageLerp : MonoBehaviour {

    public float every;   //The public variable "every" refers to "Lerp the color every X"
    float colorstep;
    Color[] colors = new Color[5]; //Insert how many colors you want to lerp between here, hard coded to 4
    int i;
    Color lerpedColor = Color.red;  //This should optimally be the color you are going to begin with

    void Start()
    {

        //In here, set the array colors you are going to use, optimally, repeat the first color in the end to keep transitions smooth

        colors[0] = Color.red;
        colors[1] = Color.yellow;
        colors[2] = Color.cyan;
        colors[3] = new Color(123, 0, 255);
        colors[4] = Color.red;
    }
    void Update()
    {
        if (colorstep < every)
        { //As long as the step is less than "every"
            lerpedColor = Color.Lerp(colors[i], colors[i + 1], colorstep);
            this.GetComponent<Image>().color = lerpedColor;
            colorstep += 0.015f;  //The lower this is, the smoother the transition, set it yourself
        }
        else
        { //Once the step equals the time we want to wait for the color, increment to lerp to the next color

            colorstep = 0;
            if (i < (colors.Length - 2))
            {
                i++;
            }
            else
            { //and then reset to zero
                i = 0;
            }
        }
    }
}

