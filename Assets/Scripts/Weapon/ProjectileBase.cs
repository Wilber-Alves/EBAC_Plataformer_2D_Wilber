using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector2 direction;
    public float timeToDestroy = 2.4f;
    public float side = 1.0f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);

    }

    private void Start()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * side;
        transform.localScale = newScale;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }
}
