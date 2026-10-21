using UnityEngine;

public class Slingshot : MonoBehaviour
{
     [Header("Inscribed")]

    public GameObject prefabProjectile;

    public float velocityMult = 10f;

    public float bandWidth = 0.15f;

    public Material bandMat;

    public Color bandColor = new Color(0.35f, 0.2f, 0.1f);

    public AudioClip launchSound;
    [Range(0f, 1f)]
    public float launchVolume = 0.7f;

    private AudioSource audioSource;

    [Header("Dynamic")]

    public GameObject launchPoint;

    public Vector3 launchPos;

    public GameObject projectile;

    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    private Transform leftArm;

    private Transform rightArm;

    private LineRenderer slingshotBand;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Transform launchPointTrans = transform.Find("LaunchPoint");

        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

        leftArm = transform.Find("LeftArm");
        rightArm = transform.Find("RightArm");

        slingshotBand = GetComponent<LineRenderer>();
        slingshotBand.material = bandMat;
        slingshotBand.startWidth = slingshotBand.endWidth = bandWidth;
        slingshotBand.numCapVertices = 4;
        slingshotBand.useWorldSpace = true;
        slingshotBand.positionCount = 3 ;

        UpdateBand(launchPos);

    }

    Vector3 ArmTip(Transform arm)
    {
        return arm.position + arm.up * arm.lossyScale.y;
    }

    // Stretches the band from the left arm tip, through projPos, to the right arm

    void UpdateBand(Vector3 projPos)
    { 
       slingshotBand.SetPosition(0, ArmTip(leftArm));
       slingshotBand.SetPosition(1, projPos);
       slingshotBand.SetPosition(2, ArmTip(rightArm));
    
    }

    void Update()
    {
        if (!aimingMode) { return; }

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        UpdateBand(projPos);

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;

            if (launchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(launchSound, launchVolume);
            }

            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            UpdateBand(launchPos);
        }

    }


    void OnMouseEnter()
    {
        // print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        // print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;

        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;

    }
}