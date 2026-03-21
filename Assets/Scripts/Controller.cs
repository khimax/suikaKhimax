using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject fruitPrefab;

    private Rigidbody2D fruitRb;
    private GameObject fruit;

    private Vector3 mousePos;

    //флаг чтобы не багалось и не падало сразу два фрукта
    private bool canDrop;
    
    //метод спавнящий фрукт
    public void SpawnFruit()
    {

        fruit = Instantiate(fruitPrefab, transform);

        //делаем его позицию 0 относительно родителя
        fruit.transform.localPosition = Vector3.zero;
        fruit.transform.localRotation = Quaternion.identity;

        //выключаем фрукту гравитацию
        fruitRb = fruit.GetComponent<Rigidbody2D>();
        fruitRb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    void Start()
    {
        SpawnFruit();
    }


    void Update()
    {
        
        transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            canDrop = true;
        }

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && canDrop)
        {
            canDrop = false;
            fruit.transform.SetParent(null);
            fruitRb.constraints = RigidbodyConstraints2D.None;
            SpawnFruit();
        }
    }
}
