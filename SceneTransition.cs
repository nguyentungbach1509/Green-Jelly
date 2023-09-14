using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneName;
    public Animator animation;
    [SerializeField]
    private string areaPassword;

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player") && !other.isTrigger) {
           PlayerMovement.instance.scenePassword = areaPassword;
           StartCoroutine(SceneEffect());
           if(areaPassword.CompareTo("Desert") == 0) {
               AudioManager audioManager = FindObjectOfType<AudioManager>();
               audioManager.DuringSound(areaPassword);
           }
       }
    }

    IEnumerator SceneEffect() {
        animation.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
