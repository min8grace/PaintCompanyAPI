using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaintStockStatusAPI.Data;
using PaintStockStatusAPI.Dtos;
using PaintStockStatusAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Services
{
    public class PaintInventoryService : IPaintInventoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public PaintInventoryService(IMapper mapper, DataContext context)
        {
            //_httpContextAccessor = httpContextAccessor; IHttpContextAccessor httpContextAccessor
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPaintInventoryDto>>> GetAllPaints()
        {
            ServiceResponse<List<GetPaintInventoryDto>> serviceResponse = new ServiceResponse<List<GetPaintInventoryDto>>();

            List<PaintInventory> dbPaintStatuses = await _context.PaintInventory.ToListAsync();
            serviceResponse.Data = dbPaintStatuses.Select(o => _mapper.Map<GetPaintInventoryDto>(o)).ToList();
            return serviceResponse;
        }


        public async Task<ServiceResponse<GetPaintInventoryDto>> GetPaintInventoryById(int ID)
        {
            ServiceResponse<GetPaintInventoryDto> serviceResponse = new ServiceResponse<GetPaintInventoryDto>();
            PaintInventory dbPaintStatus = await _context.PaintInventory.FirstOrDefaultAsync(o => (int)o.Colour == ID);
            serviceResponse.Data = _mapper.Map<GetPaintInventoryDto>(dbPaintStatus);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPaintInventoryDto>> UpdatePaintInventory(UpdatePaintInventoryDto updatePaintInventoryDto)
        {
            ServiceResponse<GetPaintInventoryDto> serviceResponse = new ServiceResponse<GetPaintInventoryDto>();
            try
            {
                PaintInventory dbPaintStatus = await _context.PaintInventory.FirstOrDefaultAsync(o => (int)o.Colour == (int)updatePaintInventoryDto.Colour);
                dbPaintStatus.Quantity = dbPaintStatus.Quantity;

                _context.PaintInventory.Update(dbPaintStatus);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetPaintInventoryDto>(dbPaintStatus);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
             
    }
}
