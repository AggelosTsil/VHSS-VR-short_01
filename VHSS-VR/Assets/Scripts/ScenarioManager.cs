using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager: MonoBehaviour {

    public static float E = 0.001f;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Image questLog;

    [SerializeField]
    private Sprite baseQuestLogImage;

    [Header("Phase 1")]

    [SerializeField]
    private GameObject[] winchHotspots;

    [SerializeField]
    private Image winchBadge;

    [SerializeField]
    private Sprite winchQuestLogImage;

    [SerializeField]
    private Sprite winchPendingBadge;

    [SerializeField]
    private Sprite winchCurrentBadge;

    [SerializeField]
    private Sprite winchDoneBadge;

    [Header("Phase 2")]

    [SerializeField]
    private GameObject[] tourketinaHotspots;

    [SerializeField]
    private Image tourketinaBadge;

    [SerializeField]
    private Sprite tourketinaQuestLogImage;

    [SerializeField]
    private Sprite tourketinaPendingBadge;

    [SerializeField]
    private Sprite tourketinahCurrentBadge;

    [SerializeField]
    private Sprite tourketinaDoneBadge;

    [Header("Phase 3")]

    [SerializeField]
    private GameObject[] maistraHotspots;

    [SerializeField]
    private Image maistraBadge;

    [SerializeField]
    private Sprite maistraQuestLogImage;

    [SerializeField]
    private Sprite maistraPendingBadge;

    [SerializeField]
    private Sprite maistraCurrentBadge;

    [SerializeField]
    private Sprite maistraDoneBadge;

    [Header("Phase 4")]

    [SerializeField]
    private GameObject[] wheelHotspots;

    [SerializeField]
    private Image wheelBadge;

    [SerializeField]
    private Sprite wheelQuestLogImage;

    [SerializeField]
    private Sprite wheelPendingBadge;

    [SerializeField]
    private Sprite wheelCurrentBadge;

    [SerializeField]
    private Sprite wheelDoneBadge;

    private Dictionary<ScenarioStep, Dictionary<ScenarioStep, AdvanceCondition[]>> scenario;

    private ScenarioStep currentStep;

    // --- the horror ----------------------------------------------------------
    private AgkyraMazemenh agkyraMazemenh = new AgkyraMazemenh();
    private TourketinaDemenh tourketinaDemenh = new TourketinaDemenh();
    private MaistraDemenh maistraDemenh = new MaistraDemenh();
    public void SetAgkyraMazemenh(bool b) {
        agkyraMazemenh.satisfied = b;
    }
    public void SetTourketinaDemenh(bool b) {
        tourketinaDemenh.satisfied = b;
    }
    public void SetMaistraDemenh(bool b) {
        maistraDemenh.satisfied = b;
    }
    // -------------------------------------------------------------------------
    
    protected void SetObjectsActive(GameObject[] objects, bool active) {
        foreach (GameObject o in objects) {
            o.SetActive(active);
        }
    }

    public void Start() {

        SetObjectsActive(winchHotspots, false);
        SetObjectsActive(tourketinaHotspots, false);
        SetObjectsActive(maistraHotspots, false);
        SetObjectsActive(wheelHotspots, false);

        /*
        // free scenario...

        ScenarioStep step1 = new ScenarioStep("’γκυρα", new GameObject[] { winchLocation });
        ScenarioStep step2 = new ScenarioStep("Πανιά", new GameObject[] { skotaTourketinasLocation, skotaMaistrasLocation });
        ScenarioStep step3 = new ScenarioStep("Τιμόνι", new GameObject[] { wheelLocation });

        scenario = new Dictionary<ScenarioStep, Dictionary<ScenarioStep, AdvanceCondition[]>>() {

            { step1, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step2, new AdvanceCondition[] {
                    // new AgkyraMazemenh
                } }
            } },

            { step2, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step3, new AdvanceCondition[] {
                    // new TourketinaDemenh
                    // new MaistraDemenh
                } }
            } }
        };
        */

        /*
        // no turning-back scenario...

        ScenarioStep step1 = new ScenarioStep("’γκυρα", new GameObject[] { winchLocation });
        ScenarioStep step2a = new ScenarioStep("Πανιά λυτά", new GameObject[] { skotaTourketinasLocation, skotaMaistrasLocation });
        ScenarioStep step2b = new ScenarioStep("Τουρκετίνα δεμένη", new GameObject[] { skotaMaistrasLocation });
        ScenarioStep step2c = new ScenarioStep("Μαΐστρα δεμένη", new GameObject[] { skotaTourketinasLocation });
        ScenarioStep step3 = new ScenarioStep("Τιμόνι", new GameObject[] { wheelLocation });

        scenario = new Dictionary<ScenarioStep, Dictionary<ScenarioStep, AdvanceCondition[]>>() {

            { step1, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step2a, new AdvanceCondition[] {
                    // new AgkyraMazemenh
                } }
            } },

            { step2a, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step2b, new AdvanceCondition[] {
                    // new TourketinaDemenh
                } },
                { step2c, new AdvanceCondition[] {
                    // new MaistraDemenh
                } },
            } },

            { step2b, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step3, new AdvanceCondition[] {
                    // new TourketinaDemenh
                    // new MaistraDemenh
                } }
            } },

            { step2c, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step3, new AdvanceCondition[] {
                    // new TourketinaDemenh
                    // new MaistraDemenh
                } }
            } }
        };
        */

        // sequential scenario...

        ScenarioStep step1 = new ScenarioStep("’γκυρα", winchHotspots, winchBadge, winchPendingBadge, winchCurrentBadge, winchDoneBadge, winchQuestLogImage);
        ScenarioStep step2a = new ScenarioStep("Πανιά λυτά", tourketinaHotspots, tourketinaBadge, tourketinaPendingBadge, tourketinahCurrentBadge, tourketinaDoneBadge, tourketinaQuestLogImage);
        ScenarioStep step2b = new ScenarioStep("Τουρκετίνα δεμένη", maistraHotspots, maistraBadge, maistraPendingBadge, maistraCurrentBadge, maistraDoneBadge, maistraQuestLogImage);
        ScenarioStep step3 = new ScenarioStep("Τιμόνι", wheelHotspots, wheelBadge, wheelPendingBadge, wheelCurrentBadge, wheelDoneBadge, wheelQuestLogImage);

        scenario = new Dictionary<ScenarioStep, Dictionary<ScenarioStep, AdvanceCondition[]>>() {

            { step1, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step2a, new AdvanceCondition[] {
                    agkyraMazemenh
                } }
            } },

            { step2a, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step2b, new AdvanceCondition[] {
                    tourketinaDemenh
                } }
            } },

            { step2b, new Dictionary<ScenarioStep, AdvanceCondition[]>() {
                { step3, new AdvanceCondition[] {
                    maistraDemenh
                } }
            } }
        };

        questLog.sprite = baseQuestLogImage;

        Transition(step1);
    }

    protected void Transition(ScenarioStep step) {
        Debug.LogFormat("[ScenarioManager] Scenario step transition, {0} -> {1}...", currentStep != null ? currentStep.GetStepName() : "(none)", step != null ? step.GetStepName() : "(none)");
        if (currentStep != null) {
            currentStep.OnExit();
        }
        currentStep = step;
        if (currentStep != null) {
            currentStep.OnEntry();
        }
        questLog.sprite = currentStep.GetQuestLogImage();
    }

    public void Update() {
        
        if (currentStep != null) {

            if (scenario.TryGetValue(currentStep, out Dictionary<ScenarioStep, AdvanceCondition[]> s)) {

                foreach (KeyValuePair<ScenarioStep, AdvanceCondition[]> e in s) {

                    bool b = true;

                    foreach (AdvanceCondition c in e.Value) {
                        if (!c.Satisfied()) {
                            b = false;
                            break;
                        }
                    }
                
                    if (b) {
                        Transition(e.Key);
                        break;
                    }
                }
            }
        }
    }

    // --- more horror ---------------------------------------------------------
    public class AgkyraMazemenh: AdvanceCondition {

        public bool satisfied = false;

        public bool Satisfied() {
            return satisfied;
        }
    }
    // -------------------------------------------------------------------------

    // --- more horror ---------------------------------------------------------
    public class TourketinaDemenh: AdvanceCondition {

        public bool satisfied = false;

        public bool Satisfied() {
            return satisfied;
        }
    }
    // -------------------------------------------------------------------------

    // --- more horror ---------------------------------------------------------
    public class MaistraDemenh: AdvanceCondition {

        public bool satisfied = false;

        public bool Satisfied() {
            return satisfied;
        }
    }
    // -------------------------------------------------------------------------
}

public class ScenarioStep {

    private string stepName;

    private GameObject[] teleportTargets;

    private Image badge;

    private Sprite pending;
    private Sprite current;
    private Sprite done;

    private Sprite questLogImage;

    public ScenarioStep(string stepName, GameObject[] teleportTargets, Image badge, Sprite pending, Sprite current, Sprite done, Sprite questLogImage) {
        this.stepName = stepName;
        this.teleportTargets = teleportTargets;
        this.badge = badge;
        this.pending = pending;
        this.current = current;
        this.done = done;
        this.questLogImage = questLogImage;
        Reset();
    }

    public virtual void OnEntry() {
        badge.sprite = current;
        foreach (GameObject teleportTarget in teleportTargets) {
            teleportTarget.SetActive(true);
        }
    }

    public virtual void OnExit() {
        foreach (GameObject teleportTarget in teleportTargets) {
            teleportTarget.SetActive(false);
        }
        badge.sprite = done;
    }

    public string GetStepName() {
        return stepName;
    }

    public Sprite GetQuestLogImage() {
        return questLogImage;
    }

    public void Reset() {
        badge.sprite = pending;
    }
}

public interface AdvanceCondition {

    public bool Satisfied();
}

