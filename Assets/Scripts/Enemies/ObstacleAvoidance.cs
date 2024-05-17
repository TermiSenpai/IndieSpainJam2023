using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public float rayDistance = 5f; // Distance of the rays
    public int rayCount = 16; // Number of rays to cast
    public LayerMask obstacleLayer; // Layer to detect obstacles
    public float coneAngle = 90f; // Angle of the cone for forward rays

    // Calculates the best direction to avoid obstacles
    public Vector2 GetBestDirection(Vector2 targetDirection)
    {
        Vector2 bestDirection = targetDirection; // Initialize the best direction to the target direction
        float maxDistance = 0; // Initialize the maximum distance to 0

        targetDirection.Normalize(); // Normalize the target direction vector

        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the angle for this ray
            float angle = (i - rayCount / 2) * (coneAngle / rayCount);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * targetDirection; // Calculate the direction of the ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, obstacleLayer); // Cast a ray to detect obstacles

            if (hit.collider == null)
            {
                // No obstacle in this direction
                if (rayDistance > maxDistance)
                {
                    // Update the best direction and maximum distance
                    bestDirection = direction;
                    maxDistance = rayDistance;
                    Debug.DrawRay(transform.position, direction * rayDistance, Color.green); // Draw a debug ray in green
                }
            }
            else
            {
                // Obstacle detected
                float distance = hit.distance;
                if (distance > maxDistance)
                {
                    // Update the best direction and maximum distance
                    bestDirection = direction;
                    maxDistance = distance;
                    Debug.DrawRay(transform.position, direction * distance, Color.red); // Draw a debug ray in red
                }
            }
        }

        // Visualize the best path (in blue)
        Debug.DrawRay(transform.position, bestDirection * rayDistance, Color.blue);

        return bestDirection; // Return the best direction to avoid obstacles
    }

    // The following method is commented out to prevent drawing Gizmos in the scene view
    //private void OnDrawGizmos()
    //{
    //    // Draw a circumference with the rays
    //    Gizmos.color = Color.green;
    //    float angleIncrement = 360f / rayCount; // Calculate the angle between each ray
    //    for (int i = 0; i < rayCount; i++)
    //    {
    //        float angle = i * angleIncrement; // Calculate the angle for this ray
    //        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up; // Calculate the direction of the ray
    //        Gizmos.DrawRay(transform.position, direction * rayDistance); // Draw the ray
    //    }
    //}
}
