namespace Nut.Core.Models
{
    public interface INutViewModel
    {
        object State { get; set; }
        void Start();
        void Resume();
        void Pause();
        void Stop();
        void Finish();
    }
}