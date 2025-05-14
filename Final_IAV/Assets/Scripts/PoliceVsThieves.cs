using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PoliceVsThieves : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI contLadrones;
    public static int contRegresiva = 10;
    void Start()
    {
        contLadrones.text = "Remaining Thieves: "+contRegresiva;
    }

    // Update is called once per frame
    void Update()
    {
        contRegresiva = GameObject.FindGameObjectsWithTag("Ladron").Length;
        contLadrones.text = "Remaining Thieves " + contRegresiva;
        if (contRegresiva == 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladron"))
        {
            Debug.Log("Ladron detectado " + other.name);
            other.enabled = false;
            Destroy(other.gameObject);
        }
    }
}
