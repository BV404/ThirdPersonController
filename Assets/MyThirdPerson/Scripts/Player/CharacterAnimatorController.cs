using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField] float horizontalParameter = 0f;
    [SerializeField] float verticalParameter = 0f;
    [SerializeField] float transitionSpeed = 1f;

    Animator animator;

    private float HorizontalTarget { get; set; }
    private float VerticalTarget { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalTarget = Mathf.Abs(InputManager.instance.HorizontalInput) > 0 ? 0.5f: 0f;
        if (InputManager.instance.IsSprinting)
            VerticalTarget = 1;
        else 
            VerticalTarget = Mathf.Abs(InputManager.instance.VerticalInput) > 0 ? 0.5f : 0f;

        GoToTarget(ref horizontalParameter, HorizontalTarget);
        GoToTarget(ref verticalParameter, VerticalTarget);

        animator.SetFloat("Velocity", 
            Mathf.Max(Mathf.Abs(horizontalParameter), Mathf.Abs(verticalParameter)));
    }

    bool GoToTarget(ref float num, float tar)
    {
        if (num - tar > 0.1f)
        {
            num -= transitionSpeed * Time.deltaTime;
            SnapToTarget(ref num, tar);
        }
        else if (tar - num > 0.1f)
        {
            num += transitionSpeed * Time.deltaTime;
            SnapToTarget(ref num, tar);
        }
        return false;
    }

    void SnapToTarget(ref float num, float tar)
    {
        if (Mathf.Abs(num - tar) <= 0.1f)
        {
            num = tar;
        }
    }

}
