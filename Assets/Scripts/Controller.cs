using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject[] fruitPrefabs;
    [SerializeField] private GameObject GameOverPanel;

    private Rigidbody2D fruitRb;
    private Fruit fruitScript;
    private GameObject fruit;

    private float spawnCooldown = 0.5f;

    private Vector3 mousePos;
    private float mousePosX;

    //флаг чтобы не багалось и не падало сразу два фрукта
    private bool canDrop;
    private bool inputLocked;
    private bool isGameOver = false;

    public static Controller Instance;

    //метод спавнящий фрукт
    public void SpawnFruit()
    {

        fruit = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], transform);

        fruitScript = fruit.GetComponent<Fruit>();

        //делаем его позицию 0 относительно родителя
        fruit.transform.localPosition = Vector3.zero;
        fruit.transform.localRotation = Quaternion.identity;

        //выключаем фрукту гравитацию
        fruitRb = fruit.GetComponent<Rigidbody2D>();
        fruitRb.constraints = RigidbodyConstraints2D.FreezePositionY;

        inputLocked = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        GameOverPanel.SetActive(true);
    }
    
    void Start()
    {
        Instance = this;
        SpawnFruit();
    }


    void Update()
    {
        //блокируем функцию до спавна фрукта
        if (inputLocked || isGameOver) return;



        //фрукт следует за мышью если лкм нажат
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //делаем ограничение по X
            mousePosX = Mathf.Clamp(mousePos.x, -2.2f, 2.2f);
        }

        //контроллер копирует позицию мышки по X
        transform.position = new Vector3(mousePosX, transform.position.y, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            canDrop = true;
        }

        if (Input.GetMouseButtonUp(0) && canDrop)
        {
            canDrop = false;
            inputLocked = true;
            fruit.transform.SetParent(null);
            fruitRb.constraints = RigidbodyConstraints2D.None;

            //вызывает Invoke(nameof(InvinsibleOff), 2f); из кода Fruit
            fruitScript.StartInvinsibleTimer();

            Invoke(nameof(SpawnFruit), spawnCooldown);
        }
    }


}
