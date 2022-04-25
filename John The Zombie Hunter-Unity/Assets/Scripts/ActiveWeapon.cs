using UnityEngine;
//using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public UnityEngine.Animations.Rigging.Rig handIK;
    private Weapon currentWeapon;
    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;
    public Transform weaponParent;
    private Animator animator;
    private AnimatorOverrideController animatorOverride;
    public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animatorOverride = animator.runtimeAnimatorController as AnimatorOverrideController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentWeapon)
        {
            handIK.weight = 1f;
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            handIK.weight = 0f;
            animator.SetLayerWeight(1, 0);
        }

        if (currentWeapon && Input.GetButton("Fire1"))
            currentWeapon.Shoot();
    }

    public void Equip(Weapon newWeapon)
    {
        if (currentWeapon)
            Destroy(currentWeapon.gameObject);

        currentWeapon = Instantiate(newWeapon, weaponParent);
        Invoke(nameof(SetOverrideAnim), .001f);
    }

    void SetOverrideAnim()
    {
        animatorOverride["weapon_empty"] = currentWeapon.weaponAnimationClip;
    }

    #region Animation saving
    /***
    [ContextMenu("Save pose")]
    void SaveWeaponPos()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        recorder.TakeSnapshot(0f);
        recorder.SaveToClip(currentWeapon.weaponAnimationClip);
        UnityEditor.AssetDatabase.SaveAssets();
    }
    ***/
    #endregion
}
