namespace characters
{
    public class DestinationTransfrom : MovementDestination
    {
        /// <summary>
        /// This function is execute everytime when the AllowMove return true. You can handle the movement of character from here.
        /// </summary>
        public override void MoveHandle(float deltaTime)
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, destination, movingSpeed * deltaTime);
        }
    }
}
