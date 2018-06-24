using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float turnSpeed = 45.0f;
    [SerializeField] float movementSpeed = 5.0f;
    [Header("Camera Position Variables")]
    [SerializeField] float cameraDistance = 5f;
    [SerializeField] float cameraHeight = 2f;


    public GameObject bulletPrefab;
    public Rigidbody localRigidBody;
    private Transform mainCamera;
    private Vector3 cameraOffset;
    private float accelation;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        localRigidBody = this.GetComponent<Rigidbody>();

        Debug.Log(transform.position);

        cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);

        mainCamera = Camera.main.transform;

        MoveCamera();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        /*var turnAmount = Input.GetAxis("Horizontal") * 0.1f;
        var moveAmount = Input.GetAxis("Vertical") * 0.1f;
        transform.Translate(x, 0, z);*/

        var turnAmount = Input.GetAxis("Horizontal") * 0.1f;
        var moveAmount = Input.GetAxis("Vertical") * 0.1f;

        Vector3 deltaTranslation = transform.position + transform.forward * movementSpeed * moveAmount * Time.deltaTime;
        localRigidBody.MovePosition(deltaTranslation);

        Quaternion deltaRotation = Quaternion.Euler(turnSpeed * new Vector3(0, turnAmount, 0) * Time.deltaTime);
        localRigidBody.MoveRotation(deltaRotation * localRigidBody.rotation);

        if (Input.GetKey(KeyCode.Space))
        {
            accelation += Time.deltaTime * 2;
            CmdPrepareAccelation();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            accelation = 0;
            CmdZeroAccelation();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CmdFire();
        }
        MoveCamera();
    }

    [Command]
    private void CmdPrepareAccelation()
    {
        accelation += Time.deltaTime * 2;
    }

    [Command]
    private void CmdZeroAccelation()
    {
        accelation = 0;
    }

    private void MoveCamera()
    {
        mainCamera.position = transform.position;
        mainCamera.rotation = transform.rotation;
        mainCamera.Translate(cameraOffset);
        mainCamera.LookAt(transform);
    }

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        Transform emitTransform = transform.GetChild(1);
        var bullet = (GameObject)Instantiate(
             bulletPrefab,
             emitTransform.position,
             Quaternion.identity);
        Debug.Log(accelation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 5 * accelation;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 4.0f);
    }

    private void OnGUI()
    {
        if (!isLocalPlayer) return;
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.fontSize = 14;
        gUIStyle.normal.textColor = Color.cyan;
        GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height / 2 + 140, 200, 100), "[w.a.s.d]:move " +
            "the boat.\n[space]:attack, and you can accumulate attacking distance by holding [space].\n" +
            "if your health bar disappear, you will move to another place to play again.", gUIStyle);
    }
}