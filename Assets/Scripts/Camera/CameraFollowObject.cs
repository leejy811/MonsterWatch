using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform playerTf;
    [SerializeField] private float flipYRotationTime = 0.5f;

    private Coroutine turnCoroutine;
    private int playerFacingDir; //right: 1, left: -1
    // Start is called before the first frame update
    void Start()
    {
        playerTf = PlayerController.instance.transform;
        playerFacingDir = PlayerController.instance.facingDir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTf.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0.0f;

        float elapsedTime = 0.0f;
        while(elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            //lerp y rotation
            yRotation = Mathf.Lerp(startRotation , endRotationAmount, elapsedTime / flipYRotationTime);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        playerFacingDir *= -1;
        if (playerFacingDir > 0)
            return 180.0f;
        else
            return 0.0f;
    }
}
