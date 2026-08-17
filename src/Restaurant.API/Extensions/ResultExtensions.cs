using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Models.Results;

namespace Restaurant.API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(
            this ControllerBase controller,
            Result<T> result)
        {
            return controller.StatusCode(result.StatusCode, result);
        }

        public static IActionResult ToActionResult(
            this ControllerBase controller,
            Result result)
        {
            return controller.StatusCode(result.StatusCode, result);
        }
    }
}
