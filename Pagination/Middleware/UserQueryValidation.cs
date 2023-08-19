using FluentValidation;
using Pagination.Models;

namespace Pagination.Middleware
{
    public class UserQueryValidation: AbstractValidator<UserQuery>
    {
        private int[] AllowedPageSize = new[] { 5, 10, 15 };
        public UserQueryValidation() 
        {
         RuleFor( r =>r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context)=>
            {
                if(!AllowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize",$"PageSize must in [{string.Join(",",AllowedPageSize)}]");
                }
            });

        }
    }
}
