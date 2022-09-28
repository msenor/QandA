using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QandA.Data;

namespace QandA.Authorization
{
    public class MustBeQuestionAuthorHandler: AuthorizationHandler<MustBeQuestionAuthorRequirement>
    {
        private readonly IDataRepository _dataRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MustBeQuestionAuthorHandler(
            IDataRepository dataRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dataRepository = dataRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MustBeQuestionAuthorRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var questionId = _httpContextAccessor.HttpContext.Request.RouteValues["questionId"];
            int questionIdAsInt = Convert.ToInt32(questionId);
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var question = _dataRepository.GetQuestion(questionIdAsInt);

            //if the question can't be found go to the next piece of middleware
            if (question == null)
            {
                //let it through so the controller can return a 404
                context.Succeed(requirement);
                return;
            }
            //return failure if the user id in the question from the data repository is
            //different to the user id in the request
            if (question.UserId != userId)
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
            return;
        }
    }
}
