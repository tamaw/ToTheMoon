using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Extensions
{
    public static class ResultExtensions
    {
        public static Result<TOutData> OnSuccess<TInData, TOutData>(
            this Result<TInData> input,
            Func<TInData, Result<TOutData>> switchFunction)
        {
            return input.IsSuccess()
                    ? switchFunction(input.Data)
                    : Result<TOutData>.Failed(input.Fault);
        }

        public static Result<TOutData> OnSuccess<TInData, TOutData>(
            this Result<TInData> input,
            Func<TInData, TOutData> function)
        {
            return input.IsSuccess()
                    ? Result<TOutData>.Success(function(input.Data))
                    : Result<TOutData>.Failed(input.Fault);
        }

        public static Task<Result<TOutData>> OnSuccess<TInData, TOutData>(
            this Result<TInData> input,
            Func<TInData, Task<Result<TOutData>>> function)
        {
            return input.IsSuccess()
                    ? function(input.Data)
                    : Task.FromResult(Result<TOutData>.Failed(input.Fault));
        }

        public static Task<Result<TOutData>> OnSuccess<TInData, TOutData>(
            this Task<Result<TInData>> inputTask,
            Func<TInData, Result<TOutData>> switchFunction)
        {
            return inputTask.ContinueWith(t => t.Result.IsSuccess()
                ? switchFunction(t.Result.Data)
                : Result<TOutData>.Failed(t.Result.Fault));
        }

        public static async Task<Result<TOutData>> OnSuccess<TInData, TOutData>(
            this Result<TInData> input,
            Func<TInData, Task<TOutData>> function)
        {
            return input.IsSuccess()
                    ? Result<TOutData>.Success(await function(input.Data))
                    : Result<TOutData>.Failed(input.Fault);
        }

        public static Task<ActionResult> Handle<TInData>(
            this Task<Result<TInData>> inputTask,
            ControllerBase controller)
        {
            return inputTask.ContinueWith(t => t.Result.IsSuccess()
                    ? controller.Ok(t.Result.Data)
                    : HandleResult(t.Result.Fault, controller));
        }

        public static ActionResult Handle<TData>(
            this Result<TData> result,
            ControllerBase controller)
        {
            return result.IsSuccess()
                ? controller.Ok(result.Data)
                : HandleResult(result.Fault, controller);
        }

        private static ActionResult HandleResult(FaultCode fault, ControllerBase controller)
        {
            switch (fault)
            {
                 case FaultCode.CoinNotProvided:
                 case FaultCode.ErrorRetrievingCoinPrice:
                 case FaultCode.FailedToReadResponseData:
                 case FaultCode.CoinUnknownOrNotAccepted:
                     return controller.BadRequest(new FaultCodeResponse() {
                         FaultCode = fault.ToString()
                     });
                case FaultCode.Fail:
                default:
                    return new StatusCodeResult(500);
            }
        }

    }
}