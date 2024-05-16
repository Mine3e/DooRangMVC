using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Absracts
{
    public interface IWorldService
    {
        void AddWorld(World world);
        void DeleteWorld(int id);
        void UpdateWorld( int id,World world);
        World GetWorld(Func<World, bool>? func = null);
        List<World> GetAllWorlds(Func<World,bool>? func=null);
        
    }
}
