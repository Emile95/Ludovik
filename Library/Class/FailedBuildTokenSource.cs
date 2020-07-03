namespace Library.Class
{
    public class FailedBuildTokenSource
    {
        public FailedBuildToken Token { get; private set; }

        public FailedBuildTokenSource()
        {
            Token = new FailedBuildToken();
        }

        public void Failed()
        {
            Token.IsFailed = true;
        }
    }
}
