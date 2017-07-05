using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
  public class CompanyService
    {
        ICompanyRepository _CompanyRepository = null;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _CompanyRepository = companyRepository;
        }
        public Company GetById(int id)
        {
           return _CompanyRepository.GetById(id);
        }

    }
}
