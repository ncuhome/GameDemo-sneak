using UnityEngine;
using System.Collections;
namespace TestScript {
    public class testGameStateManager : MonoBehaviour
    {
        public static testGameStateManager Instace;
        private int inCaluNavObject = 0;
        private void Awake() {
            if (Instace == null) {
                Instace = this;
            } else {
                Debug.Log("MoreThanOneManager");
                Destroy(this);
            }
        }

        private void PasteGame() {
            Time.timeScale = 0;
        }

        private void RestartGame() {
            Time.timeScale = 1;
        }

        public void ApplyNavPathCaluTime() {
            if (inCaluNavObject == 0) {
                inCaluNavObject += 1;
                StartCoroutine(WaitCaluNavObject());
            } else {
                inCaluNavObject += 1;
            }
        }

        public void navPathCaluOver() {
            inCaluNavObject -= 1;
            if (inCaluNavObject < 0) {
                Debug.LogWarning("WTFHappen");
                inCaluNavObject = 0;
            }
        }

        IEnumerator WaitCaluNavObject() {
            PasteGame();
            while (inCaluNavObject != 0) {
                yield return 0;
            }
            RestartGame();
        }
    }
}


