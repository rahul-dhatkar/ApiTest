using AutoMapper;
using Data.DB_Models;
using Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<AlbumViewModel, TblMasterAlbum>().ReverseMap();
        }
    }
}
