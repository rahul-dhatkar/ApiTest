using Data.DB_Models;
using Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.IRepository
{
   public interface IAlbumDetails: IGenericRepository<TblMasterAlbum>
    {
        Task<ResponseViewModel> GetAlbumDetails(IdViewModel idViewModel);
    }
}
