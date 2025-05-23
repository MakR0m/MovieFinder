using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Mobile.Services.Interfaces
{
    public interface IPosterService // Сервис для определения пути к постеру фильма по его названию
    {
        public string GetPosterPath(string title);
    }
}
