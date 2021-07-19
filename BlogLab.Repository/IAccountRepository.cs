using System;

namespace BlogLab.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, 
            CancelationToken cancelationToken);
        public Task<ApplicationUserIdentity> GetByUsernameAsync(string normalizedUsername, 
            CancelationToken cancelationToken);
    }
}
