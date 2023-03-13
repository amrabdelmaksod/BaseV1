using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Certificates.Services
{
    public interface ICertificateRepository
    {
        Task<Certificate> GetCertificateById(int id);
        Task<IEnumerable<Certificate>> GetAllCertificates();
        Task AddCertificate(Certificate certificate);
        Task UpdateCertificate(Certificate certificate);
        Task DeleteCertificate(int id);
    }

    public class CertificateRepository : ICertificateRepository
    {
        private readonly IApplicationDbContext _context;

        public CertificateRepository(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Certificate> GetCertificateById(int id)
        {
            return await _context.Certificates.FindAsync(id);
        }

        public async Task<IEnumerable<Certificate>> GetAllCertificates()
        {
            return await _context.Certificates.ToListAsync();
        }

        public async Task AddCertificate(Certificate certificate)
        {
            await _context.Certificates.AddAsync(certificate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificate(Certificate certificate)
        {
            _context.Entry(certificate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCertificate(int id)
        {
            var certificate = await GetCertificateById(id);

            if (certificate != null)
            {
                _context.Certificates.Remove(certificate);
                await _context.SaveChangesAsync();
            }
        }
    }

}
