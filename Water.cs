using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Water : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject ship;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void RespawnShip()
    {
        ship.SetActive(false);
        ship.transform.position = new Vector3(1692.1f, 35.4f, 1427.4f);
        ship.transform.rotation = new Quaternion(0f, -0.5f, 0, 0.9f);
        ship.SetActive(true);
    }

    private void RespawnPlayer()
    {
        // Fix uniemozliwiajacy bugowanie teleportu
        player.SetActive(false);
        player.transform.position = new Vector3(1719f, 45f, 1450f);
        player.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz tonie!");
            RespawnPlayer();
            RespawnShip();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Die");
    }
}
