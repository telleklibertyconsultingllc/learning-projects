using System;
using System.Collections.Generic;
using System.Text;
using DTOM = EX.DTO.Model;

namespace EX.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        DTOM.AuthorDto GetAuthors();
    }
}
