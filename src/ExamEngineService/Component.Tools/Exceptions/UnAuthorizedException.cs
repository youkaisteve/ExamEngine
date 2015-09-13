namespace Component.Tools.Exceptions
{
    public class UnAuthorizedException : BusinessException
    {
        public override int ExceptionCode
        {
            get { return 3; }
        }

        public UnAuthorizedException()
            : base("未授权")
        {
        }
    }
}