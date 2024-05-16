using Business.Exceptions;
using Business.Services.Absracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class WorldService : IWorldService
    {
        private readonly IWorldRepository _worldRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WorldService(IWorldRepository worldRepository, IWebHostEnvironment webHostEnvironment)
        {
            _worldRepository = worldRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddWorld(World world)
        {
            if (!world.ImageFile.ContentType.Contains("image/"))
                throw new FilecontentTypeException("ImageFile", "File content type errror");
            if (world.ImageFile.Length > 2097152)
                throw new FileSizeException("ImageFile", "File size error");
            if (world == null) throw new WorldNullException("", "World null ola bilmez ");
            string path=_webHostEnvironment.WebRootPath+@"\Upload\World\"+world.ImageFile.FileName;
            using(FileStream stream=new FileStream(path,FileMode.Create))
            {
                world.ImageFile.CopyTo(stream);
            }
            world.ImageUrl = world.ImageFile.FileName;
            _worldRepository.Add(world);
            _worldRepository.Commit();
        }

        public void DeleteWorld(int id)
        {
            var existWorld=_worldRepository.Get(x=>x.Id==id);
            if (existWorld == null) throw new EntityNotFound("", "world not found ");
            
            string path=_webHostEnvironment.WebRootPath+@"\Upload\World\"+existWorld.ImageUrl;
            if (!File.Exists(path)) throw new FileNotFoundException("","File not found");
            File.Delete(path);
            _worldRepository.Delete(existWorld);
            _worldRepository.Commit() ; 
        }

        public World GetWorld(Func<World, bool>? func = null)
        {
            return _worldRepository.Get(func);
        }

        public List<World> GetAllWorlds(Func<World, bool>? func = null)
        {
            return _worldRepository.GetAll(func);
        }

        public void UpdateWorld(int id, World world)
        {
           var existworld=_worldRepository.Get(x=> x.Id==id);
            if (existworld == null) throw new EntityNotFound("", "world not found ");
            if (world.ImageFile != null)
            {
                if (!world.ImageFile.ContentType.Contains("image/"))
                    throw new FilecontentTypeException("ImageFile", "File content type errror");
                if (world.ImageFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "File size error");

                string path = _webHostEnvironment.WebRootPath + @"\Upload\World\" + world.ImageFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    world.ImageFile.CopyTo(stream);
                }
                string path1= _webHostEnvironment.WebRootPath+@"\Upload\World\"+existworld.ImageUrl;
                FileInfo fileInfo = new FileInfo(path1);
                fileInfo.Delete();
            existworld.ImageUrl = world.ImageFile.FileName;
            }
            existworld.Subtitle = world.Subtitle;
            existworld.Title = world.Title;
            _worldRepository.Commit();
        }
    }
}
