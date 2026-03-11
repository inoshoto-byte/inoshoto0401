using UnityEngine;

public class PropTransform : MonoBehaviour
{
    public GameObject playerModel;
    public GameObject propObject;
    public Transform cameraPivot;

    public Vector3 thirdPersonPosition = new Vector3(0, 1.5f, -3);

    CharacterController controller;
    Rigidbody rb;
    BoxCollider box;

    SimplePlayerController playerMove;
    PropMovement propMove; // ←追加

    bool isProp = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        playerMove = GetComponent<SimplePlayerController>();
        propMove = GetComponent<PropMovement>(); // ←追加

        rb.isKinematic = true;
        box.enabled = false;

        propMove.enabled = false; // ←最初はOFF
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isProp)
        {
            TransformToProp();
        }
    }

    void TransformToProp()
    {
        isProp = true;

        playerModel.SetActive(false);

        propObject.SetActive(true);
        propObject.transform.position = transform.position;
        propObject.transform.SetParent(transform);

        propObject.transform.localPosition = new Vector3(0, -0.05f, 0);

        cameraPivot.localPosition = thirdPersonPosition;

        // 人間モードOFF
        playerMove.enabled = false;
        controller.enabled = false;

        // プロップ判定
        BoxCollider propCol = propObject.GetComponent<BoxCollider>();

        box.size = propCol.size;
        box.center = propCol.center;

        box.enabled = true;

        // 物理ON
        rb.isKinematic = false;

        // プロップ移動ON
        propMove.enabled = true;
    }
}