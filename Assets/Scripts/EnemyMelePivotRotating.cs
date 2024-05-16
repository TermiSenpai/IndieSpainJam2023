using UnityEngine;

public class EnemyMelePivotRotating : MonoBehaviour
{
    public float rotationSpeed = 45.0f;
    public float distance = 2.0f;
    public float stopAngle = 30.0f; // Angle in degrees to stop rotation
    Transform target;
    FollowState followState;

    private bool stopRotation = false;

    private void Start()
    {
        followState = GetComponentInParent<FollowState>();
    }

    void Update()
    {
        UpdateTarget();
        if (target == null) return;

        RotateTowardsTarget();
        CheckStopRotation();
        SetRotationSpeed();
    }

    private void UpdateTarget()
    {
        target = followState.currentTarget;
    }

    private void RotateTowardsTarget()
    {
        // Calculate the direction towards the target from the object's position.
        Vector3 targetDirection = (target.position - transform.position).normalized;

        // Calculate the rotation angle in degrees.
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // Apply rotation to the object.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void CheckStopRotation()
    {
        // Check if the angle is less than the stop angle.
        if (Mathf.Abs(transform.rotation.eulerAngles.z) <= stopAngle)
            stopRotation = true;
        else
            stopRotation = false;

    }

    private void SetRotationSpeed()
    {
        // Set the rotation speed based on whether we should stop or not.
        if (stopRotation)
            rotationSpeed = 0; // Stop rotation.
        else
            rotationSpeed = 45.0f; // enable rotation.

    }
}
