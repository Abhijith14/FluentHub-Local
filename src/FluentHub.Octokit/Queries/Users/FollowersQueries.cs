using FluentHub.Octokit.Helpers;
using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class FollowersQueries
    {
        public FollowersQueries() => new App();

        public async Task<List<User>> GetAllAsync(string login)
        {
            #region query
            var query = new Query()
                    .User(login)
                    .Followers(first: 30)
                    .Nodes
                    .Select(x => new User
                    {
                        AvatarUrl = x.AvatarUrl(100),
                        Name = x.Name,
                        Bio = x.Bio,
                        Login = x.Login,
                        IsOrganization = UserTypeDetectionHelper.IsOrganization(x.Id),
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
