using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    public GameObject test, options, shop, hoe, watering;
    public Text text1, text2, text3;
    public float orthoZoomSpeed;
    public float perspectiveZoomSpeed;
    public float speed;

    //private Gyroscope m_Gyro;
    private float minFov, maxFov;
    private Vector2 val2, val;
    private Vector3 initPos;
    // Use this for initialization
    void Start () {
        minFov = 3f;
        maxFov = 10f;
        orthoZoomSpeed = 0.01f;
        perspectiveZoomSpeed = 0.5f;
        speed = 0.002f;
        //m_Gyro = Input.gyro;
        //m_Gyro.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetAxis("Fire1") != 0)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position = new Vector3(transform.position.x - Input.GetAxis("Mouse X") / 10, transform.position.y - Input.GetAxis("Mouse Y") / 10, transform.position.z);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position = new Vector3(transform.position.x - Input.GetAxis("Mouse X") / 10, transform.position.y - Input.GetAxis("Mouse Y") / 10, transform.position.z);
            }
        }*/

        if (!shop.activeSelf && !options.activeSelf)
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        initPos = Input.GetTouch(0).position;
                        text1.text = "" + initPos;
                        text2.text = "" + watering.transform.position;
                        text3.text = "" + hoe.transform.position;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (EventSystem.current.currentSelectedGameObject != hoe && EventSystem.current.currentSelectedGameObject != watering)
                        {
                            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                            Vector3 move = new Vector3(-touchDeltaPosition.x * speed * Camera.main.orthographicSize, -touchDeltaPosition.y * speed * Camera.main.orthographicSize, 0f);
                            /*text3.text = "" + Camera.main.orthographicSize;
                            text2.text = "" + move;
                            text1.text = "" + new Vector3(transform.position.x, transform.position.y, transform.position.z);*/

                            if (transform.position.x - Camera.main.orthographicSize < -14f && move.x < 0)
                            {
                                move.x = 0;
                            }
                            if (transform.position.x + Camera.main.orthographicSize > 14f && move.x > 0)
                            {
                                move.x = 0;
                            }
                            if (transform.position.y - Camera.main.orthographicSize < -10f && move.y < 0)
                            {
                                move.y = 0;
                            }
                            if (transform.position.y + Camera.main.orthographicSize > 10f && move.y > 0)
                            {
                                move.y = 0;
                            }

                            transform.Translate(move);
                        }
                    }
                }
                else if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    if (Camera.main.orthographicSize <= maxFov || 0 - deltaMagnitudeDiff > 0)
                    {
                        Zoom(deltaMagnitudeDiff);
                    }
                }
            }
        }
        /*if (test.transform.position.x > -9 && test.transform.position.x < 9)
        {
            test.transform.position = new Vector3(test.transform.position.x - m_Gyro.attitude.x * Time.deltaTime * 30f, test.transform.position.y, test.transform.position.z);
        }
        else
        {
            test.transform.position = new Vector3(8, test.transform.position.y, test.transform.position.z);
        }
        if (test.transform.position.y > -5 && test.transform.position.y < 5)
        {
            test.transform.position = new Vector3(test.transform.position.x, test.transform.position.y - m_Gyro.attitude.y * Time.deltaTime * 30f, test.transform.position.z);
        }
        else
        {
            test.transform.position = new Vector3(test.transform.position.x, 4, test.transform.position.z);
        }
        text1.text = "Gyro rotation rate " + m_Gyro.rotationRate;
        text2.text = "Gyro attitude" + m_Gyro.attitude;*/
    }

    void Zoom(float value)
    {
        if (Camera.main.orthographic)
        {
            if (transform.position.x - Camera.main.orthographicSize + value * orthoZoomSpeed < -14f)
            {
                transform.Translate(new Vector3(0.1f, 0f, 0f));
            }
            if (transform.position.x + Camera.main.orthographicSize + value * orthoZoomSpeed > 14f)
            {
                transform.Translate(new Vector3(-0.1f, 0f, 0f));
            }
            if (transform.position.y - Camera.main.orthographicSize + value * orthoZoomSpeed < -10f)
            {
                transform.Translate(new Vector3(0f, 0.1f, 0f));
            }
            if (transform.position.y + Camera.main.orthographicSize + value * orthoZoomSpeed > 10f)
            {
                transform.Translate(new Vector3(0f, -0.1f, 0f));
            }
            Camera.main.orthographicSize += value * orthoZoomSpeed;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, minFov);
        }
        else
        {
            float fov = Camera.main.fieldOfView;
            fov -= value * perspectiveZoomSpeed;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }
    }
}
