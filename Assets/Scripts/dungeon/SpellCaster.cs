using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireProjectilePrefab;
    public GameObject waterProjectilePrefab;
    public GameObject earthProjectilePrefab;

    private Camera mainCamera;
    private Player player;
    private Vector2 aimDirection;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Get mouse aim direction
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        aimDirection = (worldPos - transform.position).normalized;

        // Draw aiming line
        Debug.DrawLine(transform.position, transform.position + (Vector3)aimDirection * 20f, Color.yellow);

        // Select element with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.currentElement = ElementType.Fire;
            Debug.Log("Selected: FIRE");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.currentElement = ElementType.Water;
            Debug.Log("Selected: WATER");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.currentElement = ElementType.Earth;
            Debug.Log("Selected: EARTH");
        }

        // Cast spell with mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CastSpell();
        }
    }

    private void CastSpell()
    {
        ElementType element = player.currentElement;

        GameObject projectilePrefab = null;

        switch (element)
        {
            case ElementType.Fire:
                projectilePrefab = fireProjectilePrefab;
                break;
            case ElementType.Water:
                projectilePrefab = waterProjectilePrefab;
                break;
            case ElementType.Earth:
                projectilePrefab = earthProjectilePrefab;
                break;
        }

        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            proj.Launch(aimDirection, element);

            Debug.Log($"Cast {element}!");
        }
    }
}