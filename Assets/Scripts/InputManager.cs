using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //create a singleton
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();  // Event to pass movement direction
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent<Vector3> OnRightClick = new UnityEvent<Vector3>(); // Event for dash


    public static InputManager Instance;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI dashText;  // Reference to Dash status text
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
