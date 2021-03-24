namespace ToTheMoon.Api {
    public sealed class Result<TData>
    {
        public TData Data { get; }
        public FaultCode Fault { get; }
        private readonly bool _isSuccess;

        private Result(TData data)
        {
            Data = data;
            _isSuccess = true;
        }
        private Result(FaultCode fault)
        {
            Fault = fault;
            _isSuccess = false;
        }

        public static Result<TData> Success(TData data) => new(data);

        public static Result<TData> Failed(FaultCode fault) => new(fault);

        public bool IsSuccess() => _isSuccess;
    }
}