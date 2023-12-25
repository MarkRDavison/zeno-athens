namespace mark.davison.athens.web.features.State;

public interface IStateHelper
{
    IDisposable Force();
    TimeSpan DefaultRefetchTimeSpan { get; }
}
