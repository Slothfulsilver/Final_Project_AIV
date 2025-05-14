using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ThievesVsCivilians : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI contCiviles;
    public static int contRegresiva = 10;
    void Start()
    {
        contCiviles.text = "Remaining Civilians: "+contRegresiva;
    }

    // Update is called once per frame
    void Update()
    {
        contRegresiva = GameObject.FindGameObjectsWithTag("Civil").Length;
        contCiviles.text = "Remaining Civilians: " + contRegresiva;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladron"))
        {
            this.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
