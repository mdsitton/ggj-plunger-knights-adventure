using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    //Private Variables
    private GameObject mainCamera;
    [SerializeField]
    private Vector3 changedPos;
    [SerializeField]
    private Vector3 changedPos2;
    private Vector3 cameraPos;

    //Public Variables
    public bool cameraChanged = false;
    public bool usedForChase;
    public bool usedForTongue;
    public bool usingChangedPos2;
    public float cameraFOV = 60.0f;
    public float verticalOffset = 1;
    public float cameraZ = -9.5f;
    public GameObject userActor;
    public Vector3 cameraRotation;  

    // Use this for initialization
    void Start()
    {        
        cameraRotation = new Vector3(0, 0, 0);
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.transform.rotation.Set(cameraRotation.x, cameraRotation.y, cameraRotation.z, 0.0f);
        mainCamera.transform.position = new Vector3(0f, 0f, cameraZ);
        mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
        this.transform.position = new Vector3(userActor.transform.position.x, userActor.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
		if (!userActor) return;
        if (!cameraChanged && usedForChase == true && !usedForTongue)
        {
            GameObject bossToFollow = GameObject.Find("ChaseBoss (1)");
            cameraPos = new Vector3(bossToFollow.transform.position.x, bossToFollow.transform.position.y + 14, -18);
            if (mainCamera.transform.position != cameraPos)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPos, .07f);
            }
            else
            {
                mainCamera.transform.position = cameraPos;
            }
            mainCamera.transform.rotation.Set(cameraRotation.x, cameraRotation.y, cameraRotation.z, 0.0f);
            mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
        }
        if (!cameraChanged && usedForTongue == true)
        {
            GameObject bossToFollow = GameObject.Find("TongueChase");
            cameraPos = new Vector3(bossToFollow.transform.position.x - 22, bossToFollow.transform.position.y, -15);
            if (mainCamera.transform.position != cameraPos)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPos, .07f);
            }
            else
            {
                mainCamera.transform.position = cameraPos;
            }
            mainCamera.transform.rotation.Set(cameraRotation.x, cameraRotation.y, cameraRotation.z, 0.0f);
            mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
        }
        if (!cameraChanged && usedForChase == false && usedForTongue==false)
        {
            cameraPos = new Vector3(userActor.transform.position.x, userActor.transform.position.y + verticalOffset, cameraZ);
            if (mainCamera.transform.position != cameraPos)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPos, .07f);
            }
            else
            {
                mainCamera.transform.position = cameraPos;
            }
            mainCamera.transform.rotation.Set(cameraRotation.x, cameraRotation.y, cameraRotation.z, 0.0f);
            mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
        }
        if (cameraChanged & usingChangedPos2 == false)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, changedPos, .009f);
            mainCamera.transform.rotation.Set(cameraRotation.x, cameraRotation.y, cameraRotation.z, 0.0f);
        }
        if (cameraChanged & usingChangedPos2 == true)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, changedPos2, .07f);
            mainCamera.transform.rotation.Set(cameraRotation.x, cameraRotation.y, cameraRotation.z, 0.0f);
        }
    }
}
