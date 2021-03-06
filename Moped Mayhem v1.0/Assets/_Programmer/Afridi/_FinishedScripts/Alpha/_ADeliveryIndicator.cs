﻿// Main Author - Afridi Rahim
// Last Worked On - 19/10/2018
/* Alterations - Made a second Icon for the food to be on the Indicator : Completed
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class _ADeliveryIndicator : MonoBehaviour
{

    //Variables that control the camera and its offset and image transformation 
    private Camera m_MainCamera;
    private RectTransform m_Icon;
    private RectTransform m_IconFood;
    public RawImage m_IconImage;
    public RawImage m_FoodImage;
    private Canvas m_MainCanvas;

    //The Two Sprites that show's the icons depending if they are off or on screen
    public Texture m_TargetIconOnScreen;
    public Texture m_TargetIconOffScreen;
    public Texture m_TargetIconForFood;

    //Used to set a minimum and maximum the edgebuffer and scale can go to
    [Range(0, 100)]
    public float f_EdgeBuffer;
    public Vector3 m_targetIconScale;

    //On Startup
    void Awake()
    {
        //Set To Main Camera in Game
        m_MainCamera = Camera.main;
        //Finds A Canvas in the game and applys it
        m_MainCanvas = FindObjectOfType<Canvas>();


        //If there is no Canvas, pop a message saying we need one
        Debug.Assert((m_MainCanvas != null), "Need A Canvas To Operate");
        //Calls the Instantiate Function
        InstainateTargetIcon();
    }

    //For Each Interval
    void Update()
    {
        //This Is For The Indicator itself
        var vDir = m_Icon.position - transform.position;
        var vAngle = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;

        //This is for the Food within the Indicator
        var fDir = m_IconFood.position - transform.position;
        var fAngle = Mathf.Atan2(fDir.y, fDir.x) * Mathf.Rad2Deg;

        //These Algorithms ensures that both the food and the indicator follow the same axis
        m_Icon.transform.rotation = Quaternion.AngleAxis(vAngle, transform.forward);
        m_IconFood.transform.rotation = Quaternion.AngleAxis(fAngle, transform.forward);

        //Updates Icon's Position
        UpdateTargetIconPosition();
    }

    //This Function Creates and sets up the Indicator Icon
    private void InstainateTargetIcon()
    {
        //Set's the Icon as a new GameObject
        m_Icon = new GameObject().AddComponent<RectTransform>();
        //Set's the Food as a new GameObject
        m_IconFood = new GameObject().AddComponent<RectTransform>();
        //Set's the image onto the Main Canvas
        m_Icon.transform.SetParent(m_MainCanvas.transform);
        //Set's the Food image onto the Main Canvas
        m_IconFood.transform.SetParent(m_MainCanvas.transform);
        //Set's the scale of the icon through the input scale in the inspector
        m_Icon.localScale = m_targetIconScale;
        //Set's the scale of the icon through the input scale in the inspector
        m_IconFood.localScale = m_targetIconScale;
        //Set's the Name of the Indicator
        m_Icon.name = name + "Indicator";
        //Set;s the Name of the Food
        m_IconFood.name = name + "Food";
        //Adds's an Image onto the Icon
        m_IconImage = m_Icon.gameObject.AddComponent<RawImage>();
        //Adds Food to the Icon
        m_FoodImage = m_IconFood.gameObject.AddComponent<RawImage>();
        //Sets the default image to be the the indicator on screen
        m_IconImage.texture = m_TargetIconOnScreen;
        //Sets the food image to be the the indicator on screen
        m_FoodImage.texture = m_TargetIconForFood;
    }

    //This Updates the icon's position for each time the player is rotated by
    private void UpdateTargetIconPosition()
    {
        //Created a new position that tranforms each interval to find the icon within the scene
        Vector3 newPos = transform.position;
        //Sets the New position to the camera which turns it into a viewport display
        newPos = m_MainCamera.WorldToViewportPoint(newPos);
        //if the new position's Z is less than 0
        if (newPos.z < 0)
        {
            //Set the New Position's axis to subtract by 1f
            newPos.x = 1f - newPos.x;
            newPos.y = 1f - newPos.y;

            //Makes the new pos become a 3-D Vector
            newPos = Vector3Maxamize(newPos);

            //Sprite Changes to the image off-screen
            m_IconImage.texture = m_TargetIconOffScreen;

            //Set the food Texture to the icon
            m_FoodImage.texture = m_TargetIconForFood;

        }
        else //Or Else
        {
            //Changes the sprite 
            m_IconImage.texture = m_TargetIconOnScreen;

        }

        //Sets the New position to the camera which turns it into a Screenpoint display
        newPos = m_MainCamera.ViewportToScreenPoint(newPos);
        //Fixes the new position x onto the edge of the screen's width
        newPos.x = Mathf.Clamp(newPos.x, f_EdgeBuffer, Screen.width - f_EdgeBuffer);
        //Fixes the new position y onto the edge of the screen's height
        newPos.y = Mathf.Clamp(newPos.y, f_EdgeBuffer, Screen.height - f_EdgeBuffer);
        newPos.z = 0f;
        //Transforms the icon's positon using the new position
        m_Icon.transform.position = newPos;
        //Transforms the Food icon's positon using the new position
        m_IconFood.transform.position = newPos;
    }

    //Returns a 3-D Vector that is made up of the largest components of the two specified 3-D vectors
    public Vector3 Vector3Maxamize(Vector3 vector)
    {
        //Returning vector is the vector inputed
        Vector3 returnVector = vector;
        //Default max of the vector is 0
        float max = 0;

        //if Vector's X is greater than maximum size, then the vector's X becomes the maximum size
        max = vector.x > max ? vector.x : max;
        //if Vector's Y is greater than maximum size, then the vector's Y becomes the maximum size
        max = vector.y > max ? vector.y : max;

        //Divides the vector with max
        returnVector /= max;

        //Returns the Vector
        return returnVector;
    }
}