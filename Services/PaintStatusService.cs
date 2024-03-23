using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaintStockStatusAPI.Data;
using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Services
{
    public class PaintStatusService : IPaintStatusService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public PaintStatusService(IMapper mapper, DataContext context )
        {
            //_httpContextAccessor = httpContextAccessor; IHttpContextAccessor httpContextAccessor
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPaintStatusDto>>> GetAllPaints()
        {
            ServiceResponse<List<GetPaintStatusDto>> serviceResponse = new ServiceResponse<List<GetPaintStatusDto>>();

            List<PaintStatus> dbPaintStatus = await _context.PaintStatus.ToListAsync();
            serviceResponse.Data = dbPaintStatus.Select(o => _mapper.Map<GetPaintStatusDto>(o)).ToList();
            return serviceResponse;
        }
    }
}
