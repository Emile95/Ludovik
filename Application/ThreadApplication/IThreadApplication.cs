using System;

namespace Application.ThreadApplication
{
    public interface IThreadApplication
    {
        void AddInterval(int sec, Action action);
    }
}
