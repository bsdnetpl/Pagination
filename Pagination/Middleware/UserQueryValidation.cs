using FluentValidation;
using Pagination.Models;

namespace Pagination.Middleware
{
    public class UserQueryValidation: AbstractValidator<UserQuery>
    {
        private int[] AllowedPageSize = new[] { 5, 10, 15 };
        private string[] AllowSortedByColumn = { nameof(User.FirstName), nameof(User.LastName), nameof(User.Email) };

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

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || AllowSortedByColumn.Contains(value))
                .WithMessage($"Sort by in optional or must be in [{string.Join(",",AllowSortedByColumn)}]");


        }
    }
}
