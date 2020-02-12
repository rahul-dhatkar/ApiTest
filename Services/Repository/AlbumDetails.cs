using Data.DB_Models;
using Services.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using Services.IRepository;

namespace Services.Repository
{
    public class AlbumDetails: GenericRepository<TblMasterAlbum>, IAlbumDetails
    {
        private readonly ILogger _log;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMasterAlbum> _albumRepo;       
        public AlbumDetails( ILogger<AlbumDetails> log, IMapper mapper, IGenericRepository<TblMasterAlbum> albumRepo)
        {
            _mapper = mapper;        
            _log = log;
            _albumRepo = albumRepo;            
        }
        public async Task<ResponseViewModel> GetAlbumDetails(IdViewModel idViewModel)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var album =  _albumRepo.GetById(idViewModel.AlbumId);
                var mAlbum = _mapper.Map<AlbumViewModel>(album);               
                
                if (mAlbum!=null)
                {           

                    var returnList =await (from s in _context.TblSongs
                                      join a in _context.TblMasterAlbum on s.AlbumId equals a.Id
                                      join ar in _context.TblMasterArtist on s.ArtistId equals ar.Id
                                      join g in _context.TblMasterGenre on s.GenreId equals g.Id
                                      where s.AlbumId==idViewModel.AlbumId
                                      group new { s, a, ar ,g} by new { s.AlbumId } into grp
                                      select new AlbumDetailsViewModel
                                      {
                                          GenreName = grp.FirstOrDefault().g.MusicType,
                                          albumName = grp.FirstOrDefault().a.Name,
                                          ArtistName = grp.FirstOrDefault().ar.FirstName + " " + grp.FirstOrDefault().ar.LastName,
                                          Thumbnail = grp.FirstOrDefault().a.Image,
                                          Price = grp.FirstOrDefault().a.Price,
                                          Rating = grp.FirstOrDefault().a.Rating,
                                          Review = grp.FirstOrDefault().a.Review,
                                          ReleaseDate = grp.FirstOrDefault().a.ReleasedDate,
                                          Songs=grp.Select(i=> new SongDetailsViewModel {
                                              Name = i.s.Name,
                                              Rating = i.s.Rating,
                                              Price = i.s.Price,
                                              Time = i.s.Time
                                          }).ToList()

                                      }).FirstOrDefaultAsync();
                 
                    if (mAlbum == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "RecordNotFound";
                        return response;
                    }
                    response.Content = returnList;
                    response.IsSuccess = true;
                    response.Message = "RecordFound";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _log.LogError("Error", ex.StackTrace);
                throw;
            }
            return response;
        }
    }
}
