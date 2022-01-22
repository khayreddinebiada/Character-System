namespace characters.Exceptions
{
    public class NonIntializedException : System.Exception
    {
        public NonIntializedException(string message = "The Object is not intialized") : base(message)
        {
            
        }
    }
}