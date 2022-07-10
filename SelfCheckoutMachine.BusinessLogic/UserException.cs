namespace SelfCheckoutMachine.BusinessLogic
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException(string message) : base(message) { }
    }
}
