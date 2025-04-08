using UnityEngine;

namespace com.ganast.Unity {

    public class OrientationComponentActivator : MonoBehaviour {

        [SerializeField]
        private MonoBehaviour component;

        public void Start() {
        }

        public void Update() {

            if (component != null) {

                float x = transform.eulerAngles.x;

                if ((x > 0 && x < 60) || (x > 330 && x < 360)) {
                    component.enabled = true;
                }
                else {
                    component.enabled = false;
                }
            }
        }
    }

}