using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private bool hasEntered;
    public GameObject DeathParticles;
    public Transform respawn;
    public Transform player;
    public SpriteRenderer sprite;

    public GameOverScreen GameOverScreen;

  public void OnCollisionEnter2D(Collision2D collision) 
  { 
    if (collision.gameObject.tag == ("Enemy") && !hasEntered) 
    {
      StartCoroutine(Respawn());
      DeathParticles.SetActive(true);
      
      
    }
  }

  IEnumerator Respawn(){
    while (true)
    {
      Debug.Log("Coroutine Started");
    	hasEntered = true;
      sprite.enabled = false;
      yield return new WaitForSeconds(2);
      DeathParticles.SetActive(false);
      SceneManager.LoadScene("Game Over L1");
      //sprite.enabled = true;
      //player.position = respawn.position;
			//LevelManager.instance.Respawn();
      hasEntered = false;
      yield break;
    }


  }
}

