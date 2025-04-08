using UnityEngine;

namespace com.ganast.Unity {

    public class OrientationGameObjectActivator : MonoBehaviour {

        [SerializeField]
        private GameObject[] gameobjects;

        public void Start() {
        }

        public void Update() {

            float x = transform.eulerAngles.x;

            if ((x > 0 && x < 60) || (x > 330 && x < 360)) {
                SetGameObjectsActive(true);
            }
            else {
                SetGameObjectsActive(false);
            }
        }

        protected void SetGameObjectsActive(bool active) {

            if (gameobjects != null && gameobjects.Length != 0) {
                foreach (GameObject g in gameobjects) {
                    g.SetActive(active);
                }
            }
            else {
                for (int i = 0; i != transform.childCount; i++) {
                    transform.GetChild(i).gameObject.SetActive(active);
                }
            }
        }
    }
}