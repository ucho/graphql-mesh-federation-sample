using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using System;
using Accounts.Domain;
using Accounts.Infrastructure;

namespace Accounts.API
{

    public class AccountsSchema : Schema
    {
        public AccountsSchema(IServiceProvider provider) : base(provider)
        {
            Query = new AccountsQuery();
        }
    }

    public class AccountsQuery : ObjectGraphType
    {
        public AccountsQuery()
        {
            FieldAsync<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var db = context.RequestServices.GetRequiredService<AccountsDbContext>();
                    return await db.Users.FindAsync(id);
                });
        }
    }

    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(o => o.Id);
            Field(o => o.Email);
        }
    }
}