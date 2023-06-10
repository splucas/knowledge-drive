using UnityEngine;

public class LaneLineMover : MonoBehaviour
{
    // Normal Line Movement Speed
    public float Speed = 2;
    // Movement Speed when player presses "up"
    public float HighSpeed = 6;

    // Scene camera; if left null, uses main
    public Camera ViewCamera = null;

    // Spawned gameobject that represents the lane markers
    public GameObject LinesPrefab = null;
    // Distance between the marker objects
    public float LineSpacing = 3.5f;

    float currentSpeed;
    float yReset;


    private void Awake()
    {
        currentSpeed = Speed;
        if (ViewCamera == null)
            ViewCamera = Camera.main;
    }
    private void OnEnable()
    {
        GoFast(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawLaneMarkers();
    }


    /// <summary>
    /// Draws lane markers based on the number of markers needed to fill the camera viewport
    /// space.
    /// </summary>
    void DrawLaneMarkers()
    {
        Vector3 topCorner = ViewCamera.ViewportToWorldPoint(new Vector3(0, 1, ViewCamera.nearClipPlane));

        int numMarkers = Mathf.CeilToInt(topCorner.y / LineSpacing);
        for (int cnt = 0; cnt <= numMarkers; cnt++)
        {
            yReset = cnt * LineSpacing;

            Vector3 newPos = new Vector3(0, yReset, 0);
            GameObject newLaneMarker = Instantiate(LinesPrefab, newPos, Quaternion.identity);
            newLaneMarker.transform.SetParent(transform, true);
            // Draw the lower markers
            if (yReset > 0.0f)
            {
                newPos.y = -yReset;
                newLaneMarker = Instantiate(LinesPrefab, newPos, Quaternion.identity);
                newLaneMarker.transform.SetParent(transform, true);
            }
        }

    }

    public void GoFast(bool tf)
    {
        if (tf)
            currentSpeed = HighSpeed;
        else
            currentSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        for(int cnt = 0; cnt < transform.childCount; cnt ++)
        {

            Transform kid = transform.GetChild(cnt);
            Vector3 pos = kid.position;
            pos.y -= Time.deltaTime * currentSpeed;

            if (pos.y < -yReset)
                pos.y = yReset;


            kid.position = pos;

        }
    }
}
