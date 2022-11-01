using UnityEngine;

namespace Gamekit2D
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Checkpoint : MonoBehaviour
    {
        public bool respawnFacingLeft;
        public int indexSceneCheckPoint;

        private void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true; 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerCharacter c = collision.GetComponent<PlayerCharacter>();
            if(c != null)
            {
                c.SetChekpoint(this);

                SaveManager.instance.dataSave.Life = c.gameObject.GetComponent<Damageable>().CurrentHealth;
                SaveManager.instance.dataSave.enumScene = DataScene.instance.enumSceneName;
                SaveManager.instance.dataSave.IndexCheckPoint = indexSceneCheckPoint;
                DataScene.instance.UpdateDataSceneSave();
            }
        }
    }
}