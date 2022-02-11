using backend.db;
using backend.model;

namespace backend.repository.EnterpriseRepository.cs
{
    public class EnterpriseRespository
    {
        private SKADIDBContext _context;
        private String idUser;
        public EnterpriseRespository(SKADIDBContext context, String idUser)
        {
            this._context = context;
            this.idUser = idUser;
        }
    }
}