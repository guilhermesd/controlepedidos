using AutoMapper;
using Crosscutting.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<ProdutoDTO, Produto>();
            CreateMap<Produto, ProdutoDTO>();
        }
    }
}
