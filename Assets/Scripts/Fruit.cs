using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    protected abstract float size { get; }

    protected void ApplySize ()
    {
        transform.localScale = Vector3.one * size;
    }

    protected void Merge()
    {
        Debug.Log("Слияние!");
    }

    protected void Awake()
    {
        ApplySize();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (otherFruit == null)
            return;

        if (GetType() == otherFruit.GetType())
        {
            Merge();
        }
    }

}
