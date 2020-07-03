using Library.Exception;

namespace Library.Class
{
    public class FailedBuildToken
    {
        public bool IsFailed { get; set; }

        public void ThrowIfFailed()
        {
            if(IsFailed)
                throw new FailedBuildException();
        }
    }
}
