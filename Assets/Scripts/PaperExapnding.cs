using UnityEngine;

public class ExpandablePaper : MonoBehaviour
{
    public float expansionRate = 0.01f; // Rate at which the paper expands
    public Vector3 maxScale = new Vector3(0.07f, 1f, 1.5f); // Maximum scale the paper can reach
    public Vector3 minScale = new Vector3(0.04f, 1f, 0.07f); // Minimum scale the paper can shrink to
    private Cloth cloth;

    void Start()
    {
        cloth = GetComponent<Cloth>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E)) // Press 'E' to expand
        {
            cloth.enabled = false;
            ExpandPaper();
        }
        else
        {
            cloth.enabled = true;
        }
        if (Input.GetKey(KeyCode.Q)) // Press 'Q' to shrink
        {
            ShrinkPaper();
        }
    }

    void ExpandPaper()
    {
        Vector3 newScale = Vector3.Min(transform.localScale + new Vector3(0, 0, expansionRate) * Time.deltaTime, maxScale);
        AdjustClothConstraints(newScale);
        transform.localScale = newScale;
    }

    void ShrinkPaper()
    {
        Vector3 newScale = Vector3.Max(transform.localScale - new Vector3(0, 0, expansionRate) * Time.deltaTime, minScale);
        AdjustClothConstraints(newScale);
        transform.localScale = newScale;
    }

    void AdjustClothConstraints(Vector3 newScale)
    {
        // Adjust the cloth constraints based on the new scale
        Vector3 previousScale = transform.localScale;
        ClothSkinningCoefficient[] coefficients = cloth.coefficients;
        cloth.coefficients = coefficients;
        RestoreClothState(previousScale, newScale);
    }

    void RestoreClothState(Vector3 previousScale, Vector3 newScale)
    {
        // Adjust positions of the vertices in the cloth simulation to fit the new scale
        // This is a simplified approach and might need more fine-tuning based on your exact requirements

        Mesh clothMesh = new Mesh();
        cloth.GetComponent<SkinnedMeshRenderer>().BakeMesh(clothMesh);
        Vector3[] vertices = clothMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Vector3.Scale(vertices[i], new Vector3(newScale.x / previousScale.x, 1, newScale.z / previousScale.z));
        }

        clothMesh.vertices = vertices;
        cloth.GetComponent<SkinnedMeshRenderer>().sharedMesh = clothMesh;
    }
}
