using System;
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

        public static ActionResult Handle<TData>(
            this Result<TData> result,
            ControllerBase controller)
        {
            if(result.IsSuccess())
                return controller.Ok(result.Data);

            switch (result.Fault)
            {
                 case FaultCode.ReplaceMe:
                     return controller.BadRequest(new FaultCodeResponse() {
                         FaultCode = result.Fault.ToString()
                     });
                case FaultCode.Fail:
                default:
                    return new StatusCodeResult(500);
            }
        }
    }
}