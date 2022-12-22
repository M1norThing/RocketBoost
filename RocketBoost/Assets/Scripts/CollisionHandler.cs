using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                ReloadScene();
                    break;

            case "Finish":
                LoadNextLevel();
                break;

            default:
                Debug.Log("Game over");
                break;
        }

         void ReloadScene()
        {
            SceneManager.LoadScene(currentSceneIndex);
        }

        void LoadNextLevel()
        {
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }

    }
}
