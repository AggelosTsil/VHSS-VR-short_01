using UnityEngine;

public interface ICondition {
    public bool IsTrue();
}

public class BaseMonoBehaviourCondition: MonoBehaviour, ICondition {
    public virtual bool IsTrue() {
        return false;
    }
}
