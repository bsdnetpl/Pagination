using Bogus;
using Pagination.DB;
using Pagination.Models;
using System.Linq.Expressions;

namespace Pagination.Services
{
    public class UserServices : IUserServices
    {
        private readonly ConnectMssql _connectMssql;

        public UserServices(ConnectMssql connectMssql)
        {
            _connectMssql = connectMssql;
        }

        private static readonly Faker<User> _UserFaker = new Faker<User>()
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.dateTimeCreate, f => f.Date.Past());



        public List<User> CreateAllUsers(int amountOfUsers)
        {
            List<User> Users = _UserFaker.Generate(amountOfUsers);
            _connectMssql.users.AddRange(Users);
            _connectMssql.SaveChanges();
            return Users;
        }

        public  PageResult<User> GetUser(UserQuery userQuery)
        {
            var users = _connectMssql.users
                .Where(u => u.FirstName.ToLower().Contains(userQuery.SearchPhrase.ToLower()));
                
            if(!string.IsNullOrEmpty(userQuery.SortBy))
            {
                var columnSelection = new Dictionary<string, Expression<Func<User, object>>>()
                {
                    { nameof(User.FirstName), r => r.FirstName},
                    { nameof(User.LastName), r => r.LastName},
                    { nameof(User.Email), r => r.Email}

                };

                var selectedColumn = columnSelection[userQuery.SortBy];

                users = userQuery.SortOrder == SortOrder.ASC
                    ? users.OrderBy(selectedColumn)
                    : users.OrderByDescending(selectedColumn);
            }

            var UserAll = users
                .Skip(userQuery.PageSize * userQuery.PageNumber - 1)
                .Take(userQuery.PageSize)
                .ToList();

            var totalUsers = users.Count();

            var result = new PageResult<User>(UserAll, totalUsers, userQuery.PageSize, userQuery.PageNumber);
            return result;
        }



        //public Product GetSingleProduct()
        //{
        //    Product product = ProductFaker.Generate();
        //    return product;
        //}
    }
}
