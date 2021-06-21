using Microsoft.Extensions.DependencyInjection;
using PagerApp.Application.Interfaces;
using PagerApp.Application.Services;
using PagerApp.Data.RepositoriesImpl;
using PagerApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerApp.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application Layer
            services.AddScoped<INoteService, NoteService>();

            //Data layer
            services.AddScoped<INoteRepository, NoteRepository>();
        }
    }
}
