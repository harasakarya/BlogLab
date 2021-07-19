﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;

        public AccountRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user,
    CancelationToken cancelationToken)
        {
            cancelationToken.ThrowIfCancelationRequested();

            var dataTable = new DataTable();
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("NormalizedUsername", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable.Rows.Add(
                user.Username,
                user.NormalizedUsername,
                user.Email,
                user.NormalizedEmail,
                user.Fullname,
                user.PasswordHash
                );

            using (var connection = new SqlConnection(_config.GetConnectionString("Default Connection")))
            {
                await connection.OpenAsync(cancelationToken);

                await connection.ExcecuteAsync("Account_Insert", 
                    new {Account = dataTable.AsTableValueParameter("dbo.AccountType")}, commandType: CommandType.StoredProcedure);
            }

            return IdentityResult.Success;
        }
        public async Task<ApplicationUserIdentity> GetByUsernameAsync(string normalizedUsername,
            CancelationToken cancelationToken)
        {
            cancelationToken.ThrowIfCancelationRequested();

            ApplicationUserIdentity applicationUserIdentity;

            using (var connection = new SqlConnection(_config.GetConnectionString("Default Connection")))
            {
                await connection.OpenAsync(cancelationToken);

                applicationUserIdentity = await connection.QuerySingleOrDefault<applicationUserIdentity>(
                    "Account_GetByUsername", new
                    {
                        NormalizedUsername = normalizedUsername
                    },
                    commandType: CommandType.StoredProcedure
                    );
            }

            return applicationUserIdentity;
        }
    }
}
