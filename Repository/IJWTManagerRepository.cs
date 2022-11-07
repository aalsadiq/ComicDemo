using ComicBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookAPI.Repository
{
   public interface IJWTManagerRepository
    {
        Tokens Authenticate(User users); 
    }
   
}