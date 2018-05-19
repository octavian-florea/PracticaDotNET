using System.Collections.Generic;

namespace Practica.Core
{
    public interface ICompanyProfileRepository
    {
        void Add(CompanyProfile companyProfile);
        CompanyProfile Get(string UserId);
        void Remove(CompanyProfile companyProfile);
        bool Save();
    }
}