using APITest.Controllers;
using AutoMapper;
using Data.DB_Models;
using Microsoft.Extensions.Logging;
using Serilog;
using Services.IRepository;
using Services.Repository;
using Services.ViewModel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestApi
{
    public class UnitTest1
    {
        private readonly IMapper _mapper;
        private IAlbumDetails abumDetails;
        private readonly IGenericRepository<TblMasterAlbum> _albumRepo;
        private readonly ILogger<AlbumDetails> _logger;
        public UnitTest1()
        {
            _albumRepo = new GenericRepository<TblMasterAlbum>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AlbumViewModel, TblMasterAlbum>().ReverseMap();

            });
            _mapper = config.CreateMapper();        

        }
        [Fact]
        public  async Task MatchData_GetAlbumResult()
        {
            //Arrange  
            abumDetails = new AlbumDetails(_logger,_mapper,_albumRepo);
            IdViewModel idViewModel = new IdViewModel();
            idViewModel.AlbumId = 3;
            //Act  
            ResponseViewModel data =await abumDetails.GetAlbumDetails(idViewModel);
            //Assert 
            AlbumDetailsViewModel albumDetails =(AlbumDetailsViewModel) data.Content;
            Assert.Equal("Classic", albumDetails.GenreName.Trim());
            Assert.Equal("Backstreet_Boys", albumDetails.albumName);
        }
        [Fact]
        public async Task NotFound_GetAlbumResult()
        {
            //Arrange  
            abumDetails = new AlbumDetails(_logger, _mapper, _albumRepo);
            IdViewModel idViewModel = new IdViewModel();
            idViewModel.AlbumId = 20 ;
            //Act  
            ResponseViewModel data = await abumDetails.GetAlbumDetails(idViewModel);
            //Assert              
            Assert.Null(data.Content);          

        }
    }
}
