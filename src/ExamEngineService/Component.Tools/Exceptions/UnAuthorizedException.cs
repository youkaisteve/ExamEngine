namespace Component.Tools.Exceptions
{
    public class UnAuthorizedException : BusinessException
    {
        public UnAuthorizedException()
            : base("未授权")
        {
        }
    }
}