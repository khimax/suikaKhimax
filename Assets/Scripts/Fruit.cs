using Unity.VisualScripting;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    protected abstract float size { get; }

    private bool isMerging = false;

    [SerializeField] protected GameObject nextFruitPrefab;

    protected void ApplySize ()
    {
        transform.localScale = Vector3.one * size;
    }

    protected virtual void Merge(Fruit otherFruit)
    {
        //вычисляем позицию между фруктами
        Vector3 mergePoint = (transform.position + otherFruit.transform.position) / 2f;

        //создаем фрукт
        Instantiate(nextFruitPrefab, mergePoint, Quaternion.identity);

        //удаляем предыдущие
        Destroy(gameObject);
        Destroy(otherFruit.gameObject);

        Debug.Log("Слияние!");

    }

    protected void Awake()
    {
        ApplySize();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //если другой фрукт уже начал слияние выйти из метода

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        //проверка на существование фрукта
        if (otherFruit == null)
            return;

        if (GetInstanceID() < otherFruit.gameObject.GetInstanceID()) return;

        if (isMerging || otherFruit.isMerging) return;

        if (GetType() == otherFruit.GetType())
        {
            //Debug.Log(isMerging);
            //чтобы не сливалось три фрукта
            isMerging = true;
            otherFruit.isMerging = true;
            //Debug.Log(GetType());
            Merge(otherFruit);
        }
    }

}
