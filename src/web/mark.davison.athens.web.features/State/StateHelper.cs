namespace mark.davison.athens.web.features.State;

public class StateHelper : IStateHelper
{
    private class StateHelperDisposable : IDisposable
    {
        private bool _disposedValue;
        private readonly StateHelper _stateHelper;

        public StateHelperDisposable(StateHelper stateHelper)
        {
            _stateHelper = stateHelper;
            _stateHelper._force = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _stateHelper._force = false;
                    _disposedValue = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

    private readonly ICQRSDispatcher _dispatcher;
    private readonly IStateStore _stateStore;
    private readonly IDateService _dateService;

    internal bool _force;

    public StateHelper(
        ICQRSDispatcher dispatcher,
        IStateStore stateStore,
        IDateService dateService
    )
    {
        _dispatcher = dispatcher;
        _stateStore = stateStore;
        _dateService = dateService;
    }

    public IDisposable Force() => new StateHelperDisposable(this);

    private bool RequiresRefetch(DateTime stateLastModified, TimeSpan interval)
    {
        return _force || _dateService.Now - stateLastModified > interval;
    }
    public TimeSpan DefaultRefetchTimeSpan => TimeSpan.FromMinutes(1);
}
